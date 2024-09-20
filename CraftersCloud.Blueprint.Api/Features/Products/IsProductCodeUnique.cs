﻿using CraftersCloud.Blueprint.Domain.Products;
using CraftersCloud.Core.Data;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CraftersCloud.Blueprint.Api.Features.Products;

public static class IsProductCodeUnique
{
    [PublicAPI]
    public class Request : IRequest<Response>
    {
        public Guid? Id { get; set; }
        public string Code { get; set; } = string.Empty;
    }

    [PublicAPI]
    public class Response
    {
        public bool IsUnique { get; set; }
    }

    [UsedImplicitly]
    public class RequestHandler : IRequestHandler<Request, Response>
    {
        private readonly IRepository<Product> _productRepository;

        public RequestHandler(IRepository<Product> productRepository) => _productRepository = productRepository;

        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            var isUnique = !await _productRepository.QueryAll()
                .Where(x => x.Id != request.Id)
                .AnyAsync(x => x.Code == request.Code, cancellationToken);
            return new Response { IsUnique = isUnique };
        }
    }
}