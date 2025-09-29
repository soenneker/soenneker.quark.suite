using Soenneker.Quark;

namespace Soenneker.Quark;

/// <summary>
/// Validator for maximum length requirements.
/// </summary>
public class MaxLengthValidator : BaseQuarkValidator
{
    private readonly int _maxLength;

    /// <summary>
    /// Initializes a new instance of the MaxLengthValidator class.
    /// </summary>
    /// <param name="maxLength">The maximum allowed length.</param>
    public MaxLengthValidator(int maxLength)
    {
        _maxLength = maxLength;
        ErrorMessage = $"The field must be no more than {maxLength} characters long.";
    }

    /// <summary>
    /// Initializes a new instance of the MaxLengthValidator class with a custom error message.
    /// </summary>
    /// <param name="maxLength">The maximum allowed length.</param>
    /// <param name="errorMessage">The error message to display when validation fails.</param>
    public MaxLengthValidator(int maxLength, string errorMessage)
    {
        _maxLength = maxLength;
        ErrorMessage = errorMessage;
    }

    /// <inheritdoc/>
    protected override bool ValidateValue(object value)
    {
        if (value is not string str)
            return true; // Non-string values are considered valid for max length

        return str.Length <= _maxLength;
    }
}
