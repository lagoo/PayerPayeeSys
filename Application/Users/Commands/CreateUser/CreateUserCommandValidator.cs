using Common.Extensions;
using FluentValidation;

namespace Application.Users.Commands.CreateUser
{
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
