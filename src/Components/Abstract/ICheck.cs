using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>
/// Represents a checkbox input component.
/// </summary>
public interface ICheck : IElement
{
    /// <summary>
    /// Gets or sets whether the checkbox is checked.
    /// </summary>
    bool Checked { get; set; }

    /// <summary>
    /// Gets or sets whether the checkbox is in an indeterminate state.
    /// </summary>
    bool Indeterminate { get; set; }

    /// <summary>
    /// Gets or sets whether the checkbox is disabled.
    /// </summary>
    bool Disabled { get; set; }

    /// <summary>
    /// Gets or sets the color scheme of the checkbox.
    /// </summary>
    CssValue<ColorBuilder> Color { get; set; }

    /// <summary>
    /// Gets or sets the size of the checkbox.
    /// </summary>
    CssValue<SizeBuilder>? Size { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when the checked state changes.
    /// </summary>
    EventCallback<bool> CheckedChanged { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when the checkbox value changes.
    /// </summary>
    EventCallback<ChangeEventArgs> OnChange { get; set; }

    /// <summary>
    /// Gets or sets the expression that identifies the bound checked value for validation.
    /// </summary>
    Expression<Func<bool>>? CheckedExpression { get; set; }

    /// <summary>
    /// Gets the value used for validation purposes.
    /// </summary>
    object? ValidationValue { get; }
}