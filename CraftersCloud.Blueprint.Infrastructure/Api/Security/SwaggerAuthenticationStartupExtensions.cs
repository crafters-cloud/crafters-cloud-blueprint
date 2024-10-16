﻿using CraftersCloud.Core.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Web;
using NSwag.Generation.AspNetCore;

namespace CraftersCloud.Blueprint.Infrastructure.Api.Security;

public static class SwaggerAuthenticationStartupExtensions
{
    public static void AppUseSwaggerWithAzureAdAuth(this IApplicationBuilder app, IConfiguration configuration)
    {
        var aadOptions = configuration.GetSection(AuthenticationStartupExtensions.AzureAdSection)
            .Get<MicrosoftIdentityOptions>()!;
        app.UseCoreSwaggerWithOAuth2Client(aadOptions.ClientId!, path: "/api");
    }

    public static void AppAddSwaggerWithAzureAdAuth(this IServiceCollection services,
        IConfiguration configuration,
        string appTitle,
        string appVersion = "v1",
        Action<AspNetCoreOpenApiDocumentGeneratorSettings>? configureSettings = null)
    {
        var aadOptions = configuration.GetSection(AuthenticationStartupExtensions.AzureAdSection)
            .Get<MicrosoftIdentityOptions>()!;
        var authorityUrl = $"{aadOptions.Instance}/{aadOptions.TenantId ?? aadOptions.Domain}";

        if (aadOptions.SignUpSignInPolicyId != null)
        {
            authorityUrl += $"/{aadOptions.SignUpSignInPolicyId}";
        }

        var scopesDictionary = aadOptions.Scope.ToDictionary(scope => scope, _ => "");

        services.AddCoreSwaggerWithAuthorizationCode(
            appTitle,
            $"{authorityUrl}/oauth2/v2.0/authorize",
            $"{authorityUrl}/oauth2/v2.0/token",
            scopesDictionary,
            appVersion,
            configureSettings
        );
    }
}