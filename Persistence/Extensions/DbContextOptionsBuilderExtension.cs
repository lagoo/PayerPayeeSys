using Microsoft.EntityFrameworkCore;
using System;

namespace Persistence.Extensions
{
    public static class DbContextOptionsBuilderExtension
    {
        public static DbContextOptionsBuilder SetDbContextOptions(this DbContextOptionsBuilder options, DbConnectionConfig config)
        {
            string dbConnection = config.GetConnectionString();

            return config.Type.ToLower() switch
            {
                "sqlserver" => options.UseSqlServer(dbConnection),
                _ => throw new ArgumentException("Banco não suportado pela aplicação.")
            };
        }
    }
}
