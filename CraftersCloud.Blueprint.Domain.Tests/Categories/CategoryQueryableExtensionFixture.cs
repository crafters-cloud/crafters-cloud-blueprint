using CraftersCloud.Blueprint.Domain.Categories;
using FluentAssertions;

namespace CraftersCloud.Blueprint.Domain.Tests.Categories;

[Category("unit")]
public class CategoryQueryableExtensionFixture
{
    private IQueryable<Category> _query = null!;
    private Category _category1 = null!;
    private Category _category2 = null!;

    [SetUp]
    public void SetUp()
    {
        _category1 = new CategoryBuilder().WithName("Category1");
        _category2 = new CategoryBuilder().WithName("Category2");

        _query = new List<Category> { _category1, _category2 }.AsQueryable();
    }

    [Test]
    public void TestQueryEmptyList()
    {
        var result = new List<Category>().AsQueryable().QueryByName("name").ToList();
        result.Should().BeEmpty();
    }

    [TestCase("Category1", 1, TestName = "Should find v1")]
    [TestCase("Category2", 1, TestName = "Should find v2")]
    [TestCase("Category", 2, TestName = "Should find 2")]
    [TestCase("Category3", 0, TestName = "Should not find")]
    public void TestQueryByName(string name, int expectedCount)
    {
        var result = _query.QueryByName(name).ToList();
        result.Count.Should().Be(expectedCount);
    }

}

