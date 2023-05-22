using Microsoft.EntityFrameworkCore;
using Proprete.Data.Tables;

namespace Proprete.Data
{
    public class PropreteDbContext : DbContext
    {
        public PropreteDbContext(DbContextOptions<PropreteDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Warehouse>()
                .HasKey(k => new { k.ItemID, k.DateTime });
            modelBuilder.Entity<Warehouse>()
                .HasIndex(k => new { k.ItemID, k.DateTime });
            modelBuilder.Entity<OffWarehouse>()
                .HasKey(k => new { k.LocationID, k.ItemID, k.DateTime });
            modelBuilder.Entity<OffWarehouse>()
                .HasIndex(k => new { k.LocationID, k.ItemID, k.DateTime })
                .IsUnique();

        }
    }
}
