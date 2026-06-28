using System.Collections.Generic;

namespace Soenneker.Quark;

/// <summary>
/// Represents the tree options.
/// </summary>
public sealed class TreeOptions : ComponentOptions
{
    public TreeOptions()
    {
        Selector = "[data-slot='tree']";
    }

    /// <summary>
    /// Gets or sets tree drag line styling scoped to the tree.
    /// </summary>
    public TreeDragLineOptions? DragLines { get; set; }

    /// <summary>
    /// Gets or sets icon styling scoped to the tree.
    /// </summary>
    public IconOptions? Icons { get; set; }

    /// <summary>
    /// Gets or sets tree item label styling scoped to the tree.
    /// </summary>
    public TreeItemLabelOptions? ItemLabels { get; set; }

    /// <summary>
    /// Gets or sets tree item styling scoped to the tree.
    /// </summary>
    public TreeItemOptions? Items { get; set; }

    private protected override void CollectChildCssRules(List<ComponentCssRule> buffer, string baseSelector)
    {
        AddChildCssRules(buffer, DragLines, "[data-slot='tree-drag-line']", "[data-slot='tree-drag-line']", baseSelector);
        AddChildCssRules(buffer, Icons, "[data-slot='icon']", "[data-slot='icon']", baseSelector);
        AddChildCssRules(buffer, ItemLabels, "[data-slot='tree-item-label']", "[data-slot='tree-item-label']", baseSelector);
        AddChildCssRules(buffer, Items, "[data-slot='tree-item']", "[data-slot='tree-item']", baseSelector);
    }
}
