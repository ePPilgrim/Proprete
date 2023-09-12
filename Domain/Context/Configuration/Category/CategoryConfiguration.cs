using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Proprette.Domain.Data.Entities.Category;

namespace Proprette.Domain.Context.Configuration.Category;

public class CategoryConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : class, ICategory
{
    public void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder
            .Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(32);

        builder
            .HasAlternateKey(x => x.Name);

        builder
            .HasKey(x => x.Id);
    }
}
