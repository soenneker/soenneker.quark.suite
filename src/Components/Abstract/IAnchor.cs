using Soenneker.Quark.Enums;

namespace Soenneker.Quark;

/// <summary>
/// Represents an anchor (link) component for navigation.
/// </summary>
public interface IAnchor : IElement
{
    /// <summary>
    /// Gets or sets the URL to navigate to.
    /// </summary>
    string? To { get; set; }

    /// <summary>
    /// Gets or sets the target attribute for the link (e.g., "_blank", "_self").
    /// </summary>
    Target? Target { get; set; }
}

