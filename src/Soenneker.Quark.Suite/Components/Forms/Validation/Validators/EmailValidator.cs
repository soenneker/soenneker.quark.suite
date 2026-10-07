using System.Text.RegularExpressions;
using Soenneker.Extensions.String;
using Soenneker.Quark.Base;

namespace Soenneker.Quark;

/// <summary>
/// Validator for email addresses.
/// </summary>
public class EmailValidator : QuarkValidator
{
    public override bool IsSynchronous => true;

    private static readonly Regex _emailRegex = new(@"^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,6}$", RegexOptions.IgnoreCase);

    private readonly ValidationResult _errorResult;

    public EmailValidator()
    {
        _errorResult = ValidationResult.Error("Please enter a valid email address.");
    }

    public EmailValidator(string errorMessage)
    {
        _errorResult = ValidationResult.Error(errorMessage);
    }

    /// <summary>
    /// Validates the given value as an email address.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <returns>A <see cref="ValidationResult"/> containing the validation outcome.</returns>
    public override ValidationResult Validate(object value)
    {
        if (value is not string email)
            return _errorResult;

        if (!email.HasContent() || !_emailRegex.IsMatch(email))
            return _errorResult;

        return ValidationResult.Success();
    }
}
