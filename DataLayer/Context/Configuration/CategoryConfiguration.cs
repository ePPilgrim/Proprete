﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Proprette.DataLayer.Entity.Category;

namespace Proprette.DataLayer.Context.Configuration;

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
