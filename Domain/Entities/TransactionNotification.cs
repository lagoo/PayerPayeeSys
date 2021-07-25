using Common.Interfaces;
using System;

namespace Domain.Entities
{
    public class TransactionNotification
    {
        protected TransactionNotification()
        {
        }

        public TransactionNotification(Transaction transaction)
        {
            Transaction = transaction;
            Sended = false;
        }

        public int Id { get; private set; }
        public DateTime? SendOn { get; private set; }
        public bool Sended { get; private set; }

        public void MarkAsSended(IDateTime dateTime)
        {
            Sended = true;
            SendOn = dateTime.Now;
        }

        public int TransactionId { get; private set; }
        public Transaction Transaction { get; private set; }
    }
}
