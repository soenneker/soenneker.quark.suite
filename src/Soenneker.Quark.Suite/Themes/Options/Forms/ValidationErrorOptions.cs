
namespace Soenneker.Quark;

/// <summary>
/// Configuration options for validation error component styling.
/// </summary>
public sealed class ValidationErrorOptions : ComponentOptions
{
    /// <summary>
    /// Initializes a new instance of the ValidationErrorOptions class.
    /// </summary>
    public ValidationErrorOptions()
    {
        Selector = ".q-validation-error";
    }
}
