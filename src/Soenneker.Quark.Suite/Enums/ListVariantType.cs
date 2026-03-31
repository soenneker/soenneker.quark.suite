using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Represents list variant types for Tailwind list styling.
/// </summary>
[EnumValue<string>]
public sealed partial class ListVariantType
{
    /// <summary>
    /// Removes default list styling.
    /// </summary>
    public static readonly ListVariantType None = new("none");

    /// <summary>
    /// Applies inline list layout utilities to the list.
    /// </summary>
    public static readonly ListVariantType Inline = new("inline");

    /// <summary>
    /// For items within an inline list.
    /// </summary>
    public static readonly ListVariantType InlineItem = new("inline-item");
}

