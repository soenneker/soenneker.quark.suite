using Microsoft.AspNetCore.Components;
using Soenneker.Quark.Enums;

namespace Soenneker.Quark;

/// <summary>
/// Represents a single-line text input component.
/// </summary>
public interface ITextEdit : IInput
{
    /// <summary>
    /// Gets or sets the text value.
    /// </summary>
    string? Text { get; set; }
    
    /// <summary>
    /// Gets or sets the maximum number of characters allowed.
    /// </summary>
    int MaxLength { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when the text value changes.
    /// </summary>
    EventCallback<string?> TextChanged { get; set; }

    /// <summary>
    /// Gets or sets the input mode hint for mobile keyboards.
    /// </summary>
    TextInputMode? InputMode { get; set; }
}