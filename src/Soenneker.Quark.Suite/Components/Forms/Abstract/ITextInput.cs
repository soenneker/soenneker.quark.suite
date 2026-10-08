using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>
/// Represents a single-line text input component.
/// </summary>
public interface ITextInput : IInput
{
    /// <summary>Gets or sets the CaretColor utilities, including responsive and state variants.</summary>
    CssValue<CaretColorBuilder>? CaretColor { get; set; }

    /// <summary>Gets or sets the FieldSizing utilities, including responsive and state variants.</summary>
    CssValue<FieldSizingBuilder>? FieldSizing { get; set; }

    /// <summary>
    /// Gets or sets the ID of the owning form, including a form outside the control ancestry.
    /// </summary>
    string? Form { get; set; }

    /// <summary>
    /// Gets or sets browser autofill tokens, such as off, email, or section-shipping shipping street-address.
    /// </summary>
    string? AutoComplete { get; set; }

    /// <summary>
    /// Gets or sets the HTML minlength attribute.
    /// </summary>
    int? MinLength { get; set; }

    /// <summary>
    /// Gets or sets the HTML dirname attribute.
    /// </summary>
    string? DirName { get; set; }

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    string? Value { get; set; }
    
    /// <summary>
    /// Gets or sets the maximum number of characters allowed.
    /// </summary>
    int MaxLength { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when the value changes.
    /// </summary>
    EventCallback<string?> ValueChanged { get; set; }

    /// <summary>
    /// Gets or sets the input mode hint for mobile keyboards.
    /// </summary>
    TextInputMode? InputMode { get; set; }

    /// <summary>
    /// Gets or sets the edit mask pattern used for native input validation.
    /// </summary>
    string? EditMask { get; set; }
}
