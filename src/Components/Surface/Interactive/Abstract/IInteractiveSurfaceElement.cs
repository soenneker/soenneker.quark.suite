using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Soenneker.Quark;

/// <summary>
/// Interface for interactive surface elements like buttons, inputs, and toggles.
/// Provides access to interactive properties, accessibility attributes, and event handlers
/// that are specific to user-interactive components.
/// </summary>
public interface IInteractiveSurfaceElement : ISurfaceElement
{
    // Interactive CSS Properties
    CssValue<CursorBuilder>? Cursor { get; set; }
    CssValue<FocusRingBuilder>? FocusRing { get; set; }
    CssValue<InteractionBuilder>? Interaction { get; set; }

    // Accessibility Properties
    int? TabIndex { get; set; }
    string? Role { get; set; }
    string? AriaLabel { get; set; }
    string? AriaDescribedBy { get; set; }

    // Interactive Events
    EventCallback<MouseEventArgs> OnClick { get; set; }
    EventCallback<MouseEventArgs> OnDoubleClick { get; set; }
    EventCallback<MouseEventArgs> OnMouseOver { get; set; }
    EventCallback<MouseEventArgs> OnMouseOut { get; set; }
    EventCallback<KeyboardEventArgs> OnKeyDown { get; set; }
    EventCallback<FocusEventArgs> OnFocus { get; set; }
    EventCallback<FocusEventArgs> OnBlur { get; set; }
}

