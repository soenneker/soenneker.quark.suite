using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Quark;

/// <summary>
/// Interface for custom validators that can be used with Quark validation components.
/// </summary>
public interface IQuarkValidator
{
    /// <summary>
    /// Validates the given value synchronously.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <returns>A <see cref="ValidationResult"/> containing the validation outcome.</returns>
    ValidationResult Validate(object value);

    /// <summary>
    /// Validates the given value asynchronously.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A <see cref="ValidationResult"/> containing the validation outcome.</returns>
    Task<ValidationResult> ValidateAsync(object value, CancellationToken cancellationToken = default);

    /// <summary>
    /// Validates the value with enhanced context using ValidatorEventArgs.
    /// </summary>
    /// <param name="args">The validation event arguments containing the value and validation context.</param>
    /// <returns>A <see cref="ValidationResult"/> containing the validation outcome.</returns>
    ValidationResult Validate(ValidatorEventArgs args);

    /// <summary>
    /// Validates the value asynchronously with enhanced context using ValidatorEventArgs.
    /// </summary>
    /// <param name="args">The validation event arguments containing the value and validation context.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A <see cref="ValidationResult"/> containing the validation outcome.</returns>
    Task<ValidationResult> ValidateAsync(ValidatorEventArgs args, CancellationToken cancellationToken = default);
}
