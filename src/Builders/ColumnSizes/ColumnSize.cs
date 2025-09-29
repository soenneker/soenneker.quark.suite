namespace Soenneker.Quark.Builders.ColumnSizes;

/// <summary>
/// Simplified column size utility with fluent API and Bootstrap-first approach.
/// </summary>
public static class ColumnSize
{
    /// <summary>
    /// Column size 1 (1/12 of the grid).
    /// </summary>
    public static ColumnSizeBuilder S1 => new("1");

    /// <summary>
    /// Column size 2 (2/12 of the grid).
    /// </summary>
    public static ColumnSizeBuilder S2 => new("2");

    /// <summary>
    /// Column size 3 (3/12 of the grid).
    /// </summary>
    public static ColumnSizeBuilder S3 => new("3");

    /// <summary>
    /// Column size 4 (4/12 of the grid).
    /// </summary>
    public static ColumnSizeBuilder S4 => new("4");

    /// <summary>
    /// Column size 5 (5/12 of the grid).
    /// </summary>
    public static ColumnSizeBuilder S5 => new("5");

    /// <summary>
    /// Column size 6 (6/12 of the grid).
    /// </summary>
    public static ColumnSizeBuilder S6 => new("6");

    /// <summary>
    /// Column size 7 (7/12 of the grid).
    /// </summary>
    public static ColumnSizeBuilder S7 => new("7");

    /// <summary>
    /// Column size 8 (8/12 of the grid).
    /// </summary>
    public static ColumnSizeBuilder S8 => new("8");

    /// <summary>
    /// Column size 9 (9/12 of the grid).
    /// </summary>
    public static ColumnSizeBuilder S9 => new("9");

    /// <summary>
    /// Column size 10 (10/12 of the grid).
    /// </summary>
    public static ColumnSizeBuilder S10 => new("10");

    /// <summary>
    /// Column size 11 (11/12 of the grid).
    /// </summary>
    public static ColumnSizeBuilder S11 => new("11");

    /// <summary>
    /// Column size 12 (12/12 of the grid - full width).
    /// </summary>
    public static ColumnSizeBuilder S12 => new("12");

    /// <summary>
    /// Auto column size (takes remaining space).
    /// </summary>
    public static ColumnSizeBuilder Auto => new("auto");
}
