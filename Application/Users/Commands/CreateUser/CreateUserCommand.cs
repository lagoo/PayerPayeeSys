using Application.Common.Handlers;
using Application.Common.Interfaces;
using Application.Users.Extensions;
using Domain.Entities;
using MediatR;
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
        public decimal InitialAmount { get; set; }

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
                                       request.Password,
                                       request.InitialAmount);

                _context.Users.Add(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return entity.Id;
            }
        }
    }
}
