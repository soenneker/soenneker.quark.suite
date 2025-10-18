using Soenneker.Quark.Enums;

namespace Soenneker.Quark;

/// <summary>
/// Interface for the Anchor component
/// </summary>
public interface IAnchor : IElement
{
    /// <summary>
    /// The URL that the hyperlink points to
    /// </summary>
    string? To { get; set; }

    /// <summary>
    /// Where to display the linked URL
    /// </summary>
    Target? Target { get; set; }
}
