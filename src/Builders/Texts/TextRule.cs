namespace Soenneker.Quark;

/// <summary>
/// Represents a CSS rule for Bootstrap text utilities.
/// </summary>
public readonly struct TextRule
{
    public readonly string ClassName;
    public readonly string CssRule;

    public TextRule(string className, string cssRule)
    {
        ClassName = className;
        CssRule = cssRule;
    }
}
