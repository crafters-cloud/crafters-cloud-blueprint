using CraftersCloud.Blueprint.Domain.Authorization;
using CraftersCloud.Blueprint.Domain.Companies.Commands;
using CraftersCloud.Blueprint.Infrastructure.Authorization;
using CraftersCloud.Core.AspNetCore;
using CraftersCloud.Core.Data;
using CraftersCloud.Core.Paging;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CraftersCloud.Blueprint.Api.Features.Companies;

[Route("api/[controller]")]
public class CompaniesController(IUnitOfWork unitOfWork, IMediator mediator) : Controller
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<PagedResponse<GetCompanies.Response.Item>>> Search(
        [FromQuery] GetCompanies.Request query)
    {
        var response = await mediator.Send(query);
        return response.ToActionResult();
    }

    [HttpGet]
    [Route("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [UserHasPermission(PermissionId.CompaniesRead)]
    public async Task<ActionResult<GetCompanyDetails.Response>> Get(Guid id)
    {
        var response = await mediator.Send(GetCompanyDetails.Request.ById(id));
        return response.ToActionResult();
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [UserHasPermission(PermissionId.CompaniesWrite)]
    public async Task<ActionResult<GetCompanyDetails.Response>> Post(CreateOrUpdateCompany.Command command)
    {
        var company = await mediator.Send(command);
        await unitOfWork.SaveChangesAsync();
        return await Get(company.Id);
    }

    [HttpDelete]
    [Route("{id:quid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [UserHasPermission(PermissionId.CompaniesDelete)]
    public async Task Remove(Guid id)
    {
        await mediator.Send(new RemoveCompany.Command { Id = id });
        await unitOfWork.SaveChangesAsync();
    }
}
