using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Soenneker.Lucide.Enums.Icons;

namespace Soenneker.Quark;

/// <summary>
/// Defines the copy button component.
/// </summary>
public interface ICopyButton
{
    /// <summary>
    /// Typed styling for the underlying button.
    /// </summary>
    QuarkPresetToken? Preset { get; set; }

    /// <summary>
    /// Text to copy to the clipboard.
    /// </summary>
    string? Text { get; set; }

    /// <summary>
    /// Function used to resolve text immediately before copying.
    /// </summary>
    Func<string?>? TextProvider { get; set; }

    /// <summary>
    /// Optional label content rendered after the icon.
    /// </summary>
    RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Visual style variant.
    /// </summary>
    ButtonVariant Variant { get; set; }

    /// <summary>
    /// Button size.
    /// </summary>
    CssValue<ButtonSizeBuilder>? ButtonSize { get; set; }

    /// <summary>
    /// Indicates whether the button is disabled.
    /// </summary>
    bool Disabled { get; set; }

    /// <summary>
    /// Indicates whether the button is rendered inside an input group.
    /// </summary>
    bool InputGroupButton { get; set; }

    /// <summary>
    /// Idle icon.
    /// </summary>
    LucideIcon IdleIcon { get; set; }

    /// <summary>
    /// Success icon.
    /// </summary>
    LucideIcon DoneIcon { get; set; }

    /// <summary>
    /// Error icon.
    /// </summary>
    LucideIcon ErrorIcon { get; set; }

    /// <summary>
    /// Button label used for accessible names and tooltips.
    /// </summary>
    string CopyLabel { get; set; }

    /// <summary>
    /// Label used after a successful copy.
    /// </summary>
    string DoneLabel { get; set; }

    /// <summary>
    /// Label used after a failed copy.
    /// </summary>
    string ErrorLabel { get; set; }

    /// <summary>
    /// How long the success or error state remains visible.
    /// </summary>
    int ResetDelay { get; set; }

    /// <summary>
    /// Additional button classes.
    /// </summary>
    string? Class { get; set; }

    /// <summary>
    /// Additional attributes applied to the underlying button.
    /// </summary>
    IReadOnlyDictionary<string, object>? Attributes { get; set; }

    /// <summary>
    /// Raised after text is copied successfully.
    /// </summary>
    EventCallback<string> OnCopySuccess { get; set; }

    /// <summary>
    /// Raised when copying fails.
    /// </summary>
    EventCallback<Exception?> OnCopyError { get; set; }
}
