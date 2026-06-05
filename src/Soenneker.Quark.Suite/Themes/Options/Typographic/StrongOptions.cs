
namespace Soenneker.Quark;

/// <summary>
/// Configuration options for strong text component styling.
/// </summary>
public sealed class StrongOptions : ComponentOptions
{
    public StrongOptions()
    {
        Selector = "[data-slot='strong']";
    }
}
