
namespace Soenneker.Quark;

/// <summary>
/// Configuration options for validation success component styling.
/// </summary>
public sealed class ValidationSuccessOptions : ComponentOptions
{
    public ValidationSuccessOptions()
    {
        Selector = "[data-slot='validation-success']";
    }
}
