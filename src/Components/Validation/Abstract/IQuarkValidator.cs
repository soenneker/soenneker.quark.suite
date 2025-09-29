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
    /// <returns>True if validation passes, false otherwise.</returns>
    bool Validate(object value);

    /// <summary>
    /// Validates the given value asynchronously.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if validation passes, false otherwise.</returns>
    Task<bool> ValidateAsync(object value, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the error message for validation failures.
    /// </summary>
    string ErrorMessage { get; }

    /// <summary>
    /// Gets the validation status after validation.
    /// </summary>
    ValidationStatus Status { get; }
}
