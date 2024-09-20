﻿using CraftersCloud.Core.Entities;

namespace CraftersCloud.Blueprint.Domain.Auditing;

public abstract record AuditableDomainEvent(string EventName) : DomainEvent
{
    public abstract object AuditPayload { get; }
}