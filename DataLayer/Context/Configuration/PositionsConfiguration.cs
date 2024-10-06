using Entity.ReportData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Context.Configuration;

public class PositionsConfiguration : IEntityTypeConfiguration<Position>
{
    public void Configure(EntityTypeBuilder<Position> builder)
    {
        builder
            .HasAlternateKey(p => new
            {
                p.HoldingId,
                p.Date,
            });

        builder
            .HasIndex(p => new
            {
                p.Date,
                p.HoldingId,
            })
            .IsUnique();
    }
}
