
namespace Soenneker.Quark;

/// <summary>
/// Configuration options for field component styling.
/// </summary>
public sealed class FieldOptions : ComponentOptions
{
    public FieldOptions()
    {
        Selector = "[data-slot='field']";
    }
}
