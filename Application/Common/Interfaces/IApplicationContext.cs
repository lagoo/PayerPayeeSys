using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IApplicationContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Wallet> Wallets { get; set; }
        DbSet<Transaction> Transactions { get; set; }
        DbSet<TransactionNotification> TransactionNotifications { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        int SaveChanges();
    }
}
