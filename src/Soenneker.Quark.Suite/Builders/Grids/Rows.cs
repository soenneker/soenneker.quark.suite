namespace Soenneker.Quark;

/// <summary>
/// Grid row utility with fluent responsive chaining.
/// </summary>
public static class Rows
{
    public static RowBuilder Is1 => new("grid-rows", "1");
    public static RowBuilder Is2 => new("grid-rows", "2");
    public static RowBuilder Is3 => new("grid-rows", "3");
    public static RowBuilder Is4 => new("grid-rows", "4");
    public static RowBuilder Is5 => new("grid-rows", "5");
    public static RowBuilder Is6 => new("grid-rows", "6");
    public static RowBuilder None => new("grid-rows", "none");
    public static RowBuilder Subgrid => new("grid-rows", "subgrid");

    /// <summary>
    /// Create from a numeric grid row count.
    /// </summary>
    public static RowBuilder Count(int value) => new("grid-rows", value.ToString());
}
