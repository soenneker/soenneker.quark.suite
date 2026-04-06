namespace Soenneker.Quark;

/// <summary>
/// Represents an item inside a <see cref="ISortableList"/>.
/// </summary>
public interface ISortableItem : IElement
{
    string? ItemId { get; set; }

    bool Disabled { get; set; }
}
