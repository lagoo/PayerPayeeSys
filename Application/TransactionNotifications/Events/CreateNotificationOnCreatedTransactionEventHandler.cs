using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using Domain.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TransactionNotifications.Events
{
    public class CreateNotificationOnCreatedTransactionEventHandler : INotificationHandler<DomainEventNotification<TransactionCreatedEvent>>
    {
        private readonly IApplicationContext _context;

        public CreateNotificationOnCreatedTransactionEventHandler(IApplicationContext context)
        {
            _context = context;
        }        

        public Task Handle(DomainEventNotification<TransactionCreatedEvent> notification, CancellationToken cancellationToken)
        {
            Transaction transaction = notification.DomainEvent.Transaction;

            TransactionNotification transactionNotification = new TransactionNotification(transaction);

            _context.TransactionNotifications.Add(transactionNotification);

            return Task.CompletedTask;
        }
    }
}
