using Microsoft.EntityFrameworkCore;

namespace OpenStore.Omnichannel.Infrastructure.Data.EntityFramework.Context;

public class MsSqlDbContext : ApplicationDbContext
{
    public MsSqlDbContext(DbContextOptions<MsSqlDbContext> options) : base(options)
    {
    }
}

public class MySqlDbContext : ApplicationDbContext
{
    public MySqlDbContext(DbContextOptions<MySqlDbContext> options) : base(options)
    {
    }
}

public class PostgresDbContext : ApplicationDbContext
{
    public PostgresDbContext(DbContextOptions<PostgresDbContext> options) : base(options)
    {
    }
}

public class SqliteDbContext : ApplicationDbContext
{
    public SqliteDbContext(DbContextOptions<SqliteDbContext> options) : base(options)
    {
    }
}