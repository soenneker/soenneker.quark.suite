namespace Soenneker.Quark;

/// <summary>
/// Represents a CSS rule for Bootstrap container utilities.
/// </summary>
public readonly struct ContainerRule
{
    public readonly string ClassName;
    public readonly string CssRule;

    public ContainerRule(string className, string cssRule)
    {
        ClassName = className;
        CssRule = cssRule;
    }
}
