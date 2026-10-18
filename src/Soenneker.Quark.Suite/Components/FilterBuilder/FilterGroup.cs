using System;
using System.Collections.Generic;
namespace Soenneker.Quark;
/// <summary>A serializable nested group. Empty groups match all records; updates should replace collections.</summary>
public sealed record FilterGroup
{
    public string Id { get; init; } = Guid.NewGuid().ToString("N");
    public FilterJunction Junction { get; init; }
    public IReadOnlyList<FilterRule> Rules { get; init; } = [];
    public IReadOnlyList<FilterGroup> Groups { get; init; } = [];
}
