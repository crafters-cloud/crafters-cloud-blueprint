using CraftersCloud.Blueprint.Domain.Auditing;

namespace CraftersCloud.Blueprint.Domain.Users.DomainEvents;

public record UserCreatedDomainEvent(string EmailAddress) : AuditableDomainEvent("UserCreated")
{
    public override object AuditPayload => new { EmailAddress };
}