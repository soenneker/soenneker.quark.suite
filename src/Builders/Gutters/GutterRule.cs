namespace Soenneker.Quark;

/// <summary>
/// Represents a CSS rule for Bootstrap gutter utilities.
/// </summary>
public readonly struct GutterRule
{
    public readonly string ClassName;
    public readonly string CssRule;

    public GutterRule(string className, string cssRule)
    {
        ClassName = className;
        CssRule = cssRule;
    }
}
