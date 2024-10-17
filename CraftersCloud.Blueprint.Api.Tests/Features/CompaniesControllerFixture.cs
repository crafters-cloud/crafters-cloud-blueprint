using CraftersCloud.Blueprint.Api.Features.Companies;
using CraftersCloud.Blueprint.Api.Tests.Infrastructure.Api;
using CraftersCloud.Blueprint.Domain.Authorization;
using CraftersCloud.Blueprint.Domain.Companies;
using CraftersCloud.Blueprint.Domain.Companies.Commands;
using CraftersCloud.Blueprint.Domain.Tests.Companies;
using CraftersCloud.Blueprint.Domain.Tests.Users;
using CraftersCloud.Blueprint.Domain.Users;
using CraftersCloud.Core.AspNetCore.Tests.SystemTextJson.Http;
using CraftersCloud.Core.Paging;

namespace CraftersCloud.Blueprint.Api.Tests.Features;

[Category("integration")]
public class CompaniesControllerFixture : IntegrationFixtureBase
{
    private Company _company1 = null!;
    private Company _company2 = null!;
    private User _user = null!; 

    [SetUp]
    public void SetUp()
    {
        _company1 = new CompanyBuilder().WithName("Company Name 1");
        _company2 = new CompanyBuilder().WithName("Company Name 2");
        _user = new UserBuilder()
            .WithEmailAddress("john_doe@john.doe")
            .WithFullName("John Doe")
            .WithCompany(_company1.Id)
            .WithRoleId(Role.SystemAdminRoleId)
            .WithStatusId(UserStatusId.Active);
        AddAndSaveChanges(_company1, _company2, _user);
    }

    [Test]
    public async Task TestGetAll()
    {
        var companies = (await Client.GetAsync<PagedResponse<GetCompanies.Response.Item>>(
                new Uri("api/companies", UriKind.RelativeOrAbsolute),
                new KeyValuePair<string, string>("Sort by", "Name")))
            ?.Items.ToList();
        await Verify(companies);
    }

    [Test]
    public async Task TestGetById()
    {
        var company = await Client.GetAsync<GetCompanyDetails.Response>($"api/companies/{_company1.Id}");
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
            Id = _company1.Id,
            Name = "Company  Name"
        };

        var company =
            await Client.PostAsync<CreateOrUpdateCompany.Command, GetCompanyDetails.Response>("api/companies", command);
        await Verify(company);
    }

    [Test]
    public async Task TestShouldNotRemove()
    {
        await Client.DeleteAsync($"api/companies/{_company1.Id}");
        var companies = (await Client.GetAsync<PagedResponse<GetCompanies.Response.Item>>(
                new Uri("api/companies", UriKind.RelativeOrAbsolute),
                new KeyValuePair<string, string>("Sort by", "Name")))
            ?.Items.ToList();
        await Verify(companies);
    }

    [Test]
    public async Task TestShouldRemove()
    {
        await Client.DeleteAsync($"api/companies/{_company2.Id}");
        var companies = (await Client.GetAsync<PagedResponse<GetCompanies.Response.Item>>(
                new Uri("api/companies", UriKind.RelativeOrAbsolute),
                new KeyValuePair<string, string>("Sort by", "Name")))
            ?.Items.ToList();
        await Verify(companies);
    }

}