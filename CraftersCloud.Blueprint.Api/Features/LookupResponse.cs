﻿using JetBrains.Annotations;

namespace CraftersCloud.Blueprint.Api.Features;

[PublicAPI]
public class LookupResponse<T>
{
    public required T Value { get; set; }
    public string Label { get; set; } = string.Empty;
}