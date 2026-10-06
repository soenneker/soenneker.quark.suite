namespace Soenneker.Quark;

/// <summary>
/// Represents the unordered list options.
/// </summary>
public sealed class UnorderedListOptions : ComponentOptions
{
    public UnorderedListOptions()
    {
        Selector = "[data-slot='unordered-list']";
    }

    /// <summary>
    /// Gets or sets unordered list item styling scoped to the unordered list.
    /// </summary>
    public UnorderedListItemOptions? Items { get; set; }

    private protected override void CollectChildCssRules(ref ComponentCssRuleCollector buffer, string baseSelector)
    {
        AddChildCssRules(ref buffer, Items, "[data-slot='unordered-list-item']", "[data-slot='unordered-list-item']", baseSelector);
    }
}
