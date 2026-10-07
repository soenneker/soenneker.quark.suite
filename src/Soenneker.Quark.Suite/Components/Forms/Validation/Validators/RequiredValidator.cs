using Soenneker.Extensions.String;
using Soenneker.Quark.Base;

namespace Soenneker.Quark;

/// <summary>
/// Validator for required fields.
/// </summary>
public sealed class RequiredValidator : QuarkValidator
{
    public override bool IsSynchronous => true;

    private readonly ValidationResult _errorResult;

    public RequiredValidator()
    {
        _errorResult = ValidationResult.Error("This field is required.");
    }

    public RequiredValidator(string errorMessage)
    {
        _errorResult = ValidationResult.Error(errorMessage);
    }

    /// <summary>
    /// Validates the given value to ensure it is not null or empty.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <returns>A <see cref="ValidationResult"/> indicating success or an error.</returns>
    public override ValidationResult Validate(object value)
    {
        if (value is null)
            return _errorResult;

        if (value is string str && str.IsNullOrWhiteSpace())
            return _errorResult;

        return ValidationResult.Success();
    }
}
