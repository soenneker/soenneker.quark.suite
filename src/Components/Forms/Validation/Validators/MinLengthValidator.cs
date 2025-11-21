namespace Soenneker.Quark;

/// <summary>
/// Validator for minimum length requirements.
/// </summary>
public sealed class MinLengthValidator : QuarkValidator
{
    private readonly int _minLength;
    private readonly string _errorMessage;

    /// <summary>
    /// Initializes a new instance of the MinLengthValidator class.
    /// </summary>
    /// <param name="minLength">The minimum required length.</param>
    public MinLengthValidator(int minLength)
    {
        _minLength = minLength;
        _errorMessage = $"The field must be at least {minLength} characters long.";
    }

    /// <summary>
    /// Initializes a new instance of the MinLengthValidator class with a custom error message.
    /// </summary>
    /// <param name="minLength">The minimum required length.</param>
    /// <param name="errorMessage">The error message to display when validation fails.</param>
    public MinLengthValidator(int minLength, string errorMessage)
    {
        _minLength = minLength;
        _errorMessage = errorMessage;
    }

    /// <summary>
    /// Validates the given value to ensure it meets the minimum length requirement.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <returns>A <see cref="ValidationResult"/> indicating success or an error.</returns>
    public override ValidationResult Validate(object value)
    {
        if (value is not string str)
            return ValidationResult.Error(_errorMessage);

        if (str.Length < _minLength)
            return ValidationResult.Error(_errorMessage);

        return ValidationResult.Success();
    }
}
