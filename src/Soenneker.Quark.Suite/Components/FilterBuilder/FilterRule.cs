using System;
namespace Soenneker.Quark;
/// <summary>A serializable predicate. Values use invariant numbers and ISO dates; identifiers are stable across edits.</summary>
public sealed record FilterRule
{
    public string Id { get; init; } = Guid.NewGuid().ToString("N");
    public string Field { get; init; } = "";
    public FilterOperator Operator { get; init; }
    public string? Value { get; init; }
}
