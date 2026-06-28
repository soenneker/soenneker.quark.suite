
namespace Soenneker.Quark;

/// <summary>
/// Represents the ordered list item options.
/// </summary>
public sealed class OrderedListItemOptions : ComponentOptions
{
    public OrderedListItemOptions()
    {
        Selector = "[data-slot='ordered-list-item']";
    }
}
