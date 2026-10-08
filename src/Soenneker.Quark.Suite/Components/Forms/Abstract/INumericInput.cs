using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>
/// Represents a numeric input component for entering decimal values.
/// </summary>
public interface INumericInput : IInput
{
    /// <summary>
    /// Gets or sets the ID of the owning form, including a form outside the control ancestry.
    /// </summary>
    string? Form { get; set; }

    /// <summary>
    /// Gets or sets browser autofill tokens, such as off, email, or section-shipping shipping street-address.
    /// </summary>
    string? AutoComplete { get; set; }


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
    /// Gets or sets the step increment for the numeric value.
    /// </summary>
    decimal? Step { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when the value changes.
    /// </summary>
    EventCallback<decimal?> ValueChanged { get; set; }
}
