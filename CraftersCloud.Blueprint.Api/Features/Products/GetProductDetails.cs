using AutoMapper;
using CraftersCloud.Blueprint.Domain.Products;
using CraftersCloud.Core.Data;
using CraftersCloud.Core.Entities;
using CraftersCloud.Core.EntityFramework;
using JetBrains.Annotations;
using MediatR;

namespace CraftersCloud.Blueprint.Api.Features.Products;

public static class GetProductDetails
{
    [PublicAPI]
    public class Request : IRequest<Response>
    {
        public Guid Id { get; set; }
    }

    [PublicAPI]
    public class Response
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public ProductType Type { get; set; }
        public string Description { get; set; } = string.Empty;
        public double Price { get; set; }
        public int Amount { get; set; }
        public string ContactEmail { get; set; } = string.Empty;
        public string ContactPhone { get; set; } = string.Empty;
        public string InfoLink { get; set; } = string.Empty;
        public string AdditionalInfo { get; set; } = string.Empty;
        public DateOnly? ExpiresOn { get; set; }
        public bool HasDiscount { get; set; }
        public float Discount { get; set; }
        public bool FreeShipping { get; set; }
    }

    [UsedImplicitly]
    public class MappingProfile : Profile
    {
        public MappingProfile() => CreateMap<Product, Response>().ForMember(x => x.AdditionalInfo, opt => opt.Ignore());
    }

    [UsedImplicitly]
    public class RequestHandler : IRequestHandler<Request, Response>
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IMapper _mapper;

        public RequestHandler(IRepository<Product> productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            var response = await _productRepository.QueryAll()
                .QueryById(request.Id)
                .SingleOrDefaultMappedAsync<Product, Response>(_mapper, cancellationToken);
            return response;
        }
    }
}