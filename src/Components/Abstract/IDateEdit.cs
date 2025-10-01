using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;
using Soenneker.Quark.Enums;

namespace Soenneker.Quark;

/// <summary>
/// Represents a date input component for selecting dates, date-times, or months.
/// </summary>
public interface IDateEdit : IElement
{
    /// <summary>
    /// Gets or sets the DateTime value.
    /// </summary>
    DateTime? Date { get; set; }

    /// <summary>
    /// Gets or sets the expression that identifies the bound DateTime value for validation.
    /// </summary>
    Expression<Func<DateTime>>? DateExpression { get; set; }

    /// <summary>
    /// Gets or sets the DateOnly value.
    /// </summary>
    DateOnly? DateOnly { get; set; }

    /// <summary>
    /// Gets or sets the expression that identifies the bound DateOnly value for validation.
    /// </summary>
    Expression<Func<DateOnly>>? DateOnlyExpression { get; set; }

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
    /// Gets or sets the minimum DateTime value allowed.
    /// </summary>
    DateTime Min { get; set; }

    /// <summary>
    /// Gets or sets the maximum DateTime value allowed.
    /// </summary>
    DateTime Max { get; set; }

    /// <summary>
    /// Gets or sets the minimum DateOnly value allowed.
    /// </summary>
    DateOnly MinDateOnly { get; set; }

    /// <summary>
    /// Gets or sets the maximum DateOnly value allowed.
    /// </summary>
    DateOnly MaxDateOnly { get; set; }

    /// <summary>
    /// Gets or sets the step increment for date selection.
    /// </summary>
    int Step { get; set; }

    /// <summary>
    /// Gets or sets the input mode (date, datetime-local, month).
    /// </summary>
    DateInputMode InputMode { get; set; }

    /// <summary>
    /// Gets or sets the color scheme of the input.
    /// </summary>
    CssValue<ColorBuilder>? Color { get; set; }

    /// <summary>
    /// Gets or sets the size of the input.
    /// </summary>
    CssValue<SizeBuilder>? Size { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when the DateTime value changes.
    /// </summary>
    EventCallback<DateTime?> DateChanged { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when the DateOnly value changes.
    /// </summary>
    EventCallback<DateOnly?> DateOnlyChanged { get; set; }

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