
using System.Collections.Generic;

namespace Soenneker.Quark;

/// <summary>
/// Represents the tabs options.
/// </summary>
public sealed class TabsOptions : ComponentOptions
{
    public TabsOptions()
    {
        Selector = "[data-slot='tabs']";
    }

    /// <summary>
    /// Gets or sets tabs content styling scoped to tabs.
    /// </summary>
    public TabsContentOptions? Contents { get; set; }

    /// <summary>
    /// Gets or sets tabs list styling scoped to tabs.
    /// </summary>
    public TabsListOptions? Lists { get; set; }

    /// <summary>
    /// Gets or sets tabs trigger styling scoped to tabs.
    /// </summary>
    public TabOptions? Triggers { get; set; }

    private protected override void CollectChildCssRules(List<ComponentCssRule> buffer, string baseSelector)
    {
        AddChildCssRules(buffer, Contents, "[data-slot='tabs-content']", "[data-slot='tabs-content']", baseSelector);
        AddChildCssRules(buffer, Lists, "[data-slot='tabs-list']", "[data-slot='tabs-list']", baseSelector);
        AddChildCssRules(buffer, Triggers, "[data-slot='tabs-trigger']", "[data-slot='tabs-trigger']", baseSelector);
    }
}
