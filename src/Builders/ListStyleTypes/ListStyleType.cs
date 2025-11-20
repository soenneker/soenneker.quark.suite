using Soenneker.Quark.Enums;


namespace Soenneker.Quark;

/// <summary>
/// CSS list-style-type property builder with fluent API.
/// </summary>
public static class ListStyleType
{
    /// <summary>
    /// Gets a list style type builder with none value (no marker).
    /// </summary>
    public static ListStyleTypeBuilder None => new(ListStyleTypeValue.None);
    /// <summary>
    /// Gets a list style type builder with disc value (filled circle marker).
    /// </summary>
    public static ListStyleTypeBuilder Disc => new(ListStyleTypeValue.Disc);
    /// <summary>
    /// Gets a list style type builder with circle value (hollow circle marker).
    /// </summary>
    public static ListStyleTypeBuilder Circle => new(ListStyleTypeValue.Circle);
    /// <summary>
    /// Gets a list style type builder with square value (filled square marker).
    /// </summary>
    public static ListStyleTypeBuilder Square => new(ListStyleTypeValue.Square);
    /// <summary>
    /// Gets a list style type builder with decimal value (decimal numbers).
    /// </summary>
    public static ListStyleTypeBuilder Decimal => new(ListStyleTypeValue.Decimal);
    /// <summary>
    /// Gets a list style type builder with decimal-leading-zero value (zero-padded decimal numbers).
    /// </summary>
    public static ListStyleTypeBuilder DecimalLeadingZero => new(ListStyleTypeValue.DecimalLeadingZero);
    /// <summary>
    /// Gets a list style type builder with lower-alpha value (lowercase letters).
    /// </summary>
    public static ListStyleTypeBuilder LowerAlpha => new(ListStyleTypeValue.LowerAlpha);
    /// <summary>
    /// Gets a list style type builder with upper-alpha value (uppercase letters).
    /// </summary>
    public static ListStyleTypeBuilder UpperAlpha => new(ListStyleTypeValue.UpperAlpha);
    /// <summary>
    /// Gets a list style type builder with lower-roman value (lowercase roman numerals).
    /// </summary>
    public static ListStyleTypeBuilder LowerRoman => new(ListStyleTypeValue.LowerRoman);
    /// <summary>
    /// Gets a list style type builder with upper-roman value (uppercase roman numerals).
    /// </summary>
    public static ListStyleTypeBuilder UpperRoman => new(ListStyleTypeValue.UpperRoman);
    /// <summary>
    /// Gets a list style type builder with lower-greek value (lowercase greek letters).
    /// </summary>
    public static ListStyleTypeBuilder LowerGreek => new(ListStyleTypeValue.LowerGreek);
    /// <summary>
    /// Gets a list style type builder with armenian value (armenian numbering).
    /// </summary>
    public static ListStyleTypeBuilder Armenian => new(ListStyleTypeValue.Armenian);
    /// <summary>
    /// Gets a list style type builder with georgian value (georgian numbering).
    /// </summary>
    public static ListStyleTypeBuilder Georgian => new(ListStyleTypeValue.Georgian);
    /// <summary>
    /// Gets a list style type builder with inherit keyword.
    /// </summary>
    public static ListStyleTypeBuilder Inherit => new(ListStyleTypeValue.Inherit);
    /// <summary>
    /// Gets a list style type builder with initial keyword.
    /// </summary>
    public static ListStyleTypeBuilder Initial => new(ListStyleTypeValue.Initial);
    /// <summary>
    /// Gets a list style type builder with unset keyword.
    /// </summary>
    public static ListStyleTypeBuilder Unset => new(ListStyleTypeValue.Unset);
}

