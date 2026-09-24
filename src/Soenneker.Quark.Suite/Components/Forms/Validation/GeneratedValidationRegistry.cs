using System;
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
}
