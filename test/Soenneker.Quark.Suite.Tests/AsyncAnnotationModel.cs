using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AnnotationResult = System.ComponentModel.DataAnnotations.ValidationResult;

namespace Soenneker.Quark.Suite.Tests;

internal sealed class AsyncAnnotationModel : IGeneratedQuarkValidation
{
    public string Name { get; set; } = "value";
    public bool HasAsyncValidation(string memberName) => true;
    public void ValidateField(string memberName, ICollection<AnnotationResult> results) => throw new InvalidOperationException("Async validation required");
    public async ValueTask ValidateFieldAsync(string memberName, ICollection<AnnotationResult> results, IServiceProvider? serviceProvider, CancellationToken cancellationToken)
    {
        await Task.Yield();
        cancellationToken.ThrowIfCancellationRequested();
        if (serviceProvider is null) throw new InvalidOperationException("Missing services");
        results.Add(new AnnotationResult("Remote annotation rejected value"));
    }
}
