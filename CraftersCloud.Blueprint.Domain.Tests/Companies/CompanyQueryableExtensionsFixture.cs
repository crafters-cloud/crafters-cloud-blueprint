using CraftersCloud.Blueprint.Domain.Companies;
using FluentAssertions;

namespace CraftersCloud.Blueprint.Domain.Tests.Companies;

[Category("unit")]
public class CompanyQueryableExtensionsFixture
{
    private IQueryable<Company> _query = null!;
    private Company _company1 = null!;
    private Company _company2 = null!;

    [SetUp]
    public void Setup()
    {
        _company1 = new CompanyBuilder().WithName("name1");
        _company2 = new CompanyBuilder().WithName("name2");

        _query = new List<Company> { _company1, _company2 }.AsQueryable();
    }

    [Test]
    public void TestQueryEmptyList()
    {
        var result = new List<Company>().AsQueryable().QueryByName("someName").ToList();
        result.Should().BeEmpty();
    }

    [TestCase("name1", 1, TestName = "Should find v1")]
    [TestCase("name2", 1, TestName = "Should find v2")]
    [TestCase("name", 2, TestName = "Should find 2")]
    [TestCase("name3", 0, TestName = "Should not find")]
    public void TestQueryByName(string name, int expectedCount)
    {
        var result = _query.QueryByName(name).ToList();
        result.Count.Should().Be(expectedCount);
    }

}
