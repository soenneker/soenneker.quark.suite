using System;

namespace Soenneker.Quark;

internal static class ValidationHandlerFactory
{
    private static readonly ValidatorHandler _validator = new();
    private static readonly PatternValidationHandler _pattern = new();
    private static readonly DataAnnotationValidationHandler _dataAnnotation = new();
    public static IValidationHandler Create(Type type)
    {
        if (type == ValidationHandlerType.Validator)
            return _validator;

        if (type == ValidationHandlerType.Pattern)
            return _pattern;

        if (type == ValidationHandlerType.DataAnnotation)
            return _dataAnnotation;

        throw new NotSupportedException();
    }
}
