using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>
/// Represents a single-line text input component.
/// </summary>
public interface ITextEdit : IElement
{
    /// <summary>
    /// Gets or sets the text value.
    /// </summary>
    string? Value { get; set; }

    /// <summary>
    /// Gets or sets the expression that identifies the bound value for validation.
    /// </summary>
    Expression<Func<string>>? ValueExpression { get; set; }

    /// <summary>
    /// Gets or sets the placeholder text displayed when the input is empty.
    /// </summary>
    string? Placeholder { get; set; }

    /// <summary>
    /// Gets or sets whether the input is disabled.
    /// </summary>
    bool Disabled { get; set; }

    /// <summary>
    /// Gets or sets whether the input is read-only.
    /// </summary>
    bool ReadOnly { get; set; }

    /// <summary>
    /// Gets or sets whether the input is required.
    /// </summary>
    bool Required { get; set; }

    /// <summary>
    /// Gets or sets the maximum number of characters allowed.
    /// </summary>
    int MaxLength { get; set; }

    /// <summary>
    /// Gets or sets the color scheme of the input.
    /// </summary>
    CssValue<ColorBuilder>? Color { get; set; }

    /// <summary>
    /// Gets or sets the size of the input.
    /// </summary>
    CssValue<SizeBuilder>? Size { get; set; }

    /// <summary>
    /// Gets or sets whether the input should be rendered as plain text.
    /// </summary>
    bool PlainText { get; set; }

    /// <summary>
    /// Gets or sets whether the input should use floating label styling.
    /// </summary>
    bool FloatingLabel { get; set; }

    /// <summary>
    /// Gets or sets the label text for the input.
    /// </summary>
    string? Label { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when the value changes.
    /// </summary>
    EventCallback<string?> ValueChanged { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when the input value changes.
    /// </summary>
    EventCallback<ChangeEventArgs> OnChange { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked during input.
    /// </summary>
    EventCallback<ChangeEventArgs> OnInput { get; set; }

    /// <summary>
    /// Gets the value used for validation purposes.
    /// </summary>
    object? ValidationValue { get; }
}