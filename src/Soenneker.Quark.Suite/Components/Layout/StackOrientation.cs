using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Determines whether a stack lays out items vertically or horizontally.
/// </summary>
[EnumValue<string>]
public sealed partial class StackOrientation
{
    public static readonly StackOrientation Vertical = new("vertical");
    public static readonly StackOrientation Horizontal = new("horizontal");
}
