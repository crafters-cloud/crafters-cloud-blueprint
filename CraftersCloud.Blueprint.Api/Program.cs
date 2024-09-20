﻿using CraftersCloud.Blueprint.Api;
using CraftersCloud.Blueprint.Infrastructure.Api.Init;
using CraftersCloud.Blueprint.Infrastructure.Configuration;
using CraftersCloud.Blueprint.Infrastructure.Init;
using Serilog;

internal class Program
{
    public static void Main(string[] args)
    {
        var bootstrapConfiguration = ConfigurationHelper.CreateBoostrapConfiguration();
        Log.Logger = new LoggerConfiguration()
            .AppConfigureSerilog(bootstrapConfiguration)
            .CreateBootstrapLogger();

        try
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Configuration.AddTestConfiguration();

            builder.WebHost.ConfigureKestrel(options =>
            {
                options.AddServerHeader = false;
            });

            builder.Configuration.AppAddAzureKeyVault(builder.Configuration);
            builder.Services.AppAddServices(builder.Configuration, builder.Environment);
            builder.Host.AppConfigureHost(builder.Configuration);

            var app = builder.Build();
            app.AppConfigureWebApplication();
            app.Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Host terminated unexpectedly");
            throw;
        }
        finally
        {
            Log.Information("Stopping web host");
            Log.CloseAndFlush();
        }
    }
}