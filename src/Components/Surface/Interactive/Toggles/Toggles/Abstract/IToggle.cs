using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>
/// Represents a shadcn-style toggle button with pressed state.
/// </summary>
public interface IToggle : IInteractiveSurfaceElement
{
    /// <summary>
    /// Gets or sets whether the toggle is pressed.
    /// </summary>
    bool Pressed { get; set; }

    /// <summary>
    /// Gets or sets callback invoked when pressed state changes.
    /// </summary>
    EventCallback<bool> PressedChanged { get; set; }

    /// <summary>
    /// Gets or sets whether the toggle is disabled.
    /// </summary>
    bool Disabled { get; set; }

    /// <summary>
    /// Gets or sets the visual style variant.
    /// </summary>
    ToggleVariant Variant { get; set; }

    /// <summary>
    /// Gets or sets the size.
    /// </summary>
    ToggleSize Size { get; set; }
}
