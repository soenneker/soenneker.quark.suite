using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>
/// Represents a date picker component with a calendar dropdown for selecting dates.
/// </summary>
public interface IDatePicker : IElement
{
    /// <summary>Gets or sets whether date changes are saved automatically.</summary>
    bool AutoSave { get; set; }

    /// <summary>Gets or sets the debounce delay in milliseconds. Defaults to 750.</summary>
    int AutoSaveDelay { get; set; }

    /// <summary>Gets or sets whether losing trigger focus saves pending changes. Defaults to true.</summary>
    bool AutoSaveOnBlur { get; set; }

    /// <summary>Gets or sets whether Enter saves pending changes. Defaults to false.</summary>
    bool AutoSaveOnEnter { get; set; }

    /// <summary>Gets or sets whether the save status is displayed. Defaults to true.</summary>
    bool ShowAutoSaveStatus { get; set; }

    /// <summary>Gets or sets the callback that saves the selected date with cancellation support.</summary>
    Func<DateOnly?, CancellationToken, ValueTask>? OnAutoSave { get; set; }

    /// <summary>Gets or sets the callback invoked when the save state changes.</summary>
    EventCallback<AutoSaveState> AutoSaveStateChanged { get; set; }

    /// <summary>Gets or sets the CSS class for the save status.</summary>
    string? AutoSaveStatusClass { get; set; }

    /// <summary>Gets or sets the CSS class for the saving status.</summary>
    string? AutoSaveSavingClass { get; set; }

    /// <summary>Gets or sets the CSS class for the saved status.</summary>
    string? AutoSaveSavedClass { get; set; }

    /// <summary>Gets or sets the CSS class for the failed status.</summary>
    string? AutoSaveFailedClass { get; set; }
    /// <summary>
    /// Gets or sets the selected date value.
    /// </summary>
    DateOnly? SelectedDate { get; set; }

    /// <summary>
    /// Gets or sets the expression that identifies the bound value for validation.
    /// </summary>
    Expression<Func<DateOnly?>>? SelectedDateExpression { get; set; }

    /// <summary>
    /// Gets or sets whether the input is required.
    /// </summary>
    bool Required { get; set; }

    /// <summary>
    /// Gets or sets the minimum date value allowed.
    /// </summary>
    DateOnly? Min { get; set; }

    /// <summary>
    /// Gets or sets the maximum date value allowed.
    /// </summary>
    DateOnly? Max { get; set; }

    /// <summary>
    /// Gets or sets the size of the input.
    /// </summary>
    CssValue<InputSizeBuilder>? InputSize { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked When the value changes.
    /// </summary>
    EventCallback<DateOnly?> SelectedDateChanged { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked When the input value changes.
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
