using Entity.BasicData;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Context.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasAlternateKey(u => u.Name);

        builder
            .HasIndex(u => new
            {
                u.FirstName,
                u.LastName,
                u.Email
            })
            .IsUnique();
    }
}

