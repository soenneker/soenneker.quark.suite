using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>
/// Shared base for toggle components (checkbox, switch, radio).
/// Provides common checked state and value binding.
/// </summary>
public abstract class ToggleElement : InteractiveSurface, IToggleElement
{
    /// <summary>
    /// Gets or sets whether the toggle is checked.
    /// </summary>
    [Parameter]
    public bool Checked { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when the checked state changes.
    /// </summary>
    [Parameter]
    public EventCallback<bool> CheckedChanged { get; set; }

    /// <summary>
    /// Gets or sets the expression that identifies the bound checked value for validation.
    /// </summary>
    [Parameter]
    public Expression<Func<bool>>? CheckedExpression { get; set; }

    /// <summary>
    /// Gets or sets whether the toggle is disabled.
    /// </summary>
    [Parameter]
    public bool Disabled { get; set; }

    /// <summary>
    /// Gets or sets the name attribute for form submission.
    /// </summary>
    [Parameter]
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the value attribute of the toggle.
    /// </summary>
    [Parameter]
    public string? Value { get; set; }
}

