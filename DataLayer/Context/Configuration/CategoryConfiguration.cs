using Proprette.DataLayer.Entity.BasicData.Category;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Proprette.DataLayer.Context.Configuration;

public class CategoryConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : class, ICategory, new()
{
    public void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder
            .Property(x => x.Name)
            .HasMaxLength(ConfigurationHelper.MaxLengthOfCategoryName)
            .IsRequired();

        builder
            .HasAlternateKey(x => x.Name);
            
        builder
            .HasKey(x => x.Id);

        builder
            .HasData(new TEntity { Id = ConfigurationHelper.IdOfEmptyCategoryName, Name = string.Empty });
    }
}
