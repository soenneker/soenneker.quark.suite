namespace Soenneker.Quark;

/// <summary>
/// Represents a shadcn-style item container.
/// </summary>
public interface IItem : IElement
{
    ItemVariant Variant { get; set; }
    ItemSize Size { get; set; }
}
