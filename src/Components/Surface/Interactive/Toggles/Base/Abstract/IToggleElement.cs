using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>
/// Represents a toggle input component (checkbox, radio, switch) with checked state binding.
/// </summary>
public interface IToggleElement : IInteractiveSurfaceElement
{
    /// <summary>
    /// Gets or sets whether the toggle is checked.
    /// </summary>
    bool Checked { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked When the checked state changes.
    /// </summary>
    EventCallback<bool> CheckedChanged { get; set; }

    /// <summary>
    /// Gets or sets the expression that identifies the bound checked value for validation.
    /// </summary>
    Expression<Func<bool>>? CheckedExpression { get; set; }

    /// <summary>
    /// Gets or sets whether the toggle is disabled.
    /// </summary>
    bool Disabled { get; set; }

    /// <summary>
    /// Gets or sets the name attribute for form submission.
    /// </summary>
    string? Name { get; set; }

    /// <summary>
    /// Gets or sets the value attribute of the toggle.
    /// </summary>
    string? Value { get; set; }
}

