
namespace Soenneker.Quark;

/// <summary>
/// Configuration options for validation error component styling.
/// </summary>
public sealed class ValidationErrorOptions : ComponentOptions
{
    public ValidationErrorOptions()
    {
        Selector = "[data-slot='field-error']";
    }
}
