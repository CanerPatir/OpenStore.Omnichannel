using Microsoft.EntityFrameworkCore;
using OpenStore.Data.EntityFramework.ReadOnly;

namespace OpenStore.Omnichannel.Infrastructure.Data.EntityFramework.Context;

public class ReadOnlyApplicationDbContext : ApplicationDbContext, IReadOnlyDbContext
{
    public ReadOnlyApplicationDbContext(DbContextOptions options) : base(options)
    {
        ChangeTracker.AutoDetectChangesEnabled = false;
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }
    
    public sealed override int SaveChanges(bool acceptAllChangesOnSuccess) => throw new InvalidOperationException("This context is read-only.");

    public sealed override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new()) => throw new InvalidOperationException("This context is read-only.");
}