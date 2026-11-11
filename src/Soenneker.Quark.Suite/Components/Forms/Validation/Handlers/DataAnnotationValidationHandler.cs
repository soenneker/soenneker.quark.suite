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

            var results = ctx.GetAnnotationResults();
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

    public async Task<ValidationStatus> Validate(Validation ctx, object value, CancellationToken cancellationToken)
    {
        if (ctx.EditContext is null)
        {
            ctx.NotifyValidationStatusChanged(ValidationStatus.None);
            return ctx.Status;
        }
        var context = ctx.EditContext;
        var field = ctx.FieldIdentifier;
        ctx.GetDataAnnotationMessageStore();
        var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
        await GeneratedValidationRegistry.ValidateAsync(field.Model, field.FieldName, results, ctx.ValidationServices, cancellationToken);
        cancellationToken.ThrowIfCancellationRequested();
        if (!ReferenceEquals(context, ctx.EditContext) || !field.Equals(ctx.FieldIdentifier))
            return ctx.Status;
        List<string>? messages = null;
        foreach (var result in results)
            (messages ??= new List<string>(results.Count)).Add(result.ErrorMessage ?? "Invalid");
        var store = ctx.GetDataAnnotationMessageStore();
        if (messages is not null) store.Add(field, messages);
        ctx.NotifyValidationStatusChanged(messages is null ? ValidationStatus.Success : ValidationStatus.Error, messages);
        context.NotifyValidationStateChanged();
        return ctx.Status;
    }
}
