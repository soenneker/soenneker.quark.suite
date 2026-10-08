namespace Soenneker.Quark;

/// <summary>
/// Represents an ordered (numbered) list element.
/// </summary>
public interface IOrderedList : IElement
{
    /// <summary>
    /// Gets or sets the HTML start attribute.
    /// </summary>
    int? Start { get; set; }

    /// <summary>
    /// Gets or sets the HTML reversed attribute.
    /// </summary>
    bool? Reversed { get; set; }

    /// <summary>
    /// Gets or sets the native numbering style: 1, a, A, i, or I.
    /// </summary>
    string? Type { get; set; }


}

