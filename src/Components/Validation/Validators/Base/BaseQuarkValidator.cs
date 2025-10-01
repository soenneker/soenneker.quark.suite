using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Quark;

/// <summary>
/// Base implementation of IValidator that provides common validation functionality.
/// </summary>
public abstract class BaseQuarkValidator : IQuarkValidator
{
    private ValidationStatus _status = ValidationStatus.None;

    public virtual string ErrorMessage { get; set; } = string.Empty;

    public ValidationStatus Status => _status;

    public virtual bool Validate(object value)
    {
        var result = ValidateValue(value);
        _status = result ? ValidationStatus.Success : ValidationStatus.Error;
        return result;
    }

    public virtual async Task<bool> ValidateAsync(object value, CancellationToken cancellationToken = default)
    {
        var result = await ValidateValueAsync(value, cancellationToken);
        _status = result ? ValidationStatus.Success : ValidationStatus.Error;
        return result;
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
}
