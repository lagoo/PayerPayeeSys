using Application.Common.Interfaces;
using Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TransactionNotifications.Commands.SendTransactionNotifications
{
    public class SendTransactionNotificationsCommand : IRequest
    {

        public class Handler : IRequestHandler<SendTransactionNotificationsCommand>
        {
            private readonly IMessageService _messageService;
            private readonly IDateTime _dateTime;
            private readonly IApplicationContext _context;

            public Handler(IMessageService messageService, IDateTime dateTime, IApplicationContext context)
            {
                this._messageService = messageService;
                this._dateTime = dateTime;
                _context = context;
            }

            public async Task<Unit> Handle(SendTransactionNotificationsCommand request, CancellationToken cancellationToken)
            {
                IEnumerable<TransactionNotification> notifications = await _context.TransactionNotifications.Where(e => !e.Sended)
                                                                                                            .ToListAsync();

                foreach (var notification in notifications)
                {
                    try
                    {
                        bool result = await _messageService.SendMessage();

                        if (result)
                            notification.MarkAsSended(_dateTime);
                    }
                    catch (Exception ex)
                    {

                    }
                }

                return Unit.Value;
            }
        }
    }
}
