using System;
using System.Threading;
using System.Threading.Tasks;
using Soenneker.Quark.Base;

namespace Soenneker.Quark.Suite.Tests;

internal sealed class DelegateValidationProbe(Func<ValidationResult> validate, Func<Task<ValidationResult>>? validateAsync = null) : QuarkValidator
{
    public override ValidationResult Validate(object value) => validate();
    public override Task<ValidationResult> Validate(object value, CancellationToken cancellationToken = default) =>
        validateAsync is null ? base.Validate(value, cancellationToken) : validateAsync();
}
