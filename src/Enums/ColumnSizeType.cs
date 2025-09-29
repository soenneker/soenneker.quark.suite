using Intellenum;

namespace Soenneker.Quark.Enums;

/// <summary>
/// An enumeration for Quark, representing Bootstrap column size values.
/// This enum contains the standard Bootstrap column size values used for grid layout.
/// </summary>
[Intellenum<string>]
public sealed partial class ColumnSizeType
{
    /// <summary>
    /// Column size 1 (1/12 of the grid).
    /// Bootstrap class: col-1
    /// </summary>
    public static readonly ColumnSizeType S1 = new("1");

    /// <summary>
    /// Column size 2 (2/12 of the grid).
    /// Bootstrap class: col-2
    /// </summary>
    public static readonly ColumnSizeType S2 = new("2");

    /// <summary>
    /// Column size 3 (3/12 of the grid).
    /// Bootstrap class: col-3
    /// </summary>
    public static readonly ColumnSizeType S3 = new("3");

    /// <summary>
    /// Column size 4 (4/12 of the grid).
    /// Bootstrap class: col-4
    /// </summary>
    public static readonly ColumnSizeType S4 = new("4");

    /// <summary>
    /// Column size 5 (5/12 of the grid).
    /// Bootstrap class: col-5
    /// </summary>
    public static readonly ColumnSizeType S5 = new("5");

    /// <summary>
    /// Column size 6 (6/12 of the grid).
    /// Bootstrap class: col-6
    /// </summary>
    public static readonly ColumnSizeType S6 = new("6");

    /// <summary>
    /// Column size 7 (7/12 of the grid).
    /// Bootstrap class: col-7
    /// </summary>
    public static readonly ColumnSizeType S7 = new("7");

    /// <summary>
    /// Column size 8 (8/12 of the grid).
    /// Bootstrap class: col-8
    /// </summary>
    public static readonly ColumnSizeType S8 = new("8");

    /// <summary>
    /// Column size 9 (9/12 of the grid).
    /// Bootstrap class: col-9
    /// </summary>
    public static readonly ColumnSizeType S9 = new("9");

    /// <summary>
    /// Column size 10 (10/12 of the grid).
    /// Bootstrap class: col-10
    /// </summary>
    public static readonly ColumnSizeType S10 = new("10");

    /// <summary>
    /// Column size 11 (11/12 of the grid).
    /// Bootstrap class: col-11
    /// </summary>
    public static readonly ColumnSizeType S11 = new("11");

    /// <summary>
    /// Column size 12 (12/12 of the grid - full width).
    /// Bootstrap class: col-12
    /// </summary>
    public static readonly ColumnSizeType S12 = new("12");

    /// <summary>
    /// Auto column size (takes remaining space).
    /// Bootstrap class: col-auto
    /// </summary>
    public static readonly ColumnSizeType Auto = new("auto");
}
