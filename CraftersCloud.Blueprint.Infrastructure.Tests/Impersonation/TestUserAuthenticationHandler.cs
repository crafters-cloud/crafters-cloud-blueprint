﻿using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CraftersCloud.Blueprint.Infrastructure.Tests.Impersonation;

public class TestUserAuthenticationHandler(
    IOptionsMonitor<TestAuthenticationOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder)
    : AuthenticationHandler<TestAuthenticationOptions>(options, logger, encoder)
{
    public const string AuthenticationScheme = "TestUserAuth";

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var testPrincipal = Options.TestPrincipalFactory();

        var authResult = testPrincipal != null
            ? AuthenticatedUserResult(testPrincipal)
            : AuthenticateResult.NoResult(); // no user authenticated

        return Task.FromResult(authResult);
    }

    private static AuthenticateResult AuthenticatedUserResult(ClaimsPrincipal testPrincipal) =>
        AuthenticateResult.Success(new AuthenticationTicket(testPrincipal, AuthenticationScheme));
}

public class TestAuthenticationOptions : AuthenticationSchemeOptions
{
    public Func<ClaimsPrincipal?> TestPrincipalFactory { get; set; } = () => null;
}