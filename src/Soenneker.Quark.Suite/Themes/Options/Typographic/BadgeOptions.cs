
namespace Soenneker.Quark;

/// <summary>
/// Configuration options for badge component styling.
/// </summary>
public sealed class BadgeOptions : ComponentOptions
{
    public BadgeOptions()
    {
        Selector = "[data-slot='badge']";
    }
}
