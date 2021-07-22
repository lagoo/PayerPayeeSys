using Application.Common.Handlers;
using Application.Common.Interfaces;
using Common.Extensions;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<int>
    {
        public CreateUserCommand(string name,
                                 string document,
                                 string email,
                                 string password)
        {
            Name = name;
            Document = document;
            Email = email;
            Password = password;
        }

        public string Name { get; }
        public string Document { get; }
        public string Email { get; }
        public string Password { get; }

        public class Handler : CommandBaseHandler, IRequestHandler<CreateUserCommand, int>
        {
            public Handler(IApplicationContext context,
                ICurrentUserService currentUserService) : base(context, currentUserService)
            {
            }

            public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                User entity = new User(request.Name,
                                       request.Document,
                                       request.Email,
                                       request.Password);

                _context.Users.Add(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return entity.Id;
            }
        }
    }



    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        private readonly IApplicationContext _context;

        public CreateUserCommandValidator(IApplicationContext context)
        {
            this._context = context;

            RuleFor(e => e.Name)
               .NotEmpty().WithMessage("Nome não pode ser vazio")
               .MinimumLength(4).WithMessage("Nome deve conter no mínimo 4 letras");

            RuleFor(e => e.Email)
               .NotEmpty().WithMessage("E-Mail não pode ser vazio")
               .EmailAddress().WithMessage("E-Mail deve ser no formato correto")
               .MustAsync(UniqueEmail).WithMessage(e => $"Já existe um usuário com esse E-Mail {e.Email}");

            RuleFor(e => e.Document)
               .NotEmpty().WithMessage("CPF/CNPJ não pode ser vazio")
               .ValidDocument().WithMessage("CPF/CNPJ deve ser no formato correto")
               .MustAsync(UniqueDocument).WithMessage(e => $"Já existe um usuário com esse CPF/CNPJ {e.Document}");

        }

        private async Task<bool> UniqueEmail(string email, CancellationToken arg2)
        {
            return !await _context.Users.AsNoTracking().AnyAsync(e => e.Email == email.ToLower());
        }

        private async Task<bool> UniqueDocument(string document, CancellationToken arg2)
        {
            return !await _context.Users.AsNoTracking().AnyAsync(e => e.Document == document.ToLower());
        }
    }
}
