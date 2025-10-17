using Soenneker.Quark.Enums;


namespace Soenneker.Quark;

/// <summary>
/// CSS list-style-type property builder with fluent API.
/// </summary>
public static class ListStyleType
{
    public static ListStyleTypeBuilder None => new(ListStyleTypeValue.None);
    public static ListStyleTypeBuilder Disc => new(ListStyleTypeValue.Disc);
    public static ListStyleTypeBuilder Circle => new(ListStyleTypeValue.Circle);
    public static ListStyleTypeBuilder Square => new(ListStyleTypeValue.Square);
    public static ListStyleTypeBuilder Decimal => new(ListStyleTypeValue.Decimal);
    public static ListStyleTypeBuilder DecimalLeadingZero => new(ListStyleTypeValue.DecimalLeadingZero);
    public static ListStyleTypeBuilder LowerAlpha => new(ListStyleTypeValue.LowerAlpha);
    public static ListStyleTypeBuilder UpperAlpha => new(ListStyleTypeValue.UpperAlpha);
    public static ListStyleTypeBuilder LowerRoman => new(ListStyleTypeValue.LowerRoman);
    public static ListStyleTypeBuilder UpperRoman => new(ListStyleTypeValue.UpperRoman);
    public static ListStyleTypeBuilder LowerGreek => new(ListStyleTypeValue.LowerGreek);
    public static ListStyleTypeBuilder Armenian => new(ListStyleTypeValue.Armenian);
    public static ListStyleTypeBuilder Georgian => new(ListStyleTypeValue.Georgian);
    public static ListStyleTypeBuilder Inherit => new(ListStyleTypeValue.Inherit);
    public static ListStyleTypeBuilder Initial => new(ListStyleTypeValue.Initial);
    public static ListStyleTypeBuilder Unset => new(ListStyleTypeValue.Unset);
}

