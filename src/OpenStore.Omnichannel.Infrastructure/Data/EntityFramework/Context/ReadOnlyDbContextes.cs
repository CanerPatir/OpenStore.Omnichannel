using Microsoft.EntityFrameworkCore;

namespace OpenStore.Omnichannel.Infrastructure.Data.EntityFramework.Context;

public class ReadOnlyMsSqlDbContext : ReadOnlyApplicationDbContext
{
    public ReadOnlyMsSqlDbContext(DbContextOptions<ReadOnlyMsSqlDbContext> options) : base(options)
    {
    }
}

public class ReadOnlyMySqlDbContext : ReadOnlyApplicationDbContext
{
    public ReadOnlyMySqlDbContext(DbContextOptions<ReadOnlyMySqlDbContext> options) : base(options)
    {
    }
}

public class ReadOnlyPostgresDbContext : ReadOnlyApplicationDbContext
{
    public ReadOnlyPostgresDbContext(DbContextOptions<ReadOnlyPostgresDbContext> options) : base(options)
    {
    }
}

public class ReadOnlySqliteDbContext : ReadOnlyApplicationDbContext
{
    public ReadOnlySqliteDbContext(DbContextOptions<ReadOnlySqliteDbContext> options) : base(options)
    {
    }
}