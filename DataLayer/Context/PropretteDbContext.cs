
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Proprette.DataLayer.Context;

public class PropretteDbContext : DbContext
{
    public PropretteDbContext(DbContextOptions<PropretteDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}