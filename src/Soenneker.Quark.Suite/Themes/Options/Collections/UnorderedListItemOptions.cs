
namespace Soenneker.Quark;

/// <summary>
/// Represents the unordered list item options.
/// </summary>
public sealed class UnorderedListItemOptions : ComponentOptions
{
    public UnorderedListItemOptions()
    {
        Selector = "[data-slot='unordered-list-item']";
    }
}
