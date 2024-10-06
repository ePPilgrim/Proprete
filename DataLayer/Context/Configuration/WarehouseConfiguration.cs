using Entity.StaticData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Proprette.DataLayer.Context.Configuration;

public class WarehouseConfiguration : IEntityTypeConfiguration<Warehouse>
{
    public void Configure(EntityTypeBuilder<Warehouse> builder)
    {
        builder.HasAlternateKey(w => w.Name);
        builder.HasIndex(w => w.AddressId).IsUnique();
    }
}
