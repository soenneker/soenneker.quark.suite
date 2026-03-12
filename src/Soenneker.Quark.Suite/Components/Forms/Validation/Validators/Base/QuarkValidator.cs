using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Quark;

/// <summary>
/// Base implementation of IQuarkValidator that provides common validation functionality.
/// </summary>
public abstract class QuarkValidator : IQuarkValidator
{
    /// <summary>
    /// Validates the value synchronously. Override this method to implement custom validation logic.
    /// </summary>
    public abstract ValidationResult Validate(object value);

    /// <summary>
    /// Validates the value asynchronously. Override this method to implement custom async validation logic.
    /// </summary>
    public virtual Task<ValidationResult> ValidateAsync(object value, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(Validate(value));
    }

    /// <summary>
    /// Validates the value with enhanced context. Override this method to implement custom validation logic
    /// with access to rich validation context including custom error messages and member names.
    /// </summary>
    public virtual ValidationResult Validate(ValidatorEventArgs args)
    {
        var result = Validate(args.Value);
        
        // Sync the result with ValidatorEventArgs for backwards compatibility
        args.Status = result.Status;
        args.ErrorText = result.ErrorText;
        args.MemberNames = result.MemberNames;
        
        return result;
    }

    /// <summary>
    /// Validates the value asynchronously with enhanced context. Override this method to implement 
    /// custom async validation logic with access to rich validation context.
    /// </summary>
    public virtual Task<ValidationResult> ValidateAsync(ValidatorEventArgs args, CancellationToken cancellationToken = default)
    {
        return ValidateAsync(args.Value, cancellationToken);
    }
}
