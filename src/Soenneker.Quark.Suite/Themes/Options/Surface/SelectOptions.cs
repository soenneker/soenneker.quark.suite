
using System.Collections.Generic;

namespace Soenneker.Quark;

/// <summary>
/// Represents the select options.
/// </summary>
public sealed class SelectOptions : ComponentOptions
{
    public SelectOptions()
    {
        Selector = "[data-slot='select']";
    }

    /// <summary>
    /// Gets or sets select content styling.
    /// </summary>
    public SelectContentOptions? Contents { get; set; }

    /// <summary>
    /// Gets or sets select group styling.
    /// </summary>
    public SelectGroupOptions? Groups { get; set; }

    /// <summary>
    /// Gets or sets select item styling.
    /// </summary>
    public SelectItemOptions? Items { get; set; }

    /// <summary>
    /// Gets or sets select label styling.
    /// </summary>
    public SelectLabelOptions? Labels { get; set; }

    /// <summary>
    /// Gets or sets select separator styling.
    /// </summary>
    public SelectSeparatorOptions? Separators { get; set; }

    /// <summary>
    /// Gets or sets select trigger styling.
    /// </summary>
    public SelectTriggerOptions? Triggers { get; set; }

    /// <summary>
    /// Gets or sets select value styling.
    /// </summary>
    public SelectValueOptions? Values { get; set; }

    private protected override void CollectChildCssRules(List<ComponentCssRule> buffer, string baseSelector)
    {
        string selectScope = baseSelector == "[data-slot='select']" ? string.Empty : baseSelector;

        AddChildCssRules(buffer, Contents, "[data-slot='select-content']", "[data-slot='select-content']", selectScope);
        AddChildCssRules(buffer, Groups, "[data-slot='select-group']", "[data-slot='select-group']", selectScope);
        AddChildCssRules(buffer, Items, "[data-slot='select-item']", "[data-slot='select-item']", selectScope);
        AddChildCssRules(buffer, Labels, "[data-slot='select-label']", "[data-slot='select-label']", selectScope);
        AddChildCssRules(buffer, Separators, "[data-slot='select-separator']", "[data-slot='select-separator']", selectScope);
        AddChildCssRules(buffer, Triggers, "[data-slot='select-trigger']", "[data-slot='select-trigger']", selectScope);
        AddChildCssRules(buffer, Values, "[data-slot='select-value']", "[data-slot='select-value']", selectScope);
    }
}
