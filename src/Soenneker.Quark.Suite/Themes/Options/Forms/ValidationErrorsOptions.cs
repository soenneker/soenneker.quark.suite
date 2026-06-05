
namespace Soenneker.Quark;

/// <summary>
/// Configuration options for validation errors component styling.
/// </summary>
public sealed class ValidationErrorsOptions : ComponentOptions
{
    public ValidationErrorsOptions()
    {
        Selector = "[data-slot='validation-errors']";
    }
}
