using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Quark.Base;

/// <inheritdoc cref="IQuarkValidator"/>
public abstract class QuarkValidator : IQuarkValidator
{
    public virtual bool IsSynchronous => false;

    public abstract ValidationResult Validate(object value);

    public virtual Task<ValidationResult> Validate(object value, CancellationToken cancellationToken = default)
    {
        return Validate(value).AsTask();
    }

    public virtual ValidationResult Validate(ValidatorEventArgs args)
    {
        var result = Validate(args.Value);

        // Sync the result with ValidatorEventArgs for backwards compatibility
        args.Status = result.Status;
        args.ErrorText = result.ErrorText;
        args.MemberNames = result.MemberNames;

        return result;
    }

    public virtual Task<ValidationResult> Validate(ValidatorEventArgs args, CancellationToken cancellationToken = default)
    {
        return Validate(args.Value, cancellationToken);
    }
}
