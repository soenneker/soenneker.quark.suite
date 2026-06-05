namespace Soenneker.Quark;

/// <summary>
/// Represents an item inside a <see cref="ISortableList"/>.
/// </summary>
public interface ISortableItem : IElement
{
    /// <summary>
    /// Gets or sets item id.
    /// </summary>
    string? ItemId { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether disabled.
    /// </summary>
    bool Disabled { get; set; }
}
