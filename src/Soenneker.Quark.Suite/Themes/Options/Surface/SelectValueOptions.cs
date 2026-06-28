namespace Soenneker.Quark;

/// <summary>
/// Represents the select value options.
/// </summary>
public sealed class SelectValueOptions : ComponentOptions
{
    public SelectValueOptions()
    {
        Selector = "[data-slot='select-value']";
    }
}
