using Soenneker.Quark.Base;

namespace Soenneker.Quark;

/// <summary>
/// Validator for minimum length requirements.
/// </summary>
public sealed class MinLengthValidator : QuarkValidator
{
    private readonly int _minLength;
    private readonly ValidationResult _errorResult;

    public MinLengthValidator(int minLength)
    {
        _minLength = minLength;
        _errorResult = ValidationResult.Error($"The field must be at least {minLength} characters long.");
    }

    public MinLengthValidator(int minLength, string errorMessage)
    {
        _minLength = minLength;
        _errorResult = ValidationResult.Error(errorMessage);
    }

    /// <summary>
    /// Validates the given value to ensure it meets the minimum length requirement.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <returns>A <see cref="ValidationResult"/> indicating success or an error.</returns>
    public override ValidationResult Validate(object value)
    {
        if (value is not string str)
            return _errorResult;

        if (str.Length < _minLength)
            return _errorResult;

        return ValidationResult.Success();
    }
}
