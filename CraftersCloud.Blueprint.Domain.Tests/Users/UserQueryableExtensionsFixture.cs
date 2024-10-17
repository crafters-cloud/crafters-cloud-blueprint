using CraftersCloud.Blueprint.Domain.Authorization;
using CraftersCloud.Blueprint.Domain.Companies;
using CraftersCloud.Blueprint.Domain.Tests.Companies;
using CraftersCloud.Blueprint.Domain.Users;
using FluentAssertions;

namespace CraftersCloud.Blueprint.Domain.Tests.Users;

[Category("unit")]
public class UserQueryableExtensionsFixture
{
    private IQueryable<User> _query = null!;
    private User _user = null!;
    private User _user2 = null!;
    private Company _company = null!;
    private static readonly Guid _companyId1 = new Guid("DF37404C-45ED-4321-812D-2DC4B8507976");

    [SetUp]
    public void Setup()
    {
        
        _company = new CompanyBuilder()
            .WithName("company1");
        _user = new UserBuilder()
            .WithEmailAddress("emailAddress1")
            .WithFullName("name")
            .WithRoleId(Role.SystemAdminRoleId)
            .WithCompany(_company.Id);
        _user2 = new UserBuilder()
            .WithEmailAddress("emailAddress2")
            .WithFullName("name2")
            .WithRoleId(Role.SystemAdminRoleId)
            .WithCompany(_companyId1);

        _query = new List<User> { _user, _user2 }.AsQueryable();
    }

    [Test]
    public void TestQueryEmptyList()
    {
        var result = new List<User>().AsQueryable().QueryByEmailAddress("some").ToList();
        result.Should().BeEmpty();
    }

    [TestCase("emailAddress1", 1, TestName = "Case sensitive-should find")]
    [TestCase("emailAddress2", 1, TestName = "Case sensitive-should find, v2")]
    [TestCase("EmailAddress1", 0, TestName = "Case sensitive-should not find")]
    [TestCase("EmailAddress2", 0, TestName = "Case sensitive-should not find, v2")]
    [TestCase("xyz", 0, TestName = "Should not find")]
    public void TestQueryByEmailAddress(string emailAddress, int expectedCount)
    {
        //change to use expectedCount instead of Verify
        var result = _query.QueryByEmailAddress(emailAddress).ToList();

        result.Count.Should().Be(expectedCount);
    }

    [TestCaseSource(nameof(CompanyIds))]
    public void TestQueryByCompanyId(Guid companyId, int expectedCount)
    {
        var result = _query.QueryByCompanyId(companyId).ToList();
        result.Count.Should().Be(expectedCount);
    }

    private static IEnumerable<TestCaseData> CompanyIds()
    {
        yield return new TestCaseData(_companyId1, 1) { TestName = "Expected CompanyId"};
        yield return new TestCaseData(new Guid("118F966E-2605-4074-A80A-7EA2E61DFDC6"), 0) { TestName = "Non-Expected CompanyId" };
    }
}