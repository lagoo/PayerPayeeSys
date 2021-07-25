using Common.Extensions;
using FluentValidation;

namespace Application.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(e => e.Name)
               .NotEmpty().WithMessage("Name não pode ser vazio")
               .MinimumLength(4).WithMessage("Name deve conter no mínimo 4 letras");

            RuleFor(e => e.Email)
               .NotEmpty().WithMessage("E-Mail não pode ser vazio")
               .EmailAddress().WithMessage("E-Mail deve ser no formato correto");

            RuleFor(e => e.Document)
               .NotEmpty().WithMessage("Document não pode ser vazio")
               .ValidDocument().WithMessage("Document deve ser no formato de CPF/CNPJ");
        }
    }
}
