using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Quark.Base;

/// <summary>
/// Base implementation of IQuarkValidator that provides common validation functionality.
/// </summary>
public abstract class QuarkValidator : IQuarkValidator
{
    public abstract ValidationResult Validate(object value);

    public virtual Task<ValidationResult> Validate(object value, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(Validate(value));
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
