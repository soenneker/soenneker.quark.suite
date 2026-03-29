namespace Soenneker.Quark;

/// <summary>
/// Grid column utility with fluent responsive chaining.
/// </summary>
public static class Columns
{
    public static ColumnBuilder Is1 => new("grid-cols", "1");
    public static ColumnBuilder Is2 => new("grid-cols", "2");
    public static ColumnBuilder Is3 => new("grid-cols", "3");
    public static ColumnBuilder Is4 => new("grid-cols", "4");
    public static ColumnBuilder Is5 => new("grid-cols", "5");
    public static ColumnBuilder Is6 => new("grid-cols", "6");
    public static ColumnBuilder Is7 => new("grid-cols", "7");
    public static ColumnBuilder Is8 => new("grid-cols", "8");
    public static ColumnBuilder Is9 => new("grid-cols", "9");
    public static ColumnBuilder Is10 => new("grid-cols", "10");
    public static ColumnBuilder Is11 => new("grid-cols", "11");
    public static ColumnBuilder Is12 => new("grid-cols", "12");
    public static ColumnBuilder None => new("grid-cols", "none");
    public static ColumnBuilder Subgrid => new("grid-cols", "subgrid");

    /// <summary>
    /// Create from a numeric grid column count.
    /// </summary>
    public static ColumnBuilder Count(int value) => new("grid-cols", value.ToString());
}
