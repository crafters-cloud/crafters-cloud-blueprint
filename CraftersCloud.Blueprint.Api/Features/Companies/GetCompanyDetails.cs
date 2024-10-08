using AutoMapper;
using CraftersCloud.Blueprint.Domain.Companies;
using CraftersCloud.Core.Data;
using CraftersCloud.Core.Entities;
using CraftersCloud.Core.EntityFramework;
using JetBrains.Annotations;
using MediatR;

namespace CraftersCloud.Blueprint.Api.Features.Companies;

public static class GetCompanyDetails
{
    [PublicAPI]
    public class Request : IRequest<Response>
    {
        public Guid Id { get; set; }
        public static Request ById(Guid id) => new Request { Id = id };
    }

    [PublicAPI]
    public class Response
    {
        public Guid Id { get; set;}
        public string? Name { get; set;}
    }

    [UsedImplicitly]
    public class MappingProfile : Profile
    {
        public MappingProfile() => CreateMap<Company, Response>();
    }

    [UsedImplicitly]
    public class RequestHandler(IRepository<Company> repository, IMapper mapper) : IRequestHandler<Request, Response>
    {
        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            var response = await repository.QueryAll()
                .QueryById(request.Id)
                .SingleOrDefaultMappedAsync<Company, Response>(mapper, cancellationToken);
            return response;
        }
    }

}
