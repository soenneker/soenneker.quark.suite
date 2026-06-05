namespace Soenneker.Quark;

/// <summary>
/// Represents the tree item options.
/// </summary>
public sealed class TreeItemOptions : ComponentOptions
{
    public TreeItemOptions()
    {
        Selector = "[data-slot='tree-item']";
    }
}
