using CraftersCloud.Blueprint.Domain.Auditing;

namespace CraftersCloud.Blueprint.Domain.Companies.DomainEvents;

public record CompanyCreatedDomainEvent(string Name) : AuditableDomainEvent("CompanyCreated")
{
    public override object AuditPayload => new { Name };
}
