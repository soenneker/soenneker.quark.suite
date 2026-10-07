using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Quark;

/// <inheritdoc cref="IQuarkValidator"/>
public class SimpleValidator : IQuarkValidator
{
    public virtual bool IsSynchronous => true;

    private readonly ValidationResult _errorResult;
    private readonly Func<object?, bool> _validate;

    public SimpleValidator(string errorMessage, Func<object?, bool> validate)
    {
        _errorResult = ValidationResult.Error(errorMessage ?? throw new ArgumentNullException(nameof(errorMessage)));
        _validate = validate ?? throw new ArgumentNullException(nameof(validate));
    }

    public ValidationResult Validate(object value)
    {
        var isValid = _validate(value);
        return isValid ? ValidationResult.Success() : _errorResult;
    }

    public Task<ValidationResult> Validate(object value, CancellationToken cancellationToken = default)
    {
        return Validate(value).AsTask();
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
        return Validate(args).AsTask();
    }
}
