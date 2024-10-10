using CraftersCloud.Blueprint.Domain.Auditing;

namespace CraftersCloud.Blueprint.Domain.Categories.DomainEvents;

public record CategoryUpdatedDomainEvent (string Name) : AuditableDomainEvent("UserUpdated")
{
    public override object AuditPayload => new { Name }; 
}
