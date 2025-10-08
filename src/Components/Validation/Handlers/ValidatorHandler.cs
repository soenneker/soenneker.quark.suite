using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

internal sealed class ValidatorHandler : IValidationHandler
{
    public void Validate(Validation ctx, object value)
    {
        var args = new ValidatorEventArgs(value);

        ctx.NotifyValidationStarted();
        
        if (ctx.Validator is not null)
        {
            // Use enhanced validation method if available (QuarkValidator)
            if (ctx.Validator is QuarkValidator baseValidator)
            {
                baseValidator.Validate(args);
            }
            else
            {
                // Fall back to simple validation for other validators
                ctx.Validator.Validate(value);
                args.Status = ctx.Validator.Status;
                args.ErrorText = ctx.Validator.Status == ValidationStatus.Error ? ctx.Validator.ErrorMessage : null;
            }
        }
        else if (ctx.Action is not null)
        {
            ctx.Action.Invoke(args);
        }

        if (args.Status == ValidationStatus.Error)
        {
            var messages = new List<string>();

            if (args.ErrorText.HasContent())
                messages.Add(args.ErrorText);

            ctx.NotifyValidationStatusChanged(ValidationStatus.Error, messages);
        }
        else
        {
            ctx.NotifyValidationStatusChanged(args.Status);
        }
    }

    public async Task<ValidationStatus> ValidateAsync(Validation ctx, object value, CancellationToken cancellationToken)
    {
        var args = new ValidatorEventArgs(value);

        ctx.NotifyValidationStarted();

        if (ctx.Validator is not null)
        {
            // Use enhanced validation method if available (QuarkValidator)
            if (ctx.Validator is QuarkValidator baseValidator)
            {
                await baseValidator.ValidateAsync(args, cancellationToken);
            }
            else
            {
                // Fall back to simple validation for other validators
                await ctx.Validator.ValidateAsync(value, cancellationToken);
                args.Status = ctx.Validator.Status;
                args.ErrorText = ctx.Validator.Status == ValidationStatus.Error ? ctx.Validator.ErrorMessage : null;
            }
        }
        else if (ctx.AsyncFunc is not null)
        {
            await ctx.AsyncFunc.Invoke(args, cancellationToken);
        }
        else if (ctx.Action is not null)
        {
            ctx.Action.Invoke(args);
        }

        if (args.Status == ValidationStatus.Error)
        {
            var messages = new List<string>();
            if (args.ErrorText.HasContent())
                messages.Add(args.ErrorText);
            ctx.NotifyValidationStatusChanged(ValidationStatus.Error, messages);
        }
        else
        {
            ctx.NotifyValidationStatusChanged(args.Status);
        }

        return ctx.Status;
    }
}
