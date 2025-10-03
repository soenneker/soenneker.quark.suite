namespace Soenneker.Quark;

/// <summary>
/// Represents a CSS rule for Bootstrap column auto utilities.
/// </summary>
public readonly struct ColumnAutoRule
{
    public readonly string ClassName;
    public readonly string CssRule;

    public ColumnAutoRule(string className, string cssRule)
    {
        ClassName = className;
        CssRule = cssRule;
    }
}
