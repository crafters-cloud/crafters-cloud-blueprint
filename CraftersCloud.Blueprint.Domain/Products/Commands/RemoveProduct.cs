﻿using CraftersCloud.Core.Data;
using JetBrains.Annotations;
using MediatR;

namespace CraftersCloud.Blueprint.Domain.Products.Commands;

public static class RemoveProduct
{
    [PublicAPI]
    public class Command : IRequest
    {
        public Guid Id { get; set; }
    }

    [UsedImplicitly]
    public class RequestHandler : IRequestHandler<Command>
    {
        private readonly IRepository<Product> _repository;

        public RequestHandler(IRepository<Product> repository) => _repository = repository;

        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            var product = await _repository.FindByIdAsync(request.Id) ??
                          throw new InvalidOperationException("Product could not be found");

            _repository.Delete(product);
        }
    }
}