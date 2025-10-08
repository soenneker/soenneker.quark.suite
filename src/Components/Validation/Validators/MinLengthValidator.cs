namespace Soenneker.Quark;

/// <summary>
/// Validator for minimum length requirements.
/// </summary>
public class MinLengthValidator : QuarkValidator
{
    private readonly int _minLength;

    /// <summary>
    /// Initializes a new instance of the MinLengthValidator class.
    /// </summary>
    /// <param name="minLength">The minimum required length.</param>
    public MinLengthValidator(int minLength)
    {
        _minLength = minLength;
        ErrorMessage = $"The field must be at least {minLength} characters long.";
    }

    /// <summary>
    /// Initializes a new instance of the MinLengthValidator class with a custom error message.
    /// </summary>
    /// <param name="minLength">The minimum required length.</param>
    /// <param name="errorMessage">The error message to display when validation fails.</param>
    public MinLengthValidator(int minLength, string errorMessage)
    {
        _minLength = minLength;
        ErrorMessage = errorMessage;
    }

    /// <inheritdoc/>
    protected override bool ValidateValue(object value)
    {
        if (value is not string str)
            return false;

        return str.Length >= _minLength;
    }
}
