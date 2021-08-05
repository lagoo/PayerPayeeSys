using Common;
using Common.Extensions;
using Domain.Common;
using FluentValidation;
using System.Linq;

namespace Domain.Entities
{
    public class User : AuditableEntity
    {
        protected User()
        {
        }

        public User(string name, string document, string email, string password, decimal initialAmount)
        {
            Name = name;
            Document = document;
            Email = email;
            Password = password.Hash();

            Wallet = new Wallet(initialAmount);
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Document { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }

        public Wallet Wallet { get; private set; }


        public bool ValidatePassword(string password)
        {
            var (Verified, NeedsUpgrade) = PasswordHasher.Check(Password, password);

            return Verified;
        }

        public override int EntityId => Id;
        public override string EntityUniqueIdentifier => $"{Document} - {Email}";
        public override string Type => "Usuário";

        public override bool IsValid()
        {
            _validationResults = new UserValidator().Validate(this).ToDictionaryOfErrors();

            return !_validationResults.Any();
        }
    }

    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
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

            RuleFor(e => e.Password)
               .NotEmpty().WithMessage("Senha não pode ser vazia");
        }
    }
}
