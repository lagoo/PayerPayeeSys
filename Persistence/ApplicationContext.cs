using Application.Common.Interfaces;
using Common.Interfaces;
using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Persistence
{
    public class ApplicationContext : DbContext, IApplicationContext, IDisposable
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTime _dateTime;
        private readonly IDomainEventService _domainEventService;
        
        public ApplicationContext(
            DbContextOptions<ApplicationContext> options,
            ICurrentUserService currentUserService,
            IDomainEventService domainEventService,
            IDateTime dateTime) : base(options)
        {
            _currentUserService = currentUserService;
            _domainEventService = domainEventService;
            _dateTime = dateTime;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(300)");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            await DispatchPreEvents();            

            foreach (EntityEntry<AuditableEntity> entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.MarkAsCreated(_currentUserService.UserId, _currentUserService.UserName, _dateTime);
                        break;
                    case EntityState.Modified:
                        entry.Entity.MarkAsChanged(_currentUserService.UserId, _currentUserService.UserName, _dateTime);
                        entry.Entity.MarkAsUnDeleted(_currentUserService.UserId, _currentUserService.UserName, _dateTime);
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.Entity.MarkAsDeleted(_currentUserService.UserId, _currentUserService.UserName, _dateTime);
                        break;
                }
            }


            var result = await base.SaveChangesAsync(cancellationToken);

            await DispatchPosEvents();

            return result;
        }

        private async Task DispatchPreEvents()
        {
            await _domainEventService.Publish(EventTime.PreSave);
        }

        private async Task DispatchPosEvents()
        {
            await _domainEventService.Publish(EventTime.PosSave);
        }
    }
}
