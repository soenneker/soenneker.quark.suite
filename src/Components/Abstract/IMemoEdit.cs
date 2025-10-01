using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>
/// Represents a multi-line text area input component.
/// </summary>
public interface IMemoEdit : IElement
{
    /// <summary>
    /// Gets or sets the text value of the textarea.
    /// </summary>
    string? Value { get; set; }

    /// <summary>
    /// Gets or sets the placeholder text displayed when the textarea is empty.
    /// </summary>
    string? Placeholder { get; set; }

    /// <summary>
    /// Gets or sets whether the textarea is disabled.
    /// </summary>
    bool Disabled { get; set; }

    /// <summary>
    /// Gets or sets whether the textarea is read-only.
    /// </summary>
    bool ReadOnly { get; set; }

    /// <summary>
    /// Gets or sets whether the textarea is required.
    /// </summary>
    bool Required { get; set; }

    /// <summary>
    /// Gets or sets the number of visible text rows.
    /// </summary>
    int Rows { get; set; }

    /// <summary>
    /// Gets or sets the maximum number of characters allowed.
    /// </summary>
    int MaxLength { get; set; }

    /// <summary>
    /// Gets or sets the minimum number of characters required.
    /// </summary>
    int MinLength { get; set; }

    /// <summary>
    /// Gets or sets a regular expression pattern that the value must match.
    /// </summary>
    string? Pattern { get; set; }

    /// <summary>
    /// Gets or sets whether the textarea should automatically resize based on content.
    /// </summary>
    bool AutoResize { get; set; }

    /// <summary>
    /// Gets or sets the color scheme of the textarea.
    /// </summary>
    CssValue<ColorBuilder>? Color { get; set; }

    /// <summary>
    /// Gets or sets the size of the textarea.
    /// </summary>
    CssValue<SizeBuilder>? Size { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when the value changes.
    /// </summary>
    EventCallback<string> ValueChanged { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when the textarea value changes.
    /// </summary>
    EventCallback<ChangeEventArgs> OnChange { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked during input.
    /// </summary>
    EventCallback<ChangeEventArgs> OnInput { get; set; }
}