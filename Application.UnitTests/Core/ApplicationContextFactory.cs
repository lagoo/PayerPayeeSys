using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;

namespace Application.UnitTests.Core
{
    public class ApplicationContextFactory
    {
        public static ApplicationContext Create()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new ApplicationContext(options);

            context.Database.EnsureCreated();

            context.Users.AddRange(new[] {
                new User("Iago", "92426261803","iagogs@gmail.com","123")
            });

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
