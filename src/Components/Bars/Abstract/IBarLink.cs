using Soenneker.Quark.Enums;

namespace Soenneker.Quark;

/// <summary>
/// Represents a navigation link within a bar.
/// </summary>
public interface IBarLink : IElement
{
    /// <summary>
    /// Gets or sets the URL to navigate to.
    /// </summary>
    string? To { get; set; }

    /// <summary>
    /// Gets or sets the target attribute for the link.
    /// </summary>
    Target? Target { get; set; }

    /// <summary>
    /// Gets or sets whether the link is active/selected.
    /// </summary>
    bool Active { get; set; }

    /// <summary>
    /// Gets or sets whether the link is disabled.
    /// </summary>
    bool Disabled { get; set; }
}

