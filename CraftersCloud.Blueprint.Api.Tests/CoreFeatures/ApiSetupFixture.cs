using System.Net.Http.Json;
using AutoMapper;
using CraftersCloud.Blueprint.Api.Features.Users;
using CraftersCloud.Blueprint.Api.Tests.Infrastructure.Api;
using CraftersCloud.Blueprint.Domain.Authorization;
using CraftersCloud.Blueprint.Domain.Tests.Users;
using CraftersCloud.Blueprint.Domain.Users;
using CraftersCloud.Blueprint.Domain.Users.Commands;
using CraftersCloud.Core.AspNetCore.Tests.SystemTextJson.Http;
using CraftersCloud.Core.Paging;
using FluentAssertions;

namespace CraftersCloud.Blueprint.Api.Tests.CoreFeatures;

// Fixture that validates if Api project has been setup correctly
// Uses arbitrary controller (in this case UserController) and verifies if basic operations, e.g. get, post, validations are correctly setup.  
[Category("integration")]
[NotInParallel]
public class ApiSetupFixture : IntegrationFixtureBase
{
    private User _user = null!;

    [Before(Test)]
    public async Task SetUp()
    {
        _user = new UserBuilder()
            .WithEmailAddress("john_doe@john.doe")
            .WithFullName("John Doe")
            .WithRoleId(Role.SystemAdminRoleId)
            .WithStatusId(UserStatusId.Active);

        await AddAndSaveChangesAsync(_user);
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
    public async Task GivenValidUserId_GetById_ReturnsUserDetails()
    {
        var user = await Client.GetAsync<GetUserDetails.Response>($"api/users/{_user.Id}");

        await Verify(user);
    }

    [Test]
    public async Task GivenNonExistingUserId_GetById_ReturnsNotFound()
    {
        var response = await Client.GetAsync($"api/users/{Guid.NewGuid()}");

        response.Should().BeNotFound();
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
            UserStatusId = UserStatusId.Inactive
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
            FullName = "some user",
            EmailAddress = "someuser@test.com",
            RoleId = Role.SystemAdminRoleId,
            UserStatusId = UserStatusId.Inactive
        };
        var user =
            await Client.PostAsync<CreateOrUpdateUser.Command, GetUserDetails.Response>("api/users", command);

        await Verify(user);
    }

    [Test]
    [Arguments("some user", "invalid email", "emailAddress", "is not a valid email address.")]
    [Arguments("", "someuser@test.com", "fullName", "must not be empty.")]
    [Arguments("some user", "", "emailAddress", "must not be empty.")]
    [Arguments("John Doe", "john_doe@john.doe", "emailAddress", "EmailAddress is already taken")]
    public async Task TestCreateReturnsValidationErrors(string name,
        string emailAddress,
        string validationField,
        string validationErrorMessage)
    {
        var command = new CreateOrUpdateUser.Command
        {
            Id = null,
            FullName = name,
            EmailAddress = emailAddress,
            RoleId = Role.SystemAdminRoleId,
            UserStatusId = UserStatusId.Inactive
        };
        var response = await Client.PostAsJsonAsync("api/users", command, HttpSerializationOptions.Options);

        response.Should().BeBadRequest().And.ContainValidationError(validationField, validationErrorMessage);
    }

    [Test]
    public void TestAutoMapperMappings()
    {
        var mapper = Resolve<IMapper>();
        mapper.ConfigurationProvider.AssertConfigurationIsValid();
    }
}