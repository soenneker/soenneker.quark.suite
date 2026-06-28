namespace Soenneker.Quark;

/// <summary>
/// Represents the select content options.
/// </summary>
public sealed class SelectContentOptions : ComponentOptions
{
    public SelectContentOptions()
    {
        Selector = "[data-slot='select-content']";
    }
}
