using System.Threading;
using System.Threading.Tasks;
using Soenneker.Quark.Base;
namespace Soenneker.Quark;
public sealed class LocalizedValidator(IQuarkValidator inner, IQuarkLocalizer localizer, string messageKey) : QuarkValidator, ILocalizedValidator
{
    public override bool IsSynchronous => inner.IsSynchronous;
    private ValidationResult Translate(ValidationResult result) => result.Status == ValidationStatus.Error
        ? new ValidationResult { Status = result.Status, ErrorText = localizer[messageKey], MemberNames = result.MemberNames } : result;
    public override ValidationResult Validate(object value) => Translate(inner.Validate(value));
    public override async Task<ValidationResult> Validate(object value, CancellationToken cancellationToken = default) => Translate(await inner.Validate(value, cancellationToken));
    public override ValidationResult Validate(ValidatorEventArgs args)
    {
        var result = Translate(inner.Validate(args)); args.Status = result.Status; args.ErrorText = result.ErrorText; args.MemberNames = result.MemberNames; return result;
    }
    public override async Task<ValidationResult> Validate(ValidatorEventArgs args, CancellationToken cancellationToken = default)
    {
        var result = Translate(await inner.Validate(args, cancellationToken)); args.Status = result.Status; args.ErrorText = result.ErrorText; args.MemberNames = result.MemberNames; return result;
    }
}
