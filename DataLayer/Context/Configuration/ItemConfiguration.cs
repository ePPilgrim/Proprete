using Proprette.DataLayer.Entity.StaticData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Proprette.DataLayer.Context.Configuration;

public class ItemConfiguration : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder
            .HasIndex(e => new
            {
                e.BrandId, 
                e.ItemTypeId,
                e.UsageId,
                e.ColorId,
                e.CapacityId,
                e.SizeId,
                e.UnitId,
                e.SubItemId,
                e.CompositionId,
                e.FreeCode1Id,
                e.FreeCode2Id,
                e.FreeCode3Id
            })
            .IsUnique();
    }
}