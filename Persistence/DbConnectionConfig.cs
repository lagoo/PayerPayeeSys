namespace Persistence
{
    public class DbConnectionConfig
    {
        public DbConnectionConfig()
        {
            Type = "SqlServer";
        }
        
        public string Type { get; set; }
        public string Server { get; set; }
        public string Database { get; set; }
        public string User { get; set; }        
        public string Password { get; set; }        
    }
}
