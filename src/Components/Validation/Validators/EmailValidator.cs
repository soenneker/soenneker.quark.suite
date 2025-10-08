using System.Text.RegularExpressions;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

/// <summary>
/// Validator for email addresses.
/// </summary>
public class EmailValidator : QuarkValidator
{
    private static readonly Regex EmailRegex = new(@"^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,6}$", RegexOptions.IgnoreCase);

    /// <summary>
    /// Initializes a new instance of the EmailValidator class.
    /// </summary>
    public EmailValidator()
    {
        ErrorMessage = "Please enter a valid email address.";
    }

    /// <summary>
    /// Initializes a new instance of the EmailValidator class with a custom error message.
    /// </summary>
    /// <param name="errorMessage">The error message to display when validation fails.</param>
    public EmailValidator(string errorMessage)
    {
        ErrorMessage = errorMessage;
    }

    /// <inheritdoc/>
    protected override bool ValidateValue(object value)
    {
        if (value is not string email)
            return false;

        return email.HasContent() && EmailRegex.IsMatch(email);
    }
}
