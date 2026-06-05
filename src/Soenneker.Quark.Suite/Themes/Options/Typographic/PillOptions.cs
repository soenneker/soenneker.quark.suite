
namespace Soenneker.Quark;

/// <summary>
/// Configuration options for pill component styling.
/// </summary>
public sealed class PillOptions : ComponentOptions
{
    public PillOptions()
    {
        Selector = "[data-slot='pill']";
    }
}
