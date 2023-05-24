using DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
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
            modelBuilder.Entity<SubWarehouse>()
                .HasKey(k => new { k.LocationID, k.ItemID, k.DateTime });
            modelBuilder.Entity<SubWarehouse>()
                .HasIndex(k => new { k.LocationID, k.ItemID, k.DateTime })
                .IsUnique();

        }
    }
}
