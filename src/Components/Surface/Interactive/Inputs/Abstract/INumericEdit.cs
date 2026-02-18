using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>
/// Represents a numeric input component for entering decimal values.
/// </summary>
public interface INumericEdit : IInput
{
    /// <summary>
    /// Gets or sets the numeric value.
    /// </summary>
    decimal? Value { get; set; }

    /// <summary>
    /// Gets or sets the minimum value allowed.
    /// </summary>
    decimal? Min { get; set; }

    /// <summary>
    /// Gets or sets the maximum value allowed.
    /// </summary>
    decimal? Max { get; set; }

    /// <summary>
    /// Gets or sets the step increment for the numeric value.
    /// </summary>
    decimal? Step { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked When the value changes.
    /// </summary>
    EventCallback<decimal?> ValueChanged { get; set; }
}