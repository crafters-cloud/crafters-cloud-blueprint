using CraftersCloud.Blueprint.Api.Features;
using CraftersCloud.Blueprint.Api.Features.Users;
using CraftersCloud.Blueprint.Api.Tests.Infrastructure.Api;
using CraftersCloud.Blueprint.Domain.Authorization;
using CraftersCloud.Blueprint.Domain.Companies;
using CraftersCloud.Blueprint.Domain.Tests.Companies;
using CraftersCloud.Blueprint.Domain.Tests.Users;
using CraftersCloud.Blueprint.Domain.Users;
using CraftersCloud.Blueprint.Domain.Users.Commands;
using CraftersCloud.Core.AspNetCore.Tests.SystemTextJson.Http;
using CraftersCloud.Core.Paging;

namespace CraftersCloud.Blueprint.Api.Tests.Features;

[Category("integration")]

// Basic example of an integration test.
// For every public api method add appropriate test.
// Integration tests should be used for happy flows.
// For un-happy flows (e.g. edge cases), or complex business rules
// write unit tests.
public class UsersControllerFixture : IntegrationFixtureBase
{
    private User _user = null!;
    private Company _company = null!;

    [SetUp]
    public void SetUp()
    {
        _company = new CompanyBuilder().WithName("FirstCompany").Build();

        _user = new UserBuilder()
            .WithEmailAddress("john_doe@john.doe")
            .WithFullName("John Doe")
            .WithCompany(_company.Id)
            .WithRoleId(Role.SystemAdminRoleId)
            .WithStatusId(UserStatusId.Active);
        
        AddAndSaveChanges(_user, _company);
    }

    [Test]
    public async Task TestGetAll()
    {
        var users = (await Client.GetAsync<PagedResponse<GetUsers.Response.Item>>(
                new Uri("api/users", UriKind.RelativeOrAbsolute), new KeyValuePair<string, string>("SortBy", "EmailAddress")))
            ?.Items.ToList()!;

        await Verify(users);
    }

    [Test]
    public async Task TestGetById()
    {
        var user = await Client.GetAsync<GetUserDetails.Response>($"api/users/{_user.Id}");

        await Verify(user);
    }

    [Test]
    public async Task TestGetRoles()
    {
        var user = await Client.GetAsync<IEnumerable<LookupResponse<Guid>>>($"api/users/roles");

        await Verify(user);
    }

    [Test]
    public async Task TestGetStatuses()
    {
        var user = await Client.GetAsync<IEnumerable<LookupResponse<UserStatusId>>>($"api/users/statuses");

        await Verify(user);
    }

    [Test]
    public async Task TestCreate()
    {
        var command = new CreateOrUpdateUser.Command
        {
            Id = null,
            FullName = "some user",
            EmailAddress = "someuser@test.com",
            RoleId = Role.SystemAdminRoleId,
            UserStatusId = UserStatusId.Active,
            CompanyName = "another company name"
        };
        var user =
            await Client.PostAsync<CreateOrUpdateUser.Command, GetUserDetails.Response>("api/users", command);

        await Verify(user);
    }

    [Test]
    public async Task TestUpdate()
    {
        var command = new CreateOrUpdateUser.Command
        {
            Id = _user.Id,
            FullName = "some other user",
            EmailAddress = "someuser@test.com",
            RoleId = Role.SystemAdminRoleId,
            UserStatusId = UserStatusId.Inactive,
            CompanyName = "Second Company"
        };
        var user =
            await Client.PostAsync<CreateOrUpdateUser.Command, GetUserDetails.Response>("api/users", command);

        await Verify(user);
    }
}