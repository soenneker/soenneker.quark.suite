using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Represents list variant types for Tailwind list styling.
/// </summary>
[EnumValue<string>]
public sealed partial class ListVariantType
{
    /// <summary>
    /// Removes default list styling (list-none).
    /// </summary>
    public static readonly ListVariantType Unstyled = new("unstyled");

    /// <summary>
    /// Makes list items inline (inline-flex / inline).
    /// </summary>
    public static readonly ListVariantType Inline = new("inline");

    /// <summary>
    /// For items within an inline list.
    /// </summary>
    public static readonly ListVariantType InlineItem = new("inline-item");
}

