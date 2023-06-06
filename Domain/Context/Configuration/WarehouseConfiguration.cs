using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Proprette.Domain.Entities;

namespace Proprette.Domain.Context.Configuration;

internal class WarehouseConfiguration : IEntityTypeConfiguration<Warehouse>
{
    public void Configure(EntityTypeBuilder<Warehouse> builder)
    {
        builder
            .HasKey(k => new { k.ItemID, k.DateTime });
        builder
            .HasIndex(k => new { k.ItemID, k.DateTime });
    }
}
