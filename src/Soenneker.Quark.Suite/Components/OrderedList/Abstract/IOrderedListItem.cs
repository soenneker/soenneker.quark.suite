namespace Soenneker.Quark;

/// <summary>
/// Represents a list item within an ordered list.
/// </summary>
public interface IOrderedListItem : IElement
{
    /// <summary>
    /// Gets or sets the HTML value attribute.
    /// </summary>
    int? Value { get; set; }


}

