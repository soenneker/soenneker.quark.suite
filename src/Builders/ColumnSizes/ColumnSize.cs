
namespace Soenneker.Quark;

/// <summary>
/// Simplified column size utility with fluent API and Bootstrap-first approach.
/// </summary>
public static class ColumnSize
{
    /// <summary>
    /// Column size 1 (1/12 of the grid).
    /// </summary>
    public static ColumnSizeBuilder Is1 => new("1");

    /// <summary>
    /// Column size 2 (2/12 of the grid).
    /// </summary>
    public static ColumnSizeBuilder Is2 => new("2");

    /// <summary>
    /// Column size 3 (3/12 of the grid).
    /// </summary>
    public static ColumnSizeBuilder Is3 => new("3");

    /// <summary>
    /// Column size 4 (4/12 of the grid).
    /// </summary>
    public static ColumnSizeBuilder Is4 => new("4");

    /// <summary>
    /// Column size 5 (5/12 of the grid).
    /// </summary>
    public static ColumnSizeBuilder Is5 => new("5");

    /// <summary>
    /// Column size 6 (6/12 of the grid).
    /// </summary>
    public static ColumnSizeBuilder Is6 => new("6");

    /// <summary>
    /// Column size 7 (7/12 of the grid).
    /// </summary>
    public static ColumnSizeBuilder Is7 => new("7");

    /// <summary>
    /// Column size 8 (8/12 of the grid).
    /// </summary>
    public static ColumnSizeBuilder Is8 => new("8");

    /// <summary>
    /// Column size 9 (9/12 of the grid).
    /// </summary>
    public static ColumnSizeBuilder Is9 => new("9");

    /// <summary>
    /// Column size 10 (10/12 of the grid).
    /// </summary>
    public static ColumnSizeBuilder Is10 => new("10");

    /// <summary>
    /// Column size 11 (11/12 of the grid).
    /// </summary>
    public static ColumnSizeBuilder Is11 => new("11");

    /// <summary>
    /// Column size 12 (12/12 of the grid - full width).
    /// </summary>
    public static ColumnSizeBuilder Is12 => new("12");

    /// <summary>
    /// Auto column size (takes remaining space).
    /// </summary>
    public static ColumnSizeBuilder Auto => new("auto");
}
