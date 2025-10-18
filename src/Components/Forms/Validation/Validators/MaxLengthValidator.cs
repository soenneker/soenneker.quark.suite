namespace Soenneker.Quark;

/// <summary>
/// Validator for maximum length requirements.
/// </summary>
public sealed class MaxLengthValidator : QuarkValidator
{
    private readonly int _maxLength;
    private readonly string _errorMessage;

    /// <summary>
    /// Initializes a new instance of the MaxLengthValidator class.
    /// </summary>
    /// <param name="maxLength">The maximum allowed length.</param>
    public MaxLengthValidator(int maxLength)
    {
        _maxLength = maxLength;
        _errorMessage = $"The field must be no more than {maxLength} characters long.";
    }

    /// <summary>
    /// Initializes a new instance of the MaxLengthValidator class with a custom error message.
    /// </summary>
    /// <param name="maxLength">The maximum allowed length.</param>
    /// <param name="errorMessage">The error message to display when validation fails.</param>
    public MaxLengthValidator(int maxLength, string errorMessage)
    {
        _maxLength = maxLength;
        _errorMessage = errorMessage;
    }

    public override ValidationResult Validate(object value)
    {
        if (value is not string str)
            return ValidationResult.Success(); // Non-string values are considered valid for max length

        if (str.Length > _maxLength)
            return ValidationResult.Error(_errorMessage);

        return ValidationResult.Success();
    }
}
