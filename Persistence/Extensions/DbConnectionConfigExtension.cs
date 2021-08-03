using System;

namespace Persistence.Extensions
{
    public static class DbConnectionConfigExtension
    {
        public static string GetConnectionString(this DbConnectionConfig config)
        {
            return config.Type.ToLower() switch
            {
                "sqlserver" => config.GetConnectionForSqlServer(),
                _ => throw new ArgumentException("Banco não suportado pela aplicação.")
            };
        }

        private static string GetConnectionForSqlServer(this DbConnectionConfig config)
        {
            string conn = string.Format("data source={0}; initial catalog={1}; persist security info=True; user id={2}; password={3}; MultipleActiveResultSets=True; App=EntityFrameworkCore",
                config.Server, config.Database, config.User, config.Password);

            return conn;
        }
    }
}
