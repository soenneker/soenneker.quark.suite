namespace Soenneker.Quark;

/// <summary>
/// Represents the select label options.
/// </summary>
public sealed class SelectLabelOptions : ComponentOptions
{
    public SelectLabelOptions()
    {
        Selector = "[data-slot='select-label']";
    }
}
