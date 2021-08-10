using Application.Common.Exceptions;
using Application.Common.Handlers;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Services;
using Application.Users.Extensions;
using Domain.Entities;
using Domain.Events;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Transactions.Commands.CreateTransaction
{
    public class CreateTransactionCommand : IRequest<Guid>
    {
        public decimal Amount { get; set; }
        public int Payer { get; set; }
        public int Payee { get; set; }

        internal int[] Ids => new int[] { Payee, Payer };


        public class Handler : CommandBaseHandler, IRequestHandler<CreateTransactionCommand,Guid>
        {
            private readonly IAuthorizationService _authorization;
            private readonly IDomainEventService _domainEventService;

            public Handler(IApplicationContext context, ICurrentUserService currentUserService,
                IAuthorizationService authorization, IDomainEventService domainEventService) : base(context, currentUserService)
            {
                this._authorization = authorization;
                this._domainEventService = domainEventService;
            }

            public async Task<Guid> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
            {
                IEnumerable<Wallet> wallets = await _context.Wallets.Include(e => e.Transactions)
                                                                    .Include(e => e.User)
                                                                    .Where(e => request.Ids.Contains(e.UserId))
                                                                    .ToListAsync();

                if (wallets.Count() != 2)
                    throw new NotFoundException(nameof(User), new object[] { request.Payer, request.Payee });

                Wallet payer = wallets.Where(e => e.Id == request.Payer)
                                      .FirstOrDefault();

                Wallet payee = wallets.Where(e => e.Id == request.Payee)
                                      .FirstOrDefault();

                //if (payer == null)
                //    throw new NotFoundException(nameof(User), request.Payer);

                //if (payee == null)
                //    throw new NotFoundException(nameof(User), request.Payee);

                if (payer.User.IsShopkeeper())
                    throw new WarningException("Usuário é um lojista!");

                if (!payer.User.HasSufficientBalance(request.Amount))
                    throw new WarningException("Saldo insuficiente para realizar a transação!");

                //if (!payer.User.CanMakeTransaction(request.Amount))
                //    throw new WarningException("Saldo insuficiente para realizar a transação e/ou usuário é um lojista!");

                if (!await _authorization.Authorize())
                    throw new WarningException("Transação não autorizada!");                
                
                Transaction transaction = new Transaction(payer, payee, request.Amount);

                _domainEventService.AddEvent(new TransactionCreatedEvent(transaction, payee.User), EventTime.PreSave);

                _context.Transactions.Add(transaction);

                await _context.SaveChangesAsync(cancellationToken);

                return transaction.Identifier;
            }
        }
    }
}
