using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Ordering tokens for grid or flex children.
/// </summary>
[EnumValue<string>]
public partial class ColumnOrderType
{
    /// <summary>
    /// Column order 1.
    /// CSS/Tailwind token: order-1
    /// </summary>
    public static readonly ColumnOrderType O1 = new("1");

    /// <summary>
    /// Column order 2.
    /// CSS/Tailwind token: order-2
    /// </summary>
    public static readonly ColumnOrderType O2 = new("2");

    /// <summary>
    /// Column order 3.
    /// CSS/Tailwind token: order-3
    /// </summary>
    public static readonly ColumnOrderType O3 = new("3");

    /// <summary>
    /// Column order 4.
    /// CSS/Tailwind token: order-4
    /// </summary>
    public static readonly ColumnOrderType O4 = new("4");

    /// <summary>
    /// Column order 5.
    /// CSS/Tailwind token: order-5
    /// </summary>
    public static readonly ColumnOrderType O5 = new("5");

    /// <summary>
    /// Column order 6.
    /// CSS/Tailwind token: order-6
    /// </summary>
    public static readonly ColumnOrderType O6 = new("6");

    /// <summary>
    /// Column order 7.
    /// CSS/Tailwind token: order-7
    /// </summary>
    public static readonly ColumnOrderType O7 = new("7");

    /// <summary>
    /// Column order 8.
    /// CSS/Tailwind token: order-8
    /// </summary>
    public static readonly ColumnOrderType O8 = new("8");

    /// <summary>
    /// Column order 9.
    /// CSS/Tailwind token: order-9
    /// </summary>
    public static readonly ColumnOrderType O9 = new("9");

    /// <summary>
    /// Column order 10.
    /// CSS/Tailwind token: order-10
    /// </summary>
    public static readonly ColumnOrderType O10 = new("10");

    /// <summary>
    /// Column order 11.
    /// CSS/Tailwind token: order-11
    /// </summary>
    public static readonly ColumnOrderType O11 = new("11");

    /// <summary>
    /// Column order 12.
    /// CSS/Tailwind token: order-12
    /// </summary>
    public static readonly ColumnOrderType O12 = new("12");

    /// <summary>
    /// Column order first.
    /// CSS/Tailwind token: order-first
    /// </summary>
    public static readonly ColumnOrderType First = new("first");

    /// <summary>
    /// Column order last.
    /// CSS/Tailwind token: order-last
    /// </summary>
    public static readonly ColumnOrderType Last = new("last");
}
