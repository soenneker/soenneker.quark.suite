
namespace Soenneker.Quark;

/// <summary>
/// Configuration options for field body component styling.
/// </summary>
public sealed class FieldBodyOptions : ComponentOptions
{
    public FieldBodyOptions()
    {
        Selector = "[data-slot='field-content']";
    }
}
