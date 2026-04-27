using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Defines visual variants for <see cref="EmptyMedia"/>.
/// </summary>
[EnumValue]
public sealed partial class EmptyMediaVariant
{
    /// <summary>
    /// Transparent media container.
    /// </summary>
    public static readonly EmptyMediaVariant Default = new(0);

    /// <summary>
    /// Rounded icon container with muted background.
    /// </summary>
    public static readonly EmptyMediaVariant Icon = new(1);
}
