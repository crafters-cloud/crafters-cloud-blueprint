using CraftersCloud.Blueprint.Domain.Authorization;
using CraftersCloud.Blueprint.Domain.Categories.Commands;
using CraftersCloud.Blueprint.Infrastructure.Authorization;
using CraftersCloud.Core.AspNetCore;
using CraftersCloud.Core.Data;
using CraftersCloud.Core.Paging;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace CraftersCloud.Blueprint.Api.Features.Categories;


[Produces(MediaTypeNames.Application.Json)]
[Route("api/[controller]")]
public class CategoriesController(
    IUnitOfWork unitOfWork,
    IMediator mediator) : Controller
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [UserHasPermission(PermissionId.CategoriesRead)]
    public async Task<ActionResult<PagedResponse<GetCategories.Response.Item>>> Search([FromQuery]GetCategories.Request query)
    {
        var response = await mediator.Send(query);
        return response.ToActionResult();
    }

    [HttpGet]
    [Route("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [UserHasPermission(PermissionId.CategoriesRead)]
    public async Task<ActionResult<GetCategoryDetails.Response>> Get(Guid id)
    {
        var response = await mediator.Send(GetCategoryDetails.Request.ById(id));
        return response.ToActionResult();
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [UserHasPermission(PermissionId.CategoriesWrite)]
    public async Task<ActionResult<GetCategoryDetails.Response>> Post(CreateOrUpdateCategory.Command command)
    {
        var category = await mediator.Send(command);
        await unitOfWork.SaveChangesAsync();
        return await Get(category.Id);
    }
}
