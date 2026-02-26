using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark.Enums;

/// <summary>
/// Represents CSS list-style-type values for ordered and unordered lists.
/// </summary>
[EnumValue<string>]
public sealed partial class ListStyleTypeValue
{
    /// <summary>
    /// Default marker type (browser default)
    /// </summary>
    public static readonly ListStyleTypeValue None = new("none");

    /// <summary>
    /// Filled circle (default for ul)
    /// </summary>
    public static readonly ListStyleTypeValue Disc = new("disc");

    /// <summary>
    /// Hollow circle
    /// </summary>
    public static readonly ListStyleTypeValue Circle = new("circle");

    /// <summary>
    /// Filled square
    /// </summary>
    public static readonly ListStyleTypeValue Square = new("square");

    /// <summary>
    /// Decimal numbers (default for ol): 1, 2, 3
    /// </summary>
    public static readonly ListStyleTypeValue Decimal = new("decimal");

    /// <summary>
    /// Decimal numbers with leading zeros: 01, 02, 03
    /// </summary>
    public static readonly ListStyleTypeValue DecimalLeadingZero = new("decimal-leading-zero");

    /// <summary>
    /// Lowercase letters: a, b, c
    /// </summary>
    public static readonly ListStyleTypeValue LowerAlpha = new("lower-alpha");

    /// <summary>
    /// Uppercase letters: A, B, C
    /// </summary>
    public static readonly ListStyleTypeValue UpperAlpha = new("upper-alpha");

    /// <summary>
    /// Lowercase Roman numerals: i, ii, iii
    /// </summary>
    public static readonly ListStyleTypeValue LowerRoman = new("lower-roman");

    /// <summary>
    /// Uppercase Roman numerals: I, II, III
    /// </summary>
    public static readonly ListStyleTypeValue UpperRoman = new("upper-roman");

    /// <summary>
    /// Lowercase Greek letters
    /// </summary>
    public static readonly ListStyleTypeValue LowerGreek = new("lower-greek");

    /// <summary>
    /// Armenian numbering
    /// </summary>
    public static readonly ListStyleTypeValue Armenian = new("armenian");

    /// <summary>
    /// Georgian numbering
    /// </summary>
    public static readonly ListStyleTypeValue Georgian = new("georgian");

    /// <summary>
    /// Inherits from parent
    /// </summary>
    public static readonly ListStyleTypeValue Inherit = new("inherit");

    /// <summary>
    /// Initial value
    /// </summary>
    public static readonly ListStyleTypeValue Initial = new("initial");

    /// <summary>
    /// Unsets the value
    /// </summary>
    public static readonly ListStyleTypeValue Unset = new("unset");
}

