using System;

namespace Soenneker.Quark;

/// <summary>
/// Provides static type references for validation handler types.
/// </summary>
public static class ValidationHandlerType
{
    /// <summary>
    /// Gets the type for the validator handler.
    /// </summary>
    public static readonly Type Validator = typeof(ValidatorHandler);
    /// <summary>
    /// Gets the type for the pattern validation handler.
    /// </summary>
    public static readonly Type Pattern = typeof(PatternValidationHandler);
    /// <summary>
    /// Gets the type for the data annotation validation handler.
    /// </summary>
    public static readonly Type DataAnnotation = typeof(DataAnnotationValidationHandler);
}
