using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>
/// Represents a currency input component for entering nullable decimal values with display formatting.
/// </summary>
public interface ICurrencyInput : IInput
{
    /// <summary>
    /// Gets or sets the value.
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
    /// Gets or sets the callback invoked when the value changes.
    /// </summary>
    EventCallback<decimal?> ValueChanged { get; set; }

    /// <summary>
    /// Gets or sets the number of decimal places used for formatting and rounding.
    /// </summary>
    int DecimalPlaces { get; set; }
}
