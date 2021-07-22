using Application.Common.Handlers;
using Application.Common.Interfaces;
using Application.Users.Extensions;
using Common.Extensions;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string Document { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public class Handler : CommandBaseHandler, IRequestHandler<CreateUserCommand, int>
        {
            public Handler(IApplicationContext context,
                ICurrentUserService currentUserService) : base(context, currentUserService)
            {
            }

            public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                
                if (await _context.HasUserEmail(request.Email))
                    throw new Common.Exceptions.ValidationException("E-mail", new string[] { $"Já existe um usuário com esse E-Mail {request.Email}" });

                if (await _context.HasUserDocument(request.Document))
                    throw new Common.Exceptions.ValidationException("CPF/CNPJ", new string[] { $"Já existe um usuário com esse CPF/CNPJ {request.Document}" });

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
        public CreateUserCommandValidator()
        {
            RuleFor(e => e.Name)
               .NotEmpty().WithMessage("Nome não pode ser vazio")
               .MinimumLength(4).WithMessage("Nome deve conter no mínimo 4 letras");

            RuleFor(e => e.Email)
               .NotEmpty().WithMessage("E-Mail não pode ser vazio")
               .EmailAddress().WithMessage("E-Mail deve ser no formato correto");

            RuleFor(e => e.Document)
               .NotEmpty().WithMessage("CPF/CNPJ não pode ser vazio")
               .ValidDocument().WithMessage("CPF/CNPJ deve ser no formato correto");
        }
    }
}
