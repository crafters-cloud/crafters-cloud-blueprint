using CraftersCloud.Blueprint.Domain.Authorization;
using CraftersCloud.Blueprint.Domain.Users;
using FluentAssertions;

namespace CraftersCloud.Blueprint.Domain.Tests.Users;

[Category("unit")]
public class UserQueryableExtensionsFixture
{
    private IQueryable<User> _query = null!;
    private User _user = null!;
    private User _user2 = null!;

    [Before(Test)]
    public void Setup()
    {
        _user = new UserBuilder()
            .WithEmailAddress("emailAddress1")
            .WithFullName("name")
            .WithRoleId(Role.SystemAdminRoleId);
        _user2 = new UserBuilder()
            .WithEmailAddress("emailAddress2")
            .WithFullName("name2")
            .WithRoleId(Role.SystemAdminRoleId);

        _query = new List<User> { _user, _user2 }.AsQueryable();
    }

    [Test]
    public void TestQueryEmptyList()
    {
        var result = new List<User>().AsQueryable().QueryByEmailAddress("some").ToList();
        result.Should().BeEmpty();
    }

    [Test]
    [Arguments("emailAddress1", 1)]
    [Arguments("emailAddress2", 1)]
    [Arguments("EmailAddress1", 0)]
    [Arguments("EmailAddress2", 0)]
    [Arguments("xyz", 0)]
    public void TestQueryByEmailAddress(string emailAddress, int expectedCount)
    {
        //change to use expectedCount instead of Verify
        var result = _query.QueryByEmailAddress(emailAddress).ToList();

        result.Count.Should().Be(expectedCount);
    }
}