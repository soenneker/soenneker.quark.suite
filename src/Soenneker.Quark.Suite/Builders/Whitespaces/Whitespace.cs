namespace Soenneker.Quark;

/// <summary>
/// Tailwind whitespace utility entry points.
/// </summary>
public static class Whitespace
{
    public static WhitespaceBuilder Normal => new("normal");
    public static WhitespaceBuilder Nowrap => new("nowrap");
    public static WhitespaceBuilder Pre => new("pre");
    public static WhitespaceBuilder PreLine => new("pre-line");
    public static WhitespaceBuilder PreWrap => new("pre-wrap");
    public static WhitespaceBuilder BreakSpaces => new("break-spaces");
}
