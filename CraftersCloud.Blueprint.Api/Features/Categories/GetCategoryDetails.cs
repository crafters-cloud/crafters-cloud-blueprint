using AutoMapper;
using CraftersCloud.Blueprint.Domain.Categories;
using CraftersCloud.Core.Data;
using CraftersCloud.Core.Entities;
using CraftersCloud.Core.EntityFramework;
using JetBrains.Annotations;
using MediatR;

namespace CraftersCloud.Blueprint.Api.Features.Categories;

public class GetCategoryDetails
{
    [PublicAPI]
    public class Request : IRequest<Response>
    {
        public Guid Id { get; set; }
        public static Request ById(Guid id) => new() { Id = id };
    }

    [PublicAPI]
    public class Response
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
    }

    [UsedImplicitly]
    public class MappingProfile : Profile
    {
        public MappingProfile() => CreateMap<Category, Response>();
    }

    [UsedImplicitly]
    public class RequestHandler(IRepository<Category> repository, IMapper mapper)
        : IRequestHandler<Request, Response>
    { 
        public async Task<Response> Handle(Request request, CancellationToken cancellationToken) => 
            await repository.QueryAll().QueryById(request.Id)
            .SingleOrDefaultMappedAsync<Category, Response>(mapper, cancellationToken); 
    }
}
