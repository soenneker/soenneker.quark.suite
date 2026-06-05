namespace Soenneker.Quark;

/// <summary>
/// Represents the tree item label options.
/// </summary>
public sealed class TreeItemLabelOptions : ComponentOptions
{
    public TreeItemLabelOptions()
    {
        Selector = "[data-slot='tree-item-label']";
    }
}
