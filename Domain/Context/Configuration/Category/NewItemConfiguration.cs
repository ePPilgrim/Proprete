using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Proprette.Domain.Data.Entities.Category;

namespace Proprette.Domain.Context.Configuration.Category;

public class NewItemConfiguration : IEntityTypeConfiguration<NewItem> 
{
    public void Configure(EntityTypeBuilder<NewItem> builder)
    { 
    }
}