namespace Soenneker.Quark;

/// <summary>
/// Grid row span utility with fluent responsive chaining.
/// </summary>
public static class RowSpan
{
    public static RowSpanBuilder Is1 => new("1");
    public static RowSpanBuilder Is2 => new("2");
    public static RowSpanBuilder Is3 => new("3");
    public static RowSpanBuilder Is4 => new("4");
    public static RowSpanBuilder Is5 => new("5");
    public static RowSpanBuilder Is6 => new("6");
    public static RowSpanBuilder Full => new("full");

    /// <summary>
    /// Create from a numeric grid row span.
    /// </summary>
    public static RowSpanBuilder Count(int value) => new(value.ToString());
}
