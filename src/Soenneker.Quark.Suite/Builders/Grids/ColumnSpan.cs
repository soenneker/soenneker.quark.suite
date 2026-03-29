namespace Soenneker.Quark;

/// <summary>
/// Grid column span utility with fluent responsive chaining.
/// </summary>
public static class ColumnSpan
{
    public static ColumnSpanBuilder Is1 => new("1");
    public static ColumnSpanBuilder Is2 => new("2");
    public static ColumnSpanBuilder Is3 => new("3");
    public static ColumnSpanBuilder Is4 => new("4");
    public static ColumnSpanBuilder Is5 => new("5");
    public static ColumnSpanBuilder Is6 => new("6");
    public static ColumnSpanBuilder Is7 => new("7");
    public static ColumnSpanBuilder Is8 => new("8");
    public static ColumnSpanBuilder Is9 => new("9");
    public static ColumnSpanBuilder Is10 => new("10");
    public static ColumnSpanBuilder Is11 => new("11");
    public static ColumnSpanBuilder Is12 => new("12");
    public static ColumnSpanBuilder Full => new("full");

    /// <summary>
    /// Create from a numeric grid column span.
    /// </summary>
    public static ColumnSpanBuilder Count(int value) => new(value.ToString());
}
