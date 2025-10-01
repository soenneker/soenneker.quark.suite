using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Quark;

/// <summary>
/// Simple validator that uses a function for validation logic.
/// Much simpler than the complex adapter pattern.
/// </summary>
public class SimpleValidator : IQuarkValidator
{
    /// <summary>
    /// Gets the error message to display when validation fails.
    /// </summary>
    public string ErrorMessage { get; }
    
    private readonly Func<object?, bool> _validate;

    /// <summary>
    /// Initializes a new instance of the SimpleValidator class.
    /// </summary>
    /// <param name="errorMessage">The error message to display when validation fails.</param>
    /// <param name="validate">The validation function.</param>
    public SimpleValidator(string errorMessage, Func<object?, bool> validate)
    {
        ErrorMessage = errorMessage ?? throw new ArgumentNullException(nameof(errorMessage));
        _validate = validate ?? throw new ArgumentNullException(nameof(validate));
    }

    /// <summary>
    /// Gets the validation status after validation.
    /// </summary>
    public ValidationStatus Status { get; private set; } = ValidationStatus.None;

    /// <summary>
    /// Validates the given value.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <returns>True if valid, false otherwise.</returns>
    public bool Validate(object value)
    {
        var isValid = _validate(value);
        Status = isValid ? ValidationStatus.Success : ValidationStatus.Error;
        return isValid;
    }

    /// <summary>
    /// Validates the given value asynchronously.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if valid, false otherwise.</returns>
    public Task<bool> ValidateAsync(object value, CancellationToken cancellationToken = default)
    {
        var isValid = _validate(value);
        Status = isValid ? ValidationStatus.Success : ValidationStatus.Error;
        return Task.FromResult(isValid);
    }
}
