using System.Runtime.CompilerServices;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

[assembly: InternalsVisibleTo("Domain.UnitTests")]
namespace Domain.Entities
{
    public class Wallet : ITypedEntity
    {
        protected Wallet()
        {
            _transactions = new List<WalletTransaction>();
        }

        internal Wallet(decimal initialAmount) : this()
        {
            if (initialAmount > 0)
                In(initialAmount);

            Identifier = Guid.NewGuid();
        }

        public int Id { get; private set; }
        public Guid Identifier { get; private set; }

        public int UserId { get; private set; }
        public User User { get; private set; }

        public WalletTransaction In(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(amount), "amount can't be zero or negative number");

            var wt = new WalletTransaction(amount);

            _transactions.Add(wt);

            return wt;
        }

        public WalletTransaction Out(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(amount), "amount can't be zero or negative number");

            var wt = new WalletTransaction(amount * -1);

            _transactions.Add(wt);

            return wt;
        }

        public decimal GetBalance() => _transactions.Sum(e => e.Amount);

        
        private readonly List<WalletTransaction> _transactions;
        public IReadOnlyCollection<WalletTransaction> Transactions => _transactions;


        public int EntityId => Id;
        public string EntityUniqueIdentifier => Identifier.ToString();
        public string Type => "Carteira";
    }
}
