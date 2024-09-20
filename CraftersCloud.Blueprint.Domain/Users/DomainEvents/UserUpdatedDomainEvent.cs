using CraftersCloud.Blueprint.Domain.Auditing;

namespace CraftersCloud.Blueprint.Domain.Users.DomainEvents;

public record UserUpdatedDomainEvent(string EmailAddress) : AuditableDomainEvent("UserUpdated")
{
    public override object AuditPayload => new { EmailAddress };
}