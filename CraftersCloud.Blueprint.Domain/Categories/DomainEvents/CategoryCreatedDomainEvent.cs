
using CraftersCloud.Blueprint.Domain.Auditing;

namespace CraftersCloud.Blueprint.Domain.Categories.DomainEvents;

public record CategoryCreatedDomainEvent(string Name) : AuditableDomainEvent("CategoryCreated")
{
    public override object AuditPayload => new { Name };
}
