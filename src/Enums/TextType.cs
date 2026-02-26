using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark.Enums;

/// <summary>
/// Represents the type of text utility.
/// </summary>
[EnumValue<string>]
public sealed partial class TextType
{
    /// <summary>
    /// Align text to the start (left in LTR, right in RTL).
    /// </summary>
    public static readonly TextType Start = new("start");

    /// <summary>
    /// Center-align text.
    /// </summary>
    public static readonly TextType Center = new("center");

    /// <summary>
    /// Align text to the end (right in LTR, left in RTL).
    /// </summary>
    public static readonly TextType End = new("end");

    /// <summary>
    /// Justify text alignment.
    /// </summary>
    public static readonly TextType Justify = new("justify");

    /// <summary>
    /// Allow text wrapping.
    /// </summary>
    public static readonly TextType Wrap = new("wrap");

    /// <summary>
    /// Prevent text wrapping.
    /// </summary>
    public static readonly TextType Nowrap = new("nowrap");

    /// <summary>
    /// Truncate text with ellipsis.
    /// </summary>
    public static readonly TextType Truncate = new("truncate");

    /// <summary>
    /// Transform text to lowercase.
    /// </summary>
    public static readonly TextType Lowercase = new("lowercase");

    /// <summary>
    /// Transform text to uppercase.
    /// </summary>
    public static readonly TextType Uppercase = new("uppercase");

    /// <summary>
    /// Capitalize first letter of each word.
    /// </summary>
    public static readonly TextType Capitalize = new("capitalize");

    /// <summary>
    /// Reset text styling.
    /// </summary>
    public static readonly TextType Reset = new("reset");

    /// <summary>
    /// Muted/secondary text color.
    /// </summary>
    public static readonly TextType Muted = new("muted");

    /// <summary>
    /// Emphasized text styling.
    /// </summary>
    public static readonly TextType Emphasis = new("emphasis");
}
