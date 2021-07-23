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
        public static readonly User BaseUser = new("Iago", "92426261803", "iagogs@gmail.com", "123", 100);
        public static readonly User[] Users = { BaseUser, new("Fernando", "94842021012", "fernando@gmail.com", "123", 100) };


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
