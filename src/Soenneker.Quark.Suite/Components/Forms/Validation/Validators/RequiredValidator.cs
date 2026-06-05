using Soenneker.Extensions.String;
using Soenneker.Quark.Base;

namespace Soenneker.Quark;

/// <summary>
/// Validator for required fields.
/// </summary>
public sealed class RequiredValidator : QuarkValidator
{
    private readonly string _errorMessage;

    public RequiredValidator()
    {
        _errorMessage = "This field is required.";
    }

    public RequiredValidator(string errorMessage)
    {
        _errorMessage = errorMessage;
    }

    /// <summary>
    /// Validates the given value to ensure it is not null or empty.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <returns>A <see cref="ValidationResult"/> indicating success or an error.</returns>
    public override ValidationResult Validate(object value)
    {
        if (value is null)
            return ValidationResult.Error(_errorMessage);

        if (value is string str && str.IsNullOrWhiteSpace())
            return ValidationResult.Error(_errorMessage);

        return ValidationResult.Success();
    }
}
