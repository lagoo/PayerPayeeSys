using Application.Common.Interfaces;
using Application.Common.Interfaces.Services;
using Domain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Persistence
{
    public class EntitySeederService : ISeedService
    {
        private readonly ApplicationContext _context;

        public EntitySeederService(ApplicationContext context)
        {
            this._context = context;
        }

        public async Task Seed()
        {
            await _context.Database.EnsureCreatedAsync();

            if (!_context.Users.Any())
            {
                User user1 = new User("Iago", "92426261803", "iagogs@gmail.com", "123", 100);
                User user2 = new User("Fernando", "94842021012", "fernando@gmail.com", "123", 0);
                User user3 = new User("Loja", "64879030000100", "loja@gmail.com", "123", 100);

                _context.Users.Add(user1);
                _context.Users.Add(user2);
                _context.Users.Add(user3);

                await _context.SaveChangesAsync(new CancellationToken());
            }            
        }
    }
}
