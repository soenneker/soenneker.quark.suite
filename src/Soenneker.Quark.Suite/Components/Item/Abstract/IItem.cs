namespace Soenneker.Quark;

/// <summary>
/// Represents a shadcn-style item container.
/// </summary>
public interface IItem : IElement
{
    /// <summary>
    /// Gets or sets variant.
    /// </summary>
    ItemVariant Variant { get; set; }
    /// <summary>
    /// Gets or sets item size.
    /// </summary>
    ItemSize ItemSize { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether as child.
    /// </summary>
    bool AsChild { get; set; }
}
