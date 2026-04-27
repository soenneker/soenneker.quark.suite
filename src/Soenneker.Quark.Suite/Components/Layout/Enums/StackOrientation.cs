using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Determines whether a stack lays out items vertically or horizontally.
/// </summary>
[EnumValue]
public sealed partial class StackOrientation
{
    public static readonly StackOrientation Vertical = new(0);
    public static readonly StackOrientation Horizontal = new(1);
}
