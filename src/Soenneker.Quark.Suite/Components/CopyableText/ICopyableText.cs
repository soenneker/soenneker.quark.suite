using Soenneker.Lucide.Enums.Icons;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
namespace Soenneker.Quark;

/// <summary>Displays text with a clipboard action and optional field appearance.</summary>
public interface ICopyableText : IElement
{
    /// <summary>Optional destination for the action beside Copy, such as a mailto: or tel: URI. The action is shown when this is nonempty or OnAction has a delegate.</summary>
    string? ActionHref { get; set; }
    /// <summary>Icon displayed in the optional action. Defaults to ExternalLink.</summary>
    LucideIcon ActionIcon { get; set; }
    /// <summary>Accessible label and tooltip for the optional action. Defaults to Open.</summary>
    string ActionLabel { get; set; }
    /// <summary>Disables only the optional action.</summary>
    bool ActionDisabled { get; set; }
    /// <summary>Invoked when the optional action is clicked. If ActionHref is also set, the link also retains its normal browser navigation.</summary>
    EventCallback<MouseEventArgs> OnAction { get; set; }
    /// <summary>Text displayed by the component.</summary>
    string? Text { get; set; }
    /// <summary>Optional text copied instead of <see cref="Text"/>.</summary>
    string? CopyText { get; set; }
    /// <summary>Indicates whether overflowing text is truncated with an ellipsis.</summary>
    bool TruncateText { get; set; }
    /// <summary>Exposes the full text as a native tooltip when truncation is enabled.</summary>
    bool ShowFullTextOnHover { get; set; }
    /// <summary>Keeps the copy button visible instead of revealing it on hover or keyboard focus.</summary>
    bool AlwaysShowCopyButton { get; set; }
    /// <summary>Indicates whether both copying and the optional action are disabled.</summary>
    bool Disabled { get; set; }
    /// <summary>Additional classes applied to the displayed text.</summary>
    string? TextClass { get; set; }
    /// <summary>Additional classes applied to the copy button.</summary>
    string? CopyButtonClass { get; set; }
    /// <summary>Gets or sets copy button variant.</summary>
    ButtonVariant CopyButtonVariant { get; set; }
    /// <summary>Gets or sets copy button size.</summary>
    CssValue<ButtonSizeBuilder>? CopyButtonSize { get; set; }
    /// <summary>Gets or sets idle icon.</summary>
    LucideIcon IdleIcon { get; set; }
    /// <summary>Gets or sets done icon.</summary>
    LucideIcon DoneIcon { get; set; }
    /// <summary>Gets or sets error icon.</summary>
    LucideIcon ErrorIcon { get; set; }
    /// <summary>Gets or sets copy label.</summary>
    string CopyLabel { get; set; }
    /// <summary>Gets or sets done label.</summary>
    string DoneLabel { get; set; }
    /// <summary>Gets or sets error label.</summary>
    string ErrorLabel { get; set; }
    /// <summary>Gets or sets reset delay.</summary>
    int ResetDelay { get; set; }
    /// <summary>Additional attributes applied to the displayed text.</summary>
    IReadOnlyDictionary<string, object>? TextAttributes { get; set; }
    /// <summary>Additional attributes applied to the copy button.</summary>
    IReadOnlyDictionary<string, object>? CopyButtonAttributes { get; set; }
    /// <summary>Gets or sets on copy success.</summary>
    EventCallback<string> OnCopySuccess { get; set; }
    /// <summary>Gets or sets on copy error.</summary>
    EventCallback<Exception?> OnCopyError { get; set; }
    /// <summary>Text displayed when Text is null. The placeholder is never copied.</summary>
    string? Placeholder { get; set; }
    /// <summary>Disables copying when the resolved copy text is empty. Defaults to true.</summary>
    bool DisableCopyWhenEmpty { get; set; }
    /// <summary>Uses a compact field appearance with an always-visible copy action.</summary>
    bool FieldAppearance { get; set; }
    /// <summary>Preset applied to the displayed text.</summary>
    QuarkPresetToken? TextPreset { get; set; }
    /// <summary>Preset applied to the copy button.</summary>
    QuarkPresetToken? CopyButtonPreset { get; set; }
}
