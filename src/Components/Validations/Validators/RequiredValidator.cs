using Soenneker.Quark;

namespace Soenneker.Quark;

/// <summary>
/// Validator for required fields.
/// </summary>
public class RequiredValidator : BaseQuarkValidator
{
    /// <summary>
    /// Initializes a new instance of the RequiredValidator class.
    /// </summary>
    public RequiredValidator()
    {
        ErrorMessage = "This field is required.";
    }

    /// <summary>
    /// Initializes a new instance of the RequiredValidator class with a custom error message.
    /// </summary>
    /// <param name="errorMessage">The error message to display when validation fails.</param>
    public RequiredValidator(string errorMessage)
    {
        ErrorMessage = errorMessage;
    }

    /// <inheritdoc/>
    protected override bool ValidateValue(object value)
    {
        if (value is null)
            return false;

        if (value is string str)
            return !string.IsNullOrWhiteSpace(str);

        return true;
    }
}
