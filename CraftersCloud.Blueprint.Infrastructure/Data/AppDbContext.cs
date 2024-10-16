﻿using CraftersCloud.Blueprint.Infrastructure.Api.Init;
using CraftersCloud.Core.EntityFramework.Infrastructure;
using CraftersCloud.Core.SmartEnums.EntityFramework;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace CraftersCloud.Blueprint.Infrastructure.Data;

[UsedImplicitly]
public class AppDbContext(DbContextOptions options) : EntitiesDbContext(CreateOptions(), options)
{
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder) =>
        configurationBuilder.Properties<string>()
            .HaveMaxLength(255);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // first we need to build the model so that we can later configure the smart enums
        base.OnModelCreating(modelBuilder);
        modelBuilder.CoreConfigureSmartEnums();
    }

    private static EntitiesDbContextOptions CreateOptions() => new()
    {
        ConfigurationAssembly = AssemblyFinder.InfrastructureAssembly, EntitiesAssembly = AssemblyFinder.DomainAssembly
    };
}