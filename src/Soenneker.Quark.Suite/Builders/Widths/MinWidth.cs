namespace Soenneker.Quark;

/// <summary>
/// Min-width utility with fluent API. Tailwind-first (min-w-*).
/// </summary>
public static class MinWidth
{
    public static MinWidthBuilder Is0 => new("0");
    public static MinWidthBuilder IsPx => new("px");
    public static MinWidthBuilder IsFull => new("full");
    public static MinWidthBuilder IsMin => new("min");
    public static MinWidthBuilder IsMax => new("max");
    public static MinWidthBuilder IsFit => new("fit");

    /// <summary>
    /// Create from an arbitrary Tailwind min-width token (e.g. "24", "[8rem]", "[12rem]").
    /// </summary>
    public static MinWidthBuilder Token(string token) => new(token);
}
