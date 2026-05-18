using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;

namespace Soenneker.Quark;

internal sealed class DataAnnotationValidationHandler : IValidationHandler
{
    public void Validate(Validation ctx, object value)
    {
        if (ctx.EditContext is not null)
        {
            var store = new ValidationMessageStore(ctx.EditContext);
            var field = ctx.FieldIdentifier;
            store.Clear(field);

            var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(field.Model)
            {
                MemberName = field.FieldName
            };

            var propertyValue = field.Model?.GetType().GetProperty(field.FieldName)?.GetValue(field.Model);
            System.ComponentModel.DataAnnotations.Validator.TryValidateProperty(
                propertyValue,
                validationContext,
                results);

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
