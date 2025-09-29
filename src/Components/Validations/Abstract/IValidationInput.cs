using Soenneker.Quark;

namespace Soenneker.Quark;

/// <summary>
/// Abstraction for input components that can participate in a validation.
/// </summary>
public interface IValidationInput
{
    /// <summary>
    /// The value to be validated. This should reflect the current input value.
    /// </summary>
    object? ValidationValue { get; }

    /// <summary>
    /// Indicates whether the input is disabled. Disabled inputs are skipped by validators.
    /// </summary>
    bool Disabled { get; }
}
