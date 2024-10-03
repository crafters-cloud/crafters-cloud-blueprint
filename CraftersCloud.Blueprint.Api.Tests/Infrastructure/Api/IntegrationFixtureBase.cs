﻿using CraftersCloud.Blueprint.Infrastructure.Api.Init;
using CraftersCloud.Blueprint.Infrastructure.Data;
using CraftersCloud.Blueprint.Infrastructure.Tests;
using CraftersCloud.Blueprint.Infrastructure.Tests.Configuration;
using CraftersCloud.Blueprint.Infrastructure.Tests.Database;
using CraftersCloud.Blueprint.Infrastructure.Tests.Impersonation;
using CraftersCloud.Core.AspNetCore.Tests.SystemTextJson.Http;
using CraftersCloud.Core.AspNetCore.Tests.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CraftersCloud.Blueprint.Api.Tests.Infrastructure.Api;

public class IntegrationFixtureBase
{
    private IConfiguration _configuration = null!;
    private TestDatabase? _testDatabase;
    private IServiceScope? _testScope;
    private static ApiWebApplicationFactory? _factory;
    private bool _isUserAuthenticated = true;
    private HttpClient? _client;
    protected HttpClient Client => _client!;

    [Before(Test)]
    public async Task Setup()
    {
        _testDatabase = new TestDatabase();

        _configuration = new TestConfigurationBuilder()
            .WithDbContextName(nameof(AppDbContext))
            .WithConnectionString(_testDatabase.ConnectionString)
            .Build();

        _factory = new ApiWebApplicationFactory(_configuration, _isUserAuthenticated);

        var scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();
        _testScope = scopeFactory.CreateScope();
        _client = _factory.CreateClient();

        var dbContext = Resolve<DbContext>();
        await TestDatabase.ResetAsync(dbContext);

        SeedTestUsers();

        AddApiJsonConverters();
    }

    protected void DisableUserAuthentication() => _isUserAuthenticated = false;

    private void SeedTestUsers()
    {
        var dbContext = Resolve<DbContext>();
        new TestUserDataSeeding(dbContext).Seed();
    }

    [After(Test)]
    public void Teardown()
    {
        _factory?.Dispose();
        _testScope?.Dispose();
        _client?.Dispose();
    }

    protected async Task AddAndSaveChangesAsync(params object[] entities)
    {
        var dbContext = Resolve<DbContext>();
        dbContext.AddRange(entities);
        await dbContext.SaveChangesAsync();
    }

    protected void AddToDbContext(params object[] entities)
    {
        var dbContext = Resolve<DbContext>();
        dbContext.AddRange(entities);
    }

    private static void AddApiJsonConverters()
    {
        var converters = HttpSerializationOptions.Options.Converters;

        // Guard if multiple tests are run in one context.
        if (converters.Count > 0)
        {
            return;
        }

        converters.AppRegisterJsonConverters();
    }

    protected Task SaveChangesAsync() => Resolve<DbContext>().SaveChangesAsync();

    protected IQueryable<T> QueryDb<T>() where T : class => Resolve<DbContext>().Set<T>();

    protected IQueryable<T> QueryDbSkipCache<T>() where T : class => Resolve<DbContext>().QueryDbSkipCache<T>();

    protected Task DeleteByIdsAndSaveChangesAsync<T, TId>(params TId[] ids) where T : class =>
        Resolve<DbContext>().DeleteByIdsAndSaveChangesAsync<T, TId>(ids);

    private Task DeleteByIdAsync<T, TId>(TId id) where T : class => Resolve<DbContext>().DeleteByIdAsync<T, TId>(id);

    protected T Resolve<T>() where T : notnull => _testScope!.Resolve<T>();

    protected void SetFixedUtcNow(DateTimeOffset value)
    {
        var settableTimeProvider = Resolve<SettableTimeProvider>();
        settableTimeProvider.SetNow(value);
    }
}