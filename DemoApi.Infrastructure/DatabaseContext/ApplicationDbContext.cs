﻿using Microsoft.EntityFrameworkCore;

namespace DemoApi.Infrastructure.DatabaseContext;

public class ApplicationDbContext:DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> contextOptions) : base(contextOptions)
    {
        
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        modelBuilder.Model.GetEntityTypes().SelectMany(x => x.GetForeignKeys()).ToList().ForEach(x => x.DeleteBehavior = DeleteBehavior.Restrict);
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
}
