using System;

namespace Soenneker.Quark;

public static class ValidationHandlerType
{
    public static readonly Type Validator = typeof(ValidatorHandler);
    public static readonly Type Pattern = typeof(PatternValidationHandler);
    public static readonly Type DataAnnotation = typeof(DataAnnotationValidationHandler);
}
