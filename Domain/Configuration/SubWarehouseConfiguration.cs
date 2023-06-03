using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Proprette.Domain.Models;

namespace Proprette.Domain.Configuration
{
    internal class SubWarehouseConfiguration : IEntityTypeConfiguration<SubWarehouse>
    {
        public void Configure(EntityTypeBuilder<SubWarehouse> builder)
        {
            builder
                .HasKey(k => new { k.LocationID, k.ItemID, k.DateTime });
            builder
                .HasIndex(k => new { k.LocationID, k.ItemID, k.DateTime })
                .IsUnique();
        }
    }
}
