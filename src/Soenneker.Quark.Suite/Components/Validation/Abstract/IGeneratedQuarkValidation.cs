using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using AnnotationResult = System.ComponentModel.DataAnnotations.ValidationResult;

namespace Soenneker.Quark;

/// <summary>Provides generated, reflection-free property validation for a Quark form model.</summary>
public interface IGeneratedQuarkValidation
{
    /// <summary>Reads the named public property and adds its data-annotation errors. Required failures short-circuit the remaining attributes.</summary>
    /// <remarks>Model-level attributes and IValidatableObject are intentionally not invoked, matching Quark's field validation.</remarks>
    void ValidateField(string memberName, ICollection<AnnotationResult> results);
    /// <summary>Reports whether the field has asynchronous annotations.</summary>
    bool HasAsyncValidation(string memberName) => false;

    /// <summary>Validates a field asynchronously with services and cancellation available to its annotations.</summary>
    ValueTask ValidateFieldAsync(string memberName, ICollection<AnnotationResult> results, IServiceProvider? serviceProvider, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ValidateField(memberName, results);
        return ValueTask.CompletedTask;
    }
}
