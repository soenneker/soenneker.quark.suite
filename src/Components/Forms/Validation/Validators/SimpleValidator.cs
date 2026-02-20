using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Quark;

/// <summary>
/// Simple validator that uses a function for validation logic.
/// </summary>
public class SimpleValidator : IQuarkValidator
{
    private readonly string _errorMessage;
    private readonly Func<object?, bool> _validate;

    /// <summary>
    /// Initializes a new instance of the SimpleValidator class.
    /// </summary>
    /// <param name="errorMessage">The error message to display When validation fails.</param>
    /// <param name="validate">The validation function that returns true if valid, false if invalid.</param>
    public SimpleValidator(string errorMessage, Func<object?, bool> validate)
    {
        _errorMessage = errorMessage ?? throw new ArgumentNullException(nameof(errorMessage));
        _validate = validate ?? throw new ArgumentNullException(nameof(validate));
    }

    /// <summary>
    /// Validates the given value.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <returns>A <see cref="ValidationResult"/> containing the validation outcome.</returns>
    public ValidationResult Validate(object value)
    {
        var isValid = _validate(value);
        return isValid ? ValidationResult.Success() : ValidationResult.Error(_errorMessage);
    }

    /// <summary>
    /// Validates the given value asynchronously.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A <see cref="ValidationResult"/> containing the validation outcome.</returns>
    public Task<ValidationResult> ValidateAsync(object value, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(Validate(value));
    }

    /// <summary>
    /// Validates the value with enhanced context using ValidatorEventArgs.
    /// </summary>
    /// <param name="args">The validation event arguments containing the value and validation context.</param>
    /// <returns>A <see cref="ValidationResult"/> containing the validation outcome.</returns>
    public ValidationResult Validate(ValidatorEventArgs args)
    {
        var result = Validate(args.Value);
        
        // Sync the result with ValidatorEventArgs for backwards compatibility
        args.Status = result.Status;
        args.ErrorText = result.ErrorText;
        args.MemberNames = result.MemberNames;
        
        return result;
    }

    /// <summary>
    /// Validates the value asynchronously with enhanced context using ValidatorEventArgs.
    /// </summary>
    /// <param name="args">The validation event arguments containing the value and validation context.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A <see cref="ValidationResult"/> containing the validation outcome.</returns>
    public Task<ValidationResult> ValidateAsync(ValidatorEventArgs args, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(Validate(args));
    }
}
