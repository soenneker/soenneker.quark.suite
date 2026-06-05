
namespace Soenneker.Quark;

/// <summary>
/// Represents the select item options.
/// </summary>
public sealed class SelectItemOptions : ComponentOptions
{
    public SelectItemOptions()
    {
        Selector = "[data-slot='select-item']";
    }
}
