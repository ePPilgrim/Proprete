using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Proprette.Domain.Models;

namespace Proprette.Domain.Configuration
{
    internal class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {

            builder
                .HasAlternateKey(k => new { k.ItemName, k.ItemType });
        }
    }
}
