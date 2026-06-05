
namespace Soenneker.Quark;

/// <summary>
/// Configuration options for heading component styling.
/// </summary>
public sealed class HeadingOptions : ComponentOptions
{
    public HeadingOptions()
    {
        Selector = "[data-slot='heading']";
    }
}
