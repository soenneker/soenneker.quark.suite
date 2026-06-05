using Soenneker.Quark.Base;

namespace Soenneker.Quark;

/// <summary>
/// Validator for maximum length requirements.
/// </summary>
public sealed class MaxLengthValidator : QuarkValidator
{
    private readonly int _maxLength;
    private readonly string _errorMessage;

    public MaxLengthValidator(int maxLength)
    {
        _maxLength = maxLength;
        _errorMessage = $"The field must be no more than {maxLength} characters long.";
    }

    public MaxLengthValidator(int maxLength, string errorMessage)
    {
        _maxLength = maxLength;
        _errorMessage = errorMessage;
    }

    /// <summary>
    /// Validates the given value to ensure it does not exceed the maximum length.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <returns>A <see cref="ValidationResult"/> indicating success or an error.</returns>
    public override ValidationResult Validate(object value)
    {
        if (value is not string str)
            return ValidationResult.Success(); // Non-string values are considered valid for max length

        if (str.Length > _maxLength)
            return ValidationResult.Error(_errorMessage);

        return ValidationResult.Success();
    }
}
