using Application.Common.Interfaces;
using Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using Persistence;
using System;

namespace Application.UnitTests.Core
{
    public static class ApplicationContextFactory
    {
        public static readonly User UserPayer = new("Pagador", "94638756034", "pagador@gmail.com", "123", 100);
        public static readonly User UserPayee = new("Beneficiario", "91623278015", "beneficiario@gmail.com", "123", 0);

        public static readonly User UserWithBalance = new("Iago", "92426261803", "iagogs@gmail.com", "123", 100);
        public static readonly User UserWithoutBalance = new("Fernando", "94842021012", "fernando@gmail.com", "123", 0);
        public static readonly User ShopkeeperUser = new("Loja", "64879030000100", "loja@gmail.com", "123", 100);
        public static readonly User[] Users = { UserWithBalance, UserWithoutBalance, ShopkeeperUser, UserPayer, UserPayee };        

        public static readonly Transaction transaction = new(UserPayer.Wallet, UserPayee.Wallet, 100);
        public static readonly TransactionNotification notification = new(transaction);

        public static ApplicationContext Create()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new ApplicationContext(options,
                new Mock<ICurrentUserService>().SetupService().Object,
                new Mock<IDomainEventService>().Object,
                new Mock<IDateTime>().Object);

            context.Database.EnsureCreated();

            context.Users.AddRange(Users);
            context.Transactions.Add(transaction);
            context.TransactionNotifications.Add(notification);

            context.SaveChanges();

            return context;
        }

        public static void Destroy(ApplicationContext context)
        {
            context.Database.EnsureDeleted();

            context.Dispose();
        }
    }
}
