using CraftersCloud.Blueprint.Api.Features.Authorization;
using CraftersCloud.Blueprint.Api.Tests.Infrastructure.Api;
using CraftersCloud.Core.AspNetCore.Tests.SystemTextJson.Http;

namespace CraftersCloud.Blueprint.Api.Tests.Features;

[Category("integration")]
[NotInParallel]
public class ProfileControllerFixture : IntegrationFixtureBase
{
    [Test]
    public async Task TestGetProfileDetails()
    {
        var profile = await Client.GetAsync<GetUserProfile.Response>("api/profile");
        await Verify(profile);
    }
}