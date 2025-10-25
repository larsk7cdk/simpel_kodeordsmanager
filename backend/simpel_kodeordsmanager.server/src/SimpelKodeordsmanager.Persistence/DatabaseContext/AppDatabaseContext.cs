using Microsoft.EntityFrameworkCore;
using SimpelKodeordsmanager.Domain.Entities;
using SimpelKodeordsmanager.Domain.Entities.Shared;

namespace SimpelKodeordsmanager.Persistence.DatabaseContext;

public class AppDatabaseContext(DbContextOptions<AppDatabaseContext> options) : DbContext(options)
{
    // DbSets for entities
    public DbSet<UserEntity> Users { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Loads all configurations that implement IEntityTypeConfiguration<T> from the assembly
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDatabaseContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    // Add DateTime for add and modify entities
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in base.ChangeTracker.Entries<BaseEntity>()
                     .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified))
        {
            entry.Entity.DateModified = DateTime.Now;

            if (entry.State == EntityState.Added)
            {
                entry.Entity.DateCreated = DateTime.Now;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}