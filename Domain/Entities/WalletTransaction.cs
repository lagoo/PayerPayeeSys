using Common.Enums;

namespace Domain.Entities
{
    public class WalletTransaction
    {
        protected WalletTransaction(){}

        internal WalletTransaction(decimal amount)
        {
            Amount = amount;
        }

        public int Id { get; private set; }
        public decimal Amount { get; private set; }

        public int WalletId { get; private set; }
        public Wallet Wallet { get; private set; }

        public CurrencyTypeOperationEnum OperationType => Amount > 0 ? CurrencyTypeOperationEnum.input : CurrencyTypeOperationEnum.output;

    }
}
