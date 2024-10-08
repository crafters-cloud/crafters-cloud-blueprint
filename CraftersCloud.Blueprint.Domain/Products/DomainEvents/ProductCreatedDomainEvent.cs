﻿using CraftersCloud.Blueprint.Domain.Auditing;

namespace CraftersCloud.Blueprint.Domain.Products.DomainEvents;

public record ProductCreatedDomainEvent : AuditableDomainEvent
{
    public ProductCreatedDomainEvent(Product product) : base("ProductCreated")
    {
        Name = product.Name;
        Code = product.Code;
    }

    public string Name { get; }
    public string Code { get; }
    public override object AuditPayload => new { Name, Code };
}