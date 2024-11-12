using Proprette.DataLayer.Entity.StaticData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Proprette.DataLayer.Context.Configuration;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.HasIndex(t => new
        {
            t.Date,
            t.HoldingId,
            t.UserId,
        });

        builder.Property<DateTime>("TS");
    }
}
