using Domain.Common;
using Domain.Entities;

namespace Domain.Events
{
    public class TransactionCreatedEvent : DomainEvent
    {
        public TransactionCreatedEvent(Transaction transaction, User payee)
        {
            Transaction = transaction;
            Payee = payee;
        }

        public Transaction Transaction { get; }
        public User Payee { get; }
    }
}
