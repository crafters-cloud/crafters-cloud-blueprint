using CraftersCloud.Blueprint.Domain.Auditing;

namespace CraftersCloud.Blueprint.Domain.Companies.DomainEvents;

public record CompanyUpdatedDomainEvent(string Name) : AuditableDomainEvent("CompanyUpdated")
{
    public override object AuditPayload => new { Name };
}
