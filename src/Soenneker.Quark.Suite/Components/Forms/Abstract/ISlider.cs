using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;

using Soenneker.Bradix;

namespace Soenneker.Quark;

/// <summary>
/// Represents a range slider component for selecting a numeric value within a range.
/// </summary>
public interface ISlider
{
    /// <summary>
    /// Gets or sets the current value of the slider.
    /// </summary>
    double SliderValue { get; set; }

    /// <summary>
    /// Gets or sets the expression that identifies the bound value for validation.
    /// </summary>
    Expression<Func<double>>? SliderValueExpression { get; set; }

    /// <summary>
    /// Gets or sets the current collection of slider thumb values.
    /// </summary>
    double[]? Values { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when the thumb values change.
    /// </summary>
    EventCallback<double[]> ValuesChanged { get; set; }

    /// <summary>
    /// Gets or sets the expression that identifies the bound thumb values for validation.
    /// </summary>
    Expression<Func<double[]?>>? ValuesExpression { get; set; }

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
    EventCallback<double> SliderValueChanged { get; set; }

    /// <summary>
    /// Gets or sets the slider orientation.
    /// </summary>
    Orientation Orientation { get; set; }
}
