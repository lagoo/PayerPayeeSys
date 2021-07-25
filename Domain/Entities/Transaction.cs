using Domain.Interfaces;
using System;

namespace Domain.Entities
{
    public class Transaction : ITypedEntity
    {
        protected Transaction(){ }

        public Transaction(Wallet payer, Wallet payee, decimal amount)
        {
            if (payer is null)            
                throw new ArgumentNullException(nameof(payer));            

            if (payee is null)            
                throw new ArgumentNullException(nameof(payee));            

            Identifier = Guid.NewGuid();
            Payer = payer.Out(amount);
            Payee = payee.In(amount);            
        }

        public int Id { get; private set; }
        public Guid Identifier { get; private set; }
        public DateTime CreatedOn { get; internal set; } = DateTime.UtcNow;


        public int PayerId { get; private set; }
        public WalletTransaction Payer { get; private set; }


        public int PayeeId { get; private set; }
        public WalletTransaction Payee { get; private set; }


        public int EntityId => Id;
        public string EntityUniqueIdentifier => Identifier.ToString();
        public string Type => "Transação";        
    }
}
