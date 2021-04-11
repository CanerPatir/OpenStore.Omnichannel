using Microsoft.EntityFrameworkCore;

namespace OpenStore.Omnichannel.Infrastructure.Data.EntityFramework.Context
{
    public class MsSqlDbContext : ApplicationDbContext
    {
        public MsSqlDbContext(DbContextOptions options) : base(options)
        {
        }
    }
    
    public class MySqlDbContext : ApplicationDbContext
    {
        public MySqlDbContext(DbContextOptions options) : base(options)
        {
        }
    }
    
    public class PostgreSqlDbContext : ApplicationDbContext
    {
        public PostgreSqlDbContext(DbContextOptions options) : base(options)
        {
        }
    }
    
    public class SqliteDbContext : ApplicationDbContext
    {
        public SqliteDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}