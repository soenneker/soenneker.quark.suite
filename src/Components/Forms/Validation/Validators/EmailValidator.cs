using System.Text.RegularExpressions;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

/// <summary>
/// Validator for email addresses.
/// </summary>
public class EmailValidator : QuarkValidator
{
    private static readonly Regex EmailRegex = new(@"^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,6}$", RegexOptions.IgnoreCase);
    
    private readonly string _errorMessage;

    /// <summary>
    /// Initializes a new instance of the EmailValidator class.
    /// </summary>
    public EmailValidator()
    {
        _errorMessage = "Please enter a valid email address.";
    }

    /// <summary>
    /// Initializes a new instance of the EmailValidator class with a custom error message.
    /// </summary>
    /// <param name="errorMessage">The error message to display when validation fails.</param>
    public EmailValidator(string errorMessage)
    {
        _errorMessage = errorMessage;
    }

    /// <summary>
    /// Validates the given value as an email address.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <returns>A <see cref="ValidationResult"/> containing the validation outcome.</returns>
    public override ValidationResult Validate(object value)
    {
        if (value is not string email)
            return ValidationResult.Error(_errorMessage);

        if (!email.HasContent() || !EmailRegex.IsMatch(email))
            return ValidationResult.Error(_errorMessage);

        return ValidationResult.Success();
    }
}
