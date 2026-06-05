
namespace Soenneker.Quark;

/// <summary>
/// Configuration options for line break component styling.
/// </summary>
public sealed class BrOptions : ComponentOptions
{
    public BrOptions()
    {
        Selector = "[data-slot='br']";
    }
}
