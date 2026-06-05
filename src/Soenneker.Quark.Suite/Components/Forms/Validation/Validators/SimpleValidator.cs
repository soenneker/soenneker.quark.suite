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

    public SimpleValidator(string errorMessage, Func<object?, bool> validate)
    {
        _errorMessage = errorMessage ?? throw new ArgumentNullException(nameof(errorMessage));
        _validate = validate ?? throw new ArgumentNullException(nameof(validate));
    }

    public ValidationResult Validate(object value)
    {
        var isValid = _validate(value);
        return isValid ? ValidationResult.Success() : ValidationResult.Error(_errorMessage);
    }

    public Task<ValidationResult> Validate(object value, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(Validate(value));
    }

    public ValidationResult Validate(ValidatorEventArgs args)
    {
        var result = Validate(args.Value);
        
        // Sync the result with ValidatorEventArgs for backwards compatibility
        args.Status = result.Status;
        args.ErrorText = result.ErrorText;
        args.MemberNames = result.MemberNames;
        
        return result;
    }

    public Task<ValidationResult> Validate(ValidatorEventArgs args, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(Validate(args));
    }
}
