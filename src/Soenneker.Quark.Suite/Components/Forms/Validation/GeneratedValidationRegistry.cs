using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Collections.Generic;
using AnnotationResult = System.ComponentModel.DataAnnotations.ValidationResult;

namespace Soenneker.Quark;

/// <summary>Registers statically generated field validators. Registrations use the exact runtime model type.</summary>
public static class GeneratedValidationRegistry
{
    private static readonly ConcurrentDictionary<Type, Action<object, string, ICollection<AnnotationResult>>> _validators = new();

    /// <summary>Registers a validator for a model type. The validator must read the current model property and add its annotation results.</summary>
    public static void Register<T>(Action<T, string, ICollection<AnnotationResult>> validator)
    {
        ArgumentNullException.ThrowIfNull(validator);
        _validators[typeof(T)] = (model, memberName, results) => validator((T)model, memberName, results);
    }

    /// <summary>Validates one field using generated code. Throws when the model has no registration, rather than silently skipping validation.</summary>
    public static void Validate(object model, string memberName, ICollection<AnnotationResult> results)
    {
        ArgumentNullException.ThrowIfNull(model);
        ArgumentNullException.ThrowIfNull(memberName);
        ArgumentNullException.ThrowIfNull(results);
        if (model is IGeneratedQuarkValidation generated)
        {
            generated.ValidateField(memberName, results);
            return;
        }
        if (!_validators.TryGetValue(model.GetType(), out var validate))
            throw new InvalidOperationException($"No generated Quark validation exists for '{model.GetType()}'. Add Soenneker.Quark.Gen.Validation to the model project and rebuild. Models without validation attributes must opt in with [GenerateQuarkValidation].");
        validate(model, memberName, results);
    }
    /// <summary>Reports whether generated validation for the field must be awaited.</summary>
    public static bool HasAsyncValidation(object model, string memberName) =>
        model is IGeneratedQuarkValidation generated && generated.HasAsyncValidation(memberName);

    /// <summary>Validates a generated field, awaiting asynchronous annotations and supplying their services.</summary>
    public static ValueTask ValidateAsync(object model, string memberName, ICollection<AnnotationResult> results,
        IServiceProvider? serviceProvider = null, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(model);
        ArgumentNullException.ThrowIfNull(memberName);
        ArgumentNullException.ThrowIfNull(results);
        cancellationToken.ThrowIfCancellationRequested();
        if (model is IGeneratedQuarkValidation generated)
            return generated.ValidateFieldAsync(memberName, results, serviceProvider, cancellationToken);
        Validate(model, memberName, results);
        return ValueTask.CompletedTask;
    }
}
