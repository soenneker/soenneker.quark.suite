using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Soenneker.Lucide.Enums.Icons;

namespace Soenneker.Quark;

/// <summary>
/// Defines the copy field component.
/// </summary>
public interface ICopyField
{
    /// <summary>
    /// Typed styling for the field root.
    /// </summary>
    QuarkPresetToken? Preset { get; set; }

    /// <summary>
    /// Typed styling for the displayed value.
    /// </summary>
    QuarkPresetToken? TextPreset { get; set; }

    /// <summary>
    /// Typed styling for the copy button.
    /// </summary>
    QuarkPresetToken? CopyButtonPreset { get; set; }

    /// <summary>
    /// Gets or sets the root id.
    /// </summary>
    string? Id { get; set; }

    /// <summary>
    /// Gets or sets the displayed value.
    /// </summary>
    string? Value { get; set; }

    /// <summary>
    /// Optional text copied instead of <see cref="Value"/>.
    /// </summary>
    string? CopyText { get; set; }

    /// <summary>
    /// Gets or sets the placeholder text.
    /// </summary>
    string? Placeholder { get; set; }

    /// <summary>
    /// Indicates whether the field and copy button are disabled.
    /// </summary>
    bool Disabled { get; set; }

    /// <summary>
    /// Additional root field classes.
    /// </summary>
    string? Class { get; set; }

    /// <summary>
    /// Additional root field styles.
    /// </summary>
    string? Style { get; set; }

    /// <summary>
    /// Additional text classes.
    /// </summary>
    string? TextClass { get; set; }

    /// <summary>
    /// Additional copy button classes.
    /// </summary>
    string? CopyButtonClass { get; set; }

    /// <summary>
    /// Accessible field label.
    /// </summary>
    string? AriaLabel { get; set; }

    /// <summary>
    /// Element id that labels the field.
    /// </summary>
    string? AriaLabelledBy { get; set; }

    /// <summary>
    /// Element id that describes the field.
    /// </summary>
    string? AriaDescribedBy { get; set; }

    /// <summary>
    /// Idle copy icon.
    /// </summary>
    LucideIcon IdleIcon { get; set; }

    /// <summary>
    /// Successful copy icon.
    /// </summary>
    LucideIcon DoneIcon { get; set; }

    /// <summary>
    /// Failed copy icon.
    /// </summary>
    LucideIcon ErrorIcon { get; set; }

    /// <summary>
    /// Copy button variant.
    /// </summary>
    ButtonVariant CopyButtonVariant { get; set; }

    /// <summary>
    /// Copy button size.
    /// </summary>
    CssValue<ButtonSizeBuilder>? CopyButtonSize { get; set; }

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
    /// Indicates whether the copy button should be disabled when no copy text is available.
    /// </summary>
    bool DisableCopyWhenEmpty { get; set; }

    /// <summary>
    /// Additional attributes applied to the root field.
    /// </summary>
    IReadOnlyDictionary<string, object>? Attributes { get; set; }

    /// <summary>
    /// Additional attributes applied to the display text.
    /// </summary>
    IReadOnlyDictionary<string, object>? TextAttributes { get; set; }

    /// <summary>
    /// Additional attributes applied to the copy button.
    /// </summary>
    IReadOnlyDictionary<string, object>? CopyButtonAttributes { get; set; }

    /// <summary>
    /// Raised after text is copied successfully.
    /// </summary>
    EventCallback<string> OnCopySuccess { get; set; }

    /// <summary>
    /// Raised when copying fails.
    /// </summary>
    EventCallback<Exception?> OnCopyError { get; set; }
}
