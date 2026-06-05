
namespace Soenneker.Quark;

/// <summary>
/// Configuration options for field help component styling.
/// </summary>
public sealed class FieldHelpOptions : ComponentOptions
{
    public FieldHelpOptions()
    {
        Selector = "[data-slot='field-description']";
    }
}
