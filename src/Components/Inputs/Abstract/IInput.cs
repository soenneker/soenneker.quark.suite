using Microsoft.AspNetCore.Components;
using System;
using System.Linq.Expressions;

namespace Soenneker.Quark;

/// <summary>
/// Represents a base input component with common input properties and functionality.
/// </summary>
public interface IInput : IElement
{
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

    /// <summary>
    /// Gets the validation class for the input based on validation status.
    /// </summary>
    string? GetValidationClass();

    /// <summary>
    /// Gets the color class for the input based on background color.
    /// </summary>
    string? GetColorClass();

    /// <summary>
    /// Gets the size class for the input based on size.
    /// </summary>
    string? GetSizeClass();
}

