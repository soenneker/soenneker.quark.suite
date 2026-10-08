using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>
/// Represents a button component with support for different types, colors, sizes, and states.
/// </summary>
public interface IButton : IElement
{
    /// <summary>
    /// Gets or sets the browsing context used when the button renders as a link.
    /// </summary>
    Target? Target { get; set; }

    /// <summary>
    /// Gets or sets link relationship tokens when rendering as a link.
    /// </summary>
    string? Rel { get; set; }

    /// <summary>
    /// Gets or sets a link download filename. An empty string lets the browser choose.
    /// </summary>
    string? Download { get; set; }

    /// <summary>
    /// Gets or sets the BCP 47 language tag of the linked resource.
    /// </summary>
    string? HrefLang { get; set; }

    /// <summary>
    /// Gets or sets the referrer policy when rendering as a link.
    /// </summary>
    string? ReferrerPolicy { get; set; }


    /// <summary>
    /// Gets or sets the HTML formaction attribute.
    /// </summary>
    string? FormAction { get; set; }

    /// <summary>
    /// Gets or sets the HTML formenctype attribute.
    /// </summary>
    string? FormEncType { get; set; }

    /// <summary>
    /// Gets or sets the HTML formmethod attribute.
    /// </summary>
    string? FormMethod { get; set; }

    /// <summary>
    /// Gets or sets the HTML formnovalidate attribute.
    /// </summary>
    bool? FormNoValidate { get; set; }

    /// <summary>
    /// Gets or sets the HTML formtarget attribute.
    /// </summary>
    string? FormTarget { get; set; }

    /// <summary>
    /// Gets or sets the HTML popovertarget attribute.
    /// </summary>
    string? PopoverTarget { get; set; }

    /// <summary>
    /// Gets or sets the HTML popovertargetaction attribute.
    /// </summary>
    string? PopoverTargetAction { get; set; }


    /// <summary>
    /// Gets or sets the type of button (button, submit, reset, or link).
    /// </summary>
    ButtonType? Type { get; set; }

    /// <summary>
    /// Gets or sets whether the button is disabled.
    /// </summary>
    bool Disabled { get; set; }

    /// <summary>
    /// Gets or sets whether the button is in a loading state.
    /// </summary>
    bool Loading { get; set; }

    /// <summary>
    /// Gets or sets the template to display When the button is loading.
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
    /// Gets or sets the URL to navigate to (When Type is Link).
    /// </summary>
    string? Href { get; set; }

    /// <summary>
    /// Gets or sets the name attribute of the button.
    /// </summary>
    string? Name { get; set; }

    /// <summary>
    /// Gets or sets the size of the button (shadcn: default, xs, sm, lg, icon, icon-xs, icon-sm, icon-lg).
    /// </summary>
    CssValue<ButtonSizeBuilder>? ButtonSize { get; set; }

    /// <summary>
    /// Gets or sets the visual style variant (shadcn/ui).
    /// </summary>
    ButtonVariant Variant { get; set; }

    /// <summary>
    /// Gets or sets whether the button should span the full width of its container.
    /// </summary>
    bool Block { get; set; }

}
