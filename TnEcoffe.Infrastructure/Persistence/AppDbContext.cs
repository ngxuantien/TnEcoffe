using Microsoft.EntityFrameworkCore;
using TnEcoffe.Infrastructure.Persistence.Conventions;

namespace TnEcoffe.Infrastructure.Persistence;

public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        modelBuilder.ApplyBaseEntityConventions();
        modelBuilder.ApplyEnumConventions();
    }
}
