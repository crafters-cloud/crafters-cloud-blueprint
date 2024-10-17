using CraftersCloud.Blueprint.Api.Features.Categories;
using CraftersCloud.Blueprint.Api.Tests.Infrastructure.Api;
using CraftersCloud.Blueprint.Domain.Categories;
using CraftersCloud.Blueprint.Domain.Categories.Commands;
using CraftersCloud.Blueprint.Domain.Tests.Categories;
using CraftersCloud.Core.AspNetCore.Tests.SystemTextJson.Http;
using CraftersCloud.Core.Paging;

namespace CraftersCloud.Blueprint.Api.Tests.Features;

[Category("integration")]
public class CategoriesControllerFixture : IntegrationFixtureBase
{
    private Category _category1 = null!;
    private Category _category2 = null!;

    [SetUp]
    public void SetUp()
    {
        _category1 = new CategoryBuilder().WithName("Category1");
        _category2 = new CategoryBuilder().WithName("Category2");
        AddAndSaveChanges(_category1, _category2);
    }

    [Test]
    public async Task TestGetAll()
    {
        var categories = (await Client.GetAsync<PagedResponse<GetCategories.Response.Item>>(
            new Uri("api/categories", UriKind.RelativeOrAbsolute),
            new KeyValuePair<string, string>("Sort by", "Name")))
            ?.Items.ToList();
        await Verify(categories);
    }

    [Test]
    public async Task TestGetById()
    {
        var category = await Client.GetAsync<GetCategoryDetails.Response>($"api/categories/{_category1.Id}");
        await Verify(category);
    }

    [Test]
    public async Task TestCreate()
    {
        var command = new CreateOrUpdateCategory.Command()
        {
            Id = null,
            Name = "Category Name"
        };
        var category =
            await Client.PostAsync<CreateOrUpdateCategory.Command, GetCategoryDetails.Response>("api/categories", command);
        await Verify(category);
    }

    [Test]
    public async Task TestUpdate()
    {
        var command = new CreateOrUpdateCategory.Command()
        {
            Id = _category1.Id,
            Name = "Category Name"
        };
        var category =
            await Client.PostAsync<CreateOrUpdateCategory.Command, GetCategoryDetails.Response>("api/categories", command);
        await Verify(category);
    }
}