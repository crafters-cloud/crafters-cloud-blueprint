using AutoMapper;
using AutoMapper.QueryableExtensions;
using CraftersCloud.Blueprint.Domain.Companies;
using CraftersCloud.Core.Data;
using CraftersCloud.Core.EntityFramework;
using CraftersCloud.Core.Paging;
using JetBrains.Annotations;

namespace CraftersCloud.Blueprint.Api.Features.Companies;

public static class GetCompanies
{
    [PublicAPI]
    public class Request : PagedRequest<Response.Item>
    {
        public string? Name { get; set; }
    }

    [PublicAPI]
    public static class Response
    {
        [PublicAPI]
        public class Item
        {
            public Guid Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public DateTimeOffset CreatedOn { get; set; }
            public DateTimeOffset UpdatedOn { get; set; }
        }

        [UsedImplicitly]
        public class MappingProfile : Profile
        {
            public MappingProfile() => CreateMap<Company, Item>();
        }
    }

    [UsedImplicitly]
    public class RequestHandler(IRepository<Company> repository, IMapper mapper)
        : IPagedRequestHandler<Request, Response.Item>
    {
        public async Task<PagedResponse<Response.Item>> Handle(Request request, CancellationToken cancellationToken) =>
            await repository.QueryAll()
            .QueryByName(request.Name)
            .ProjectTo<Response.Item>(mapper.ConfigurationProvider, cancellationToken)
            .ToPagedResponseAsync(request, cancellationToken);
    }
}
