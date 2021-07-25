using FluentValidation;

namespace Application.Transactions.Commands.CreateTransaction
{
    public class CreateTransactionCommandValidator : AbstractValidator<CreateTransactionCommand>
    {
        public CreateTransactionCommandValidator()
        {
            RuleFor(e => e.Amount)
               .GreaterThan(0).WithMessage("Amount não poder ser menor ou igual a 0.");

            RuleFor(e => e.Payer)
               .NotEqual(0).WithMessage("Payer deve ser um Id válido.");

            RuleFor(e => e.Payee)
               .NotEqual(0).WithMessage("Payee deve ser um Id válido.");
        }
    }
}
