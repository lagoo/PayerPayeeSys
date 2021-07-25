using Domain.Entities;
using NetDevPack.Specification;
using System;
using System.Linq.Expressions;

namespace Application.Users.Specifications
{
    public class SufficientBalanceSpec : Specification<User>
    {
        private readonly decimal amount;

        public SufficientBalanceSpec(decimal amount)
        {
            this.amount = amount;
        }

        public override Expression<Func<User, bool>> ToExpression()
        {
            return user => user.Wallet.GetBalance() >= amount;
        }
    }
}
