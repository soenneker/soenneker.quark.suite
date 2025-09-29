using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Quark;

internal sealed class PatternValidationHandler : IValidationHandler
{
    public void Validate(Validation ctx, object value)
    {
        if (ctx.Pattern is null)
        {
            ctx.NotifyValidationStatusChanged(ValidationStatus.None);
            return;
        }

        var isMatch = ctx.Pattern.IsMatch(value?.ToString() ?? string.Empty);
        ctx.NotifyValidationStatusChanged(isMatch ? ValidationStatus.Success : ValidationStatus.Error,
            isMatch ? null : new[] { "Value does not match the required pattern." });
    }

    public Task<ValidationStatus> ValidateAsync(Validation ctx, object value, CancellationToken cancellationToken)
    {
        Validate(ctx, value);
        return Task.FromResult(ctx.Status);
    }
}
