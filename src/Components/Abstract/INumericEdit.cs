using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>
/// Represents a numeric input component for entering decimal values.
/// </summary>
public interface INumericEdit : IElement
{
    /// <summary>
    /// Gets or sets the numeric value.
    /// </summary>
    decimal? Value { get; set; }

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
    /// Gets or sets the minimum value allowed.
    /// </summary>
    decimal? Min { get; set; }

    /// <summary>
    /// Gets or sets the maximum value allowed.
    /// </summary>
    decimal? Max { get; set; }

    /// <summary>
    /// Gets or sets the step increment for the numeric value.
    /// </summary>
    decimal? Step { get; set; }


    /// <summary>
    /// Gets or sets the size of the input.
    /// </summary>
    CssValue<SizeBuilder>? Size { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when the value changes.
    /// </summary>
    EventCallback<decimal?> ValueChanged { get; set; }

    /// <summary>
    /// Gets or sets the expression that identifies the bound value for validation.
    /// </summary>
    Expression<Func<decimal?>>? ValueExpression { get; set; }

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