using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Quark;

/// <summary>
/// Base implementation of IValidator that provides common validation functionality.
/// </summary>
public abstract class QuarkValidator : IQuarkValidator
{
    public virtual string ErrorMessage { get; set; } = string.Empty;

    public ValidationStatus Status { get; private set; } = ValidationStatus.None;

    public virtual bool Validate(object value)
    {
        var result = ValidateValue(value);
        Status = result ? ValidationStatus.Success : ValidationStatus.Error;
        return result;
    }

    public virtual async Task<bool> ValidateAsync(object value, CancellationToken cancellationToken = default)
    {
        var result = await ValidateValueAsync(value, cancellationToken);
        Status = result ? ValidationStatus.Success : ValidationStatus.Error;
        return result;
    }

    /// <summary>
    /// Validates the value with enhanced context using ValidatorEventArgs. 
    /// This method provides access to rich validation context including custom error messages and member names.
    /// </summary>
    /// <param name="args">The validation event arguments containing the value and validation context.</param>
    /// <returns>True if validation passes, false otherwise.</returns>
    public virtual bool Validate(ValidatorEventArgs args)
    {
        var result = ValidateValue(args);
        Status = args.Status;
        return args.Status != ValidationStatus.Error;
    }

    /// <summary>
    /// Validates the value asynchronously with enhanced context using ValidatorEventArgs.
    /// </summary>
    /// <param name="args">The validation event arguments containing the value and validation context.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if validation passes, false otherwise.</returns>
    public virtual async Task<bool> ValidateAsync(ValidatorEventArgs args, CancellationToken cancellationToken = default)
    {
        var result = await ValidateValueAsync(args, cancellationToken);
        Status = args.Status;
        return args.Status != ValidationStatus.Error;
    }

    /// <summary>
    /// Validates the value synchronously. Override this method to implement custom validation logic.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <returns>True if validation passes, false otherwise.</returns>
    protected abstract bool ValidateValue(object value);

    /// <summary>
    /// Validates the value asynchronously. Override this method to implement custom async validation logic.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if validation passes, false otherwise.</returns>
    protected virtual Task<bool> ValidateValueAsync(object value, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(ValidateValue(value));
    }

    /// <summary>
    /// Validates the value with enhanced context. Override this method to implement custom validation logic
    /// with access to rich validation context including custom error messages and member names.
    /// </summary>
    /// <param name="args">The validation event arguments containing the value and validation context.</param>
    /// <returns>True if validation passes, false otherwise.</returns>
    protected virtual bool ValidateValue(ValidatorEventArgs args)
    {
        // Default implementation falls back to simple value validation
        var result = ValidateValue(args.Value);

        if (!result)
        {
            args.Status = ValidationStatus.Error;
            args.ErrorText = ErrorMessage;
        }
        else
        {
            args.Status = ValidationStatus.Success;
        }
        return result;
    }

    /// <summary>
    /// Validates the value asynchronously with enhanced context. Override this method to implement 
    /// custom async validation logic with access to rich validation context.
    /// </summary>
    /// <param name="args">The validation event arguments containing the value and validation context.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if validation passes, false otherwise.</returns>
    protected virtual Task<bool> ValidateValueAsync(ValidatorEventArgs args, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(ValidateValue(args));
    }
}
