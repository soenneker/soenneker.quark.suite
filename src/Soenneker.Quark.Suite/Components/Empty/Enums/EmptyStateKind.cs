using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Supported empty state kind values.
/// </summary>
[EnumValue]
public sealed partial class EmptyStateKind
{
    /// <summary>Empty presentation.</summary>
    public static readonly EmptyStateKind Empty = new(0);
    /// <summary>Error presentation.</summary>
    public static readonly EmptyStateKind Error = new(1);
    /// <summary>Disabled presentation.</summary>
    public static readonly EmptyStateKind Disabled = new(2);
}
