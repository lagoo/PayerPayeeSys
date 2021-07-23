using Common.Enums;

namespace Domain.Entities
{
    public class WallatTransaction
    {
        protected WallatTransaction(){}

        internal WallatTransaction(decimal amount)
        {
            Amount = amount;
        }

        public int Id { get; private set; }
        public decimal Amount { get; private set; }

        public int WallatId { get; private set; }
        public Wallat Wallat { get; private set; }

        public CurrencyTypeOperationEnum OperationType => Amount > 0 ? CurrencyTypeOperationEnum.input : CurrencyTypeOperationEnum.output;

    }
}
