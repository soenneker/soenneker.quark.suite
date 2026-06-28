
using System.Collections.Generic;

namespace Soenneker.Quark;

/// <summary>
/// Represents the ordered list options.
/// </summary>
public sealed class OrderedListOptions : ComponentOptions
{
    public OrderedListOptions()
    {
        Selector = "[data-slot='ordered-list']";
    }

    /// <summary>
    /// Gets or sets ordered list item styling scoped to the ordered list.
    /// </summary>
    public OrderedListItemOptions? Items { get; set; }

    private protected override void CollectChildCssRules(List<ComponentCssRule> buffer, string baseSelector)
    {
        AddChildCssRules(buffer, Items, "[data-slot='ordered-list-item']", "[data-slot='ordered-list-item']", baseSelector);
    }
}
