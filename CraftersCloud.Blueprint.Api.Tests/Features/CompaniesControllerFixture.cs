using CraftersCloud.Blueprint.Api.Features.Companies;
using CraftersCloud.Blueprint.Api.Tests.Infrastructure.Api;
using CraftersCloud.Blueprint.Domain.Companies;
using CraftersCloud.Blueprint.Domain.Companies.Commands;
using CraftersCloud.Blueprint.Domain.Tests.Companies;
using CraftersCloud.Core.AspNetCore.Tests.SystemTextJson.Http;
using CraftersCloud.Core.Paging;

namespace CraftersCloud.Blueprint.Api.Tests.Features;

[Category("integration")]

public class CompaniesControllerFixture : IntegrationFixtureBase
{
    private Company _company = null!;

    [SetUp]
    public void SetUp()
    {
        _company = new CompanyBuilder().WithName("Company Name");
        AddAndSaveChanges(_company);
    }

    [Test]
    public async Task TestGetAll()
    {
        var companies = (await Client.GetAsync<PagedResponse<GetCompanies.Response.Item>>(
            new Uri("api/companies", UriKind.RelativeOrAbsolute), new KeyValuePair<string, string>("Sort by", "Name")))
            ?.Items.ToList();
        await Verify(companies);
    }

    [Test]
    public async Task TestGetById()
    {
        var company = await Client.GetAsync<GetCompanyDetails.Response>($"api/companies/{_company.Id}");
        await Verify(company);
    }

    [Test]
    public async Task TestCreate()
    {
        var command = new CreateOrUpdateCompany.Command
        {
            Id = null,
            Name = "Company Name"
        };

        var company =
            await Client.PostAsync<CreateOrUpdateCompany.Command, GetCompanyDetails.Response>("api/companies", command);
        await Verify(company);
    }

    [Test]
    public async Task TestUpdate()
    {
        var command = new CreateOrUpdateCompany.Command
        {
            Id = _company.Id,
            Name = "Company  Name"
        };

        var company =
            await Client.PostAsync<CreateOrUpdateCompany.Command, GetCompanyDetails.Response>("api/companies", command);
        await Verify(company);
    }
}
