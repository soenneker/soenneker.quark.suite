using System.Collections.Generic;
using AnnotationResult = System.ComponentModel.DataAnnotations.ValidationResult;

namespace Soenneker.Quark;

/// <summary>Provides generated, reflection-free property validation for a Quark form model.</summary>
public interface IGeneratedQuarkValidation
{
    /// <summary>Reads the named public property and adds its data-annotation errors. Required failures short-circuit the remaining attributes.</summary>
    /// <remarks>Model-level attributes and IValidatableObject are intentionally not invoked, matching Quark's field validation.</remarks>
    void ValidateField(string memberName, ICollection<AnnotationResult> results);
}
