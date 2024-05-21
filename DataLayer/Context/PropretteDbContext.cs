﻿
using Microsoft.EntityFrameworkCore;
using Proprette.DataLayer.Context.Configuration;
using Proprette.DataLayer.Entity.Category;

namespace Proprette.DataLayer.Context;

public class PropretteDbContext : DbContext
{
    public PropretteDbContext(DbContextOptions<PropretteDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CategoryConfiguration<Brand>).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CategoryConfiguration<Color>).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ItemConfiguration).Assembly);

        modelBuilder.Entity<Brand>().HasData(new Brand(string.Empty) { Id = -1});
        modelBuilder.Entity<Color>().HasData(new Color(string.Empty) { Id = -1});
    }
}