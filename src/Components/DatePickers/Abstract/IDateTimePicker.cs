using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>
/// Represents a date-time picker component with calendar and time selection dropdowns.
/// </summary>
public interface IDateTimePicker : IElement
{
    /// <summary>
    /// Gets or sets the selected date-time value.
    /// </summary>
    DateTime? Value { get; set; }

    /// <summary>
    /// Gets or sets the expression that identifies the bound value for validation.
    /// </summary>
    Expression<Func<DateTime?>>? ValueExpression { get; set; }

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
    /// Gets or sets the minimum date-time value allowed.
    /// </summary>
    DateTime? Min { get; set; }

    /// <summary>
    /// Gets or sets the maximum date-time value allowed.
    /// </summary>
    DateTime? Max { get; set; }

    /// <summary>
    /// Gets or sets the background color scheme of the input.
    /// </summary>
    CssValue<BackgroundColorBuilder>? BackgroundColor { get; set; }

    /// <summary>
    /// Gets or sets the size of the input.
    /// </summary>
    CssValue<SizeBuilder>? Size { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when the value changes.
    /// </summary>
    EventCallback<DateTime?> ValueChanged { get; set; }

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

