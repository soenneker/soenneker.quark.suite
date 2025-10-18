namespace Soenneker.Quark;

/// <summary>
/// Represents an individual item within a list group.
/// </summary>
public interface IListGroupItem : IElement
{
    /// <summary>
    /// Gets or sets the unique name identifier for this item.
    /// </summary>
    string? ItemName { get; set; }

    /// <summary>
    /// Gets or sets whether the item is disabled.
    /// </summary>
    bool Disabled { get; set; }

    /// <summary>
    /// Gets or sets whether the item is marked as active/selected.
    /// </summary>
    bool Active { get; set; }

}

