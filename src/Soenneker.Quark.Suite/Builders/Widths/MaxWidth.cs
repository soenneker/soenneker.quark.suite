namespace Soenneker.Quark;

/// <summary>
/// Max-width utility with fluent API. Tailwind-first (max-w-*).
/// </summary>
public static class MaxWidth
{
    public static MaxWidthBuilder IsNone => new("none");
    public static MaxWidthBuilder IsXs => new("xs");
    public static MaxWidthBuilder IsSm => new("sm");
    public static MaxWidthBuilder IsMd => new("md");
    public static MaxWidthBuilder IsLg => new("lg");
    public static MaxWidthBuilder IsXl => new("xl");
    public static MaxWidthBuilder Is2xl => new("2xl");
    public static MaxWidthBuilder Is3xl => new("3xl");
    public static MaxWidthBuilder Is4xl => new("4xl");
    public static MaxWidthBuilder Is5xl => new("5xl");
    public static MaxWidthBuilder Is6xl => new("6xl");
    public static MaxWidthBuilder Is7xl => new("7xl");
    public static MaxWidthBuilder IsFull => new("full");
    public static MaxWidthBuilder IsMin => new("min");
    public static MaxWidthBuilder IsMax => new("max");
    public static MaxWidthBuilder IsFit => new("fit");
    public static MaxWidthBuilder IsScreen => new("screen");
    public static MaxWidthBuilder IsProse => new("prose");

    /// <summary>
    /// Create from an arbitrary Tailwind max-width token (e.g. "4xl", "[56rem]", "[75ch]").
    /// </summary>
    public static MaxWidthBuilder Token(string token) => new(token);
}
