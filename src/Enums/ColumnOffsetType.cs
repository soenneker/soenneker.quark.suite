using Intellenum;

namespace Soenneker.Quark.Enums;

/// <summary>
/// An enumeration for Quark, representing Bootstrap column offset values.
/// This enum contains the standard Bootstrap column offset values used for grid layout.
/// </summary>
[Intellenum<string>]
public partial class ColumnOffsetType
{
    /// <summary>
    /// Column offset 1 (1/12 of the grid).
    /// Bootstrap class: offset-1
    /// </summary>
    public static readonly ColumnOffsetType O1 = new("1");

    /// <summary>
    /// Column offset 2 (2/12 of the grid).
    /// Bootstrap class: offset-2
    /// </summary>
    public static readonly ColumnOffsetType O2 = new("2");

    /// <summary>
    /// Column offset 3 (3/12 of the grid).
    /// Bootstrap class: offset-3
    /// </summary>
    public static readonly ColumnOffsetType O3 = new("3");

    /// <summary>
    /// Column offset 4 (4/12 of the grid).
    /// Bootstrap class: offset-4
    /// </summary>
    public static readonly ColumnOffsetType O4 = new("4");

    /// <summary>
    /// Column offset 5 (5/12 of the grid).
    /// Bootstrap class: offset-5
    /// </summary>
    public static readonly ColumnOffsetType O5 = new("5");

    /// <summary>
    /// Column offset 6 (6/12 of the grid).
    /// Bootstrap class: offset-6
    /// </summary>
    public static readonly ColumnOffsetType O6 = new("6");

    /// <summary>
    /// Column offset 7 (7/12 of the grid).
    /// Bootstrap class: offset-7
    /// </summary>
    public static readonly ColumnOffsetType O7 = new("7");

    /// <summary>
    /// Column offset 8 (8/12 of the grid).
    /// Bootstrap class: offset-8
    /// </summary>
    public static readonly ColumnOffsetType O8 = new("8");

    /// <summary>
    /// Column offset 9 (9/12 of the grid).
    /// Bootstrap class: offset-9
    /// </summary>
    public static readonly ColumnOffsetType O9 = new("9");

    /// <summary>
    /// Column offset 10 (10/12 of the grid).
    /// Bootstrap class: offset-10
    /// </summary>
    public static readonly ColumnOffsetType O10 = new("10");

    /// <summary>
    /// Column offset 11 (11/12 of the grid).
    /// Bootstrap class: offset-11
    /// </summary>
    public static readonly ColumnOffsetType O11 = new("11");

    /// <summary>
    /// Column offset 12 (12/12 of the grid - full width).
    /// Bootstrap class: offset-12
    /// </summary>
    public static readonly ColumnOffsetType O12 = new("12");
}
