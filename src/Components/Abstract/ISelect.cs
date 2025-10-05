using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>
/// Represents a select dropdown component for choosing from a list of options.
/// </summary>
/// <typeparam name="TValue">The type of the selected value.</typeparam>
public interface ISelect<TValue> : IElement
{
    /// <summary>
    /// Gets or sets the selected value.
    /// </summary>
    TValue? SelectedValue { get; set; }

    /// <summary>
    /// Gets or sets the expression that identifies the bound value for validation.
    /// </summary>
    Expression<Func<TValue>>? SelectedValueExpression { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when the selected value changes.
    /// </summary>
    EventCallback<TValue?> SelectedValueChanged { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when the selection changes.
    /// </summary>
    EventCallback<ChangeEventArgs> OnChange { get; set; }

    /// <summary>
    /// Gets or sets whether the select is disabled.
    /// </summary>
    bool Disabled { get; set; }

    /// <summary>
    /// Gets or sets whether the select is required.
    /// </summary>
    bool Required { get; set; }

    /// <summary>
    /// Gets or sets the size of the select.
    /// </summary>
    CssValue<SizeBuilder>? Size { get; set; }

    /// <summary>
    /// Gets or sets whether multiple selection is allowed.
    /// </summary>
    bool Multiple { get; set; }

    /// <summary>
    /// Gets or sets the placeholder text displayed when no option is selected.
    /// </summary>
    string? Placeholder { get; set; }

    /// <summary>
    /// Gets or sets the default item text.
    /// </summary>
    string? DefaultItemText { get; set; }

    /// <summary>
    /// Gets or sets the default item value.
    /// </summary>
    TValue? DefaultItemValue { get; set; }

    /// <summary>
    /// Gets the value used for validation purposes.
    /// </summary>
    object? ValidationValue { get; }
}
