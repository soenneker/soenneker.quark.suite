using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Soenneker.Quark;

/// <summary>
/// Represents an interactive surface component with event handling and accessibility features.
/// </summary>
public interface IInteractiveSurfaceElement : ISurfaceElement
{
    /// <summary>
    /// Gets or sets the focus ring style when the element is focused.
    /// </summary>
    CssValue<FocusRingBuilder>? FocusRing { get; set; }

    /// <summary>
    /// Gets or sets the interaction behavior (e.g., pointer-events).
    /// </summary>
    CssValue<InteractionBuilder>? Interaction { get; set; }

    /// <summary>
    /// Gets or sets the tab index for keyboard navigation.
    /// </summary>
    int? TabIndex { get; set; }

    /// <summary>
    /// Gets or sets the ARIA role attribute.
    /// </summary>
    string? Role { get; set; }

    /// <summary>
    /// Gets or sets the ARIA label for accessibility.
    /// </summary>
    string? AriaLabel { get; set; }

    /// <summary>
    /// Gets or sets the ARIA described-by attribute referencing describing element IDs.
    /// </summary>
    string? AriaDescribedBy { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when the element is double-clicked.
    /// </summary>
    EventCallback<MouseEventArgs> OnDoubleClick { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when the mouse enters the element.
    /// </summary>
    EventCallback<MouseEventArgs> OnMouseOver { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when the mouse leaves the element.
    /// </summary>
    EventCallback<MouseEventArgs> OnMouseOut { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when a key is pressed while the element is focused.
    /// </summary>
    EventCallback<KeyboardEventArgs> OnKeyDown { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when the element receives focus.
    /// </summary>
    EventCallback<FocusEventArgs> OnFocus { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when the element loses focus.
    /// </summary>
    EventCallback<FocusEventArgs> OnBlur { get; set; }
}

