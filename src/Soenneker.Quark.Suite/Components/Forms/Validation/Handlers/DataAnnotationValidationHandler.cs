using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Quark;

internal sealed class DataAnnotationValidationHandler : IValidationHandler
{
    public void Validate(Validation ctx, object value)
    {
        if (ctx.EditContext is not null)
        {
            var store = ctx.GetDataAnnotationMessageStore();
            var field = ctx.FieldIdentifier;

            var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
            GeneratedValidationRegistry.Validate(field.Model, field.FieldName, results);

            List<string>? messages = null;

            foreach (var r in results)
            {
                var message = r.ErrorMessage ?? "Invalid";
                messages ??= new List<string>(results.Count);
                messages.Add(message);
                store.Add(field, message);
            }

            if (messages is not null)
                ctx.NotifyValidationStatusChanged(ValidationStatus.Error, messages);
            else
                ctx.NotifyValidationStatusChanged(ValidationStatus.Success);

            ctx.EditContext.NotifyValidationStateChanged();
        }
        else
        {
            ctx.NotifyValidationStatusChanged(ValidationStatus.None);
        }
    }

    public Task<ValidationStatus> Validate(Validation ctx, object value, CancellationToken cancellationToken)
    {
        Validate(ctx, value);
        return Task.FromResult(ctx.Status);
    }
}
