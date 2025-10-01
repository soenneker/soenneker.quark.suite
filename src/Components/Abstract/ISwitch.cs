using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>
/// Represents a toggle switch component for boolean values.
/// </summary>
public interface ISwitch : IElement
{
    /// <summary>
    /// Gets or sets whether the switch is checked (on).
    /// </summary>
    bool Checked { get; set; }

    /// <summary>
    /// Gets or sets whether the switch is disabled.
    /// </summary>
    bool Disabled { get; set; }

    /// <summary>
    /// Gets or sets the label text displayed next to the switch.
    /// </summary>
    string? Label { get; set; }

    /// <summary>
    /// Gets or sets the color scheme of the switch.
    /// </summary>
    CssValue<ColorBuilder>? Color { get; set; }

    /// <summary>
    /// Gets or sets the size of the switch.
    /// </summary>
    CssValue<SizeBuilder>? Size { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when the checked state changes.
    /// </summary>
    EventCallback<bool> CheckedChanged { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when the switch is toggled.
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

