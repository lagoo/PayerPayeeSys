using Application.Users.Specifications;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Users.Extensions
{
    public static class UserExtensions
    {        
        public static bool CanMakeTransaction(this User user, decimal amount)
        {
            return !user.IsShopkeeper() && user.HasSufficientBalance(amount);
        }

        private static bool IsShopkeeper(this User user)
        {
            return new ShopkeeperSpec().IsSatisfiedBy(user);
        }

        private static bool HasSufficientBalance(this User user, decimal amount)
        {
            return new SufficientBalanceSpec(amount).IsSatisfiedBy(user);
        }
    }
}
