using Microsoft.EntityFrameworkCore;

namespace Proprette.Domain.Services;

public class PropretteDbContext : DbContext
{
    public PropretteDbContext(DbContextOptions<PropretteDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PropretteDbContext).Assembly);
    }
}
