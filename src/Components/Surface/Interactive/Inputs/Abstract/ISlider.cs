using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>
/// Represents a range slider component for selecting a numeric value within a range.
/// </summary>
public interface ISlider : IInput
{
    /// <summary>
    /// Gets or sets the current value of the slider.
    /// </summary>
    double Value { get; set; }

    /// <summary>
    /// Gets or sets the expression that identifies the bound value for validation.
    /// </summary>
    Expression<Func<double>>? ValueExpression { get; set; }

    /// <summary>
    /// Gets or sets the minimum value of the slider range.
    /// </summary>
    double Min { get; set; }

    /// <summary>
    /// Gets or sets the maximum value of the slider range.
    /// </summary>
    double Max { get; set; }

    /// <summary>
    /// Gets or sets the step increment for the slider value.
    /// </summary>
    double Step { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked When the value changes.
    /// </summary>
    EventCallback<double> ValueChanged { get; set; }
}