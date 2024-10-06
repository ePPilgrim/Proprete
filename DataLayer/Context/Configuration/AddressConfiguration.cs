using Entity.BasicData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Proprette.DataLayer.Context.Configuration;

public class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.HasAlternateKey(a => a.Name);

        builder
            .HasIndex(a => new
            {
                a.City, 
                a.Street,
                a.Building
            })
            .IsUnique();
    }
}
