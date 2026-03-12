namespace Soenneker.Quark;

internal readonly struct ComponentCssRule
{
    public ComponentCssRule(string selector, string declaration)
    {
        Selector = selector;
        Declaration = declaration;
    }

    public string Selector { get; }

    public string Declaration { get; }
}