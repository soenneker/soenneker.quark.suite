using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>
/// Represents a radio button input component.
/// </summary>
public interface IRadio : IElement
{
    /// <summary>
    /// Gets or sets whether the radio button is checked.
    /// </summary>
    bool Checked { get; set; }

    /// <summary>
    /// Gets or sets whether the radio button is disabled.
    /// </summary>
    bool Disabled { get; set; }

    /// <summary>
    /// Gets or sets the value attribute of the radio button.
    /// </summary>
    string? Value { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when the checked state changes.
    /// </summary>
    EventCallback<bool> CheckedChanged { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when the radio button is selected.
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

