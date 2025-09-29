namespace Soenneker.Quark.Builders.ColumnOffsets;

/// <summary>
/// Simplified column offset utility with fluent API and Bootstrap-first approach.
/// </summary>
public static class ColumnOffset
{
    /// <summary>
    /// Column offset 1 (1/12 of the grid).
    /// </summary>
    public static ColumnOffsetBuilder O1 => new("1");

    /// <summary>
    /// Column offset 2 (2/12 of the grid).
    /// </summary>
    public static ColumnOffsetBuilder O2 => new("2");

    /// <summary>
    /// Column offset 3 (3/12 of the grid).
    /// </summary>
    public static ColumnOffsetBuilder O3 => new("3");

    /// <summary>
    /// Column offset 4 (4/12 of the grid).
    /// </summary>
    public static ColumnOffsetBuilder O4 => new("4");

    /// <summary>
    /// Column offset 5 (5/12 of the grid).
    /// </summary>
    public static ColumnOffsetBuilder O5 => new("5");

    /// <summary>
    /// Column offset 6 (6/12 of the grid).
    /// </summary>
    public static ColumnOffsetBuilder O6 => new("6");

    /// <summary>
    /// Column offset 7 (7/12 of the grid).
    /// </summary>
    public static ColumnOffsetBuilder O7 => new("7");

    /// <summary>
    /// Column offset 8 (8/12 of the grid).
    /// </summary>
    public static ColumnOffsetBuilder O8 => new("8");

    /// <summary>
    /// Column offset 9 (9/12 of the grid).
    /// </summary>
    public static ColumnOffsetBuilder O9 => new("9");

    /// <summary>
    /// Column offset 10 (10/12 of the grid).
    /// </summary>
    public static ColumnOffsetBuilder O10 => new("10");

    /// <summary>
    /// Column offset 11 (11/12 of the grid).
    /// </summary>
    public static ColumnOffsetBuilder O11 => new("11");

    /// <summary>
    /// Column offset 12 (12/12 of the grid - full width).
    /// </summary>
    public static ColumnOffsetBuilder O12 => new("12");
}
