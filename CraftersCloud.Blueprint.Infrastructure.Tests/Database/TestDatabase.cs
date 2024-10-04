using CraftersCloud.Blueprint.Domain.Authorization;
using CraftersCloud.Blueprint.Domain.Users;
using CraftersCloud.Blueprint.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Testcontainers.MsSql;
using Assembly = System.Reflection.Assembly;

namespace CraftersCloud.Blueprint.Infrastructure.Tests.Database;

internal class TestDatabase
{
    public string ConnectionString { get; }
    private static MsSqlContainer? _container;

    private static readonly IEnumerable<string> TablesToIgnore =
        ["__EFMigrationsHistory", nameof(Role), nameof(RolePermission), nameof(Permission), nameof(UserStatus)];

    public TestDatabase()
    {
        var connectionString = ReadConnectionString();
        if (!string.IsNullOrEmpty(connectionString))
        {
            // do not write ConnectionString to the console since it might contain username/password 
            ConnectionString = connectionString;
        }
        else
        {
            try
            {
                // These cannot be changed (it is hardcoded in MsSqlBuilder and changing any of them breaks starting of the container
                // default database: master
                // default username: sa
                // default password: yourStrong(!)Password

                _container ??= new MsSqlBuilder()
                    .WithAutoRemove(true)
                    .WithCleanUp(true)
                    .Build();

                _container!.StartAsync().Wait();
                ConnectionString = _container.GetConnectionString();
                WriteLine($"Docker SQL connection string: {ConnectionString}");
            }
            catch (Exception e)
            {
                WriteLine($"Failed to start docker container: {e.Message}");
                throw;
            }
        }
    }

    private string? ReadConnectionString()
    {
        var configuration = ReadTestConfiguration();
        // To use a local sqlServer instance, Create an Environment variable using R# Test Runner, with name "IntegrationTestsConnectionString"
        // and value: "Server=.;Database={DatabaseName};Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=False"
        var connectionString = configuration["environmentVariables:IntegrationTestsConnectionString"];
        if (connectionString == null)
        {
            connectionString = Environment.GetEnvironmentVariable("IntegrationTestsConnectionString");
        }

        return connectionString;
    }

    private IConfiguration ReadTestConfiguration()
    {
        var configurationBuilder = new ConfigurationBuilder();
        // how to get the assembly name of the entry assembly
        var assemblyName = Assembly.GetEntryAssembly()?.GetName().Name ??
                           throw new InvalidOperationException("Failed to get assembly name");

        configurationBuilder.SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile($"{assemblyName}.testconfig.json", true);
        return configurationBuilder.Build();
    }

    public static Task ResetAsync(DbContext dbContext) =>
        DatabaseInitializer.RecreateDatabaseAsync(dbContext, TablesToIgnore);

    private static void WriteLine(string value) => TestContext.Current?.OutputWriter.WriteLine(value);
}