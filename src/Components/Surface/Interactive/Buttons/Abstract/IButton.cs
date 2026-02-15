using Microsoft.AspNetCore.Components;
using Soenneker.Quark;
using Soenneker.Quark.Enums;

namespace Soenneker.Quark.Components.Surface.Interactive.Buttons.Abstract;

/// <summary>
/// Represents a button component with support for different types, colors, sizes, and states.
/// </summary>
public interface IButton : IElement
{
    /// <summary>
    /// Gets or sets the type of button (button, submit, reset, or link).
    /// </summary>
    ButtonType Type { get; set; }

    /// <summary>
    /// Gets or sets whether the button is disabled.
    /// </summary>
    bool Disabled { get; set; }

    /// <summary>
    /// Gets or sets whether the button is in a loading state.
    /// </summary>
    bool Loading { get; set; }

    /// <summary>
    /// Gets or sets the template to display when the button is loading.
    /// </summary>
    RenderFragment? LoadingTemplate { get; set; }

    /// <summary>
    /// Gets or sets the value attribute of the button.
    /// </summary>
    string? Value { get; set; }

    /// <summary>
    /// Gets or sets the form attribute to associate the button with a form element.
    /// </summary>
    string? Form { get; set; }

    /// <summary>
    /// Gets or sets whether the button should automatically receive focus when the page loads.
    /// </summary>
    bool AutoFocus { get; set; }

    /// <summary>
    /// Gets or sets the URL to navigate to (when Type is Link).
    /// </summary>
    string? To { get; set; }

    /// <summary>
    /// Gets or sets the name attribute of the button.
    /// </summary>
    string? Name { get; set; }

    /// <summary>
    /// Gets or sets the size of the button (shadcn: default, xs, sm, lg, icon, icon-xs, icon-sm, icon-lg).
    /// </summary>
    ButtonSize Size { get; set; }

    /// <summary>
    /// Gets or sets the visual style variant (shadcn/ui).
    /// </summary>
    ButtonVariant Variant { get; set; }

    /// <summary>
    /// Gets or sets whether the button should span the full width of its container.
    /// </summary>
    bool Block { get; set; }

    /// <summary>
    /// Gets or sets the aria-label for accessibility.
    /// </summary>
    string? AriaLabel { get; set; }
}