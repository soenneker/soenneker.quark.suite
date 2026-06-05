namespace Soenneker.Quark;

/// <summary>
/// Represents the collapse options.
/// </summary>
public sealed class CollapseOptions : ComponentOptions
{
    public CollapseOptions()
    {
        Selector = "[data-slot='collapse']";
    }
}
