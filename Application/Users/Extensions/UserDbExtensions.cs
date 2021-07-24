using Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Application.Users.Extensions
{
    public static class UserDBExtensions
    {
        public static async Task<bool> HasUserEmail(this IApplicationContext context, string email)
        {
            return await context.Users.AsNoTracking().AnyAsync(e => e.Email == email.ToLower());
        }

        public static async Task<bool> HasUserDocument(this IApplicationContext context, string document)
        {
            return await context.Users.AsNoTracking().AnyAsync(e => e.Document == document.ToLower());
        }
    }
}
