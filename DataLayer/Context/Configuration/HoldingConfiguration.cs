using Entity.StaticData;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Context.Configuration;

public class HoldingConfiguration : IEntityTypeConfiguration<Holding>
{
    public void Configure(EntityTypeBuilder<Holding> builder)
    {
        builder.HasAlternateKey(h => new
        {
            h.ItemId,
            h.WarehouseId
        });
    }
}

