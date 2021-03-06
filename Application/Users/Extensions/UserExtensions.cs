using Application.Users.Specifications;
using Domain.Entities;

namespace Application.Users.Extensions
{
    public static class UserExtensions
    {        
        public static bool CanMakeTransaction(this User user, decimal amount)
        {
            return !user.IsShopkeeper() && user.HasSufficientBalance(amount);
        }

        public static bool IsShopkeeper(this User user)
        {
            return new ShopkeeperSpec().IsSatisfiedBy(user);
        }

        public static bool HasSufficientBalance(this User user, decimal amount)
        {
            return new SufficientBalanceSpec(amount).IsSatisfiedBy(user);
        }
    }
}
