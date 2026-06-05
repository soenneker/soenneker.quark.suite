
namespace Soenneker.Quark;

/// <summary>
/// Configuration options for horizontal rule component styling.
/// </summary>
public sealed class HrOptions : ComponentOptions
{
    public HrOptions()
    {
        Selector = "[data-slot='separator']";
    }
}
