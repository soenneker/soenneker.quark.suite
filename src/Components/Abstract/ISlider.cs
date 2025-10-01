using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>
/// Represents a range slider component for selecting a numeric value within a range.
/// </summary>
public interface ISlider : IElement
{
    /// <summary>
    /// Gets or sets the current value of the slider.
    /// </summary>
    double Value { get; set; }

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
    /// Gets or sets whether the slider is disabled.
    /// </summary>
    bool Disabled { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when the value changes.
    /// </summary>
    EventCallback<double> ValueChanged { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when the slider value changes.
    /// </summary>
    EventCallback<ChangeEventArgs> OnChange { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked during slider movement.
    /// </summary>
    EventCallback<ChangeEventArgs> OnInput { get; set; }
}

