using System.Runtime.CompilerServices;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

[assembly: InternalsVisibleTo("Domain.UnitTests")]
namespace Domain.Entities
{
    public class Wallat : ITypedEntity
    {
        protected Wallat()
        {
            _transactions = new List<WallatTransaction>();
        }

        internal Wallat(decimal initialAmount) : this()
        {
            if (initialAmount > 0)
                In(initialAmount);

            Identifier = Guid.NewGuid();
        }

        public int Id { get; private set; }
        public Guid Identifier { get; private set; }

        public int UserId { get; private set; }
        public User User { get; private set; }

        public WallatTransaction In(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(amount), "amount can't be zero or negative number");

            var wt = new WallatTransaction(amount);

            _transactions.Add(wt);

            return wt;
        }

        public WallatTransaction Out(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(amount), "amount can't be zero or negative number");

            var wt = new WallatTransaction(amount * -1);

            _transactions.Add(wt);

            return wt;
        }

        public decimal GetBalance() => _transactions.Sum(e => e.Amount);

        
        private readonly List<WallatTransaction> _transactions;
        public IReadOnlyCollection<WallatTransaction> Transactions => _transactions;


        public int EntityId => Id;
        public string EntityUniqueIdentifier => Identifier.ToString();
        public string Type => "Carteira";
    }
}
