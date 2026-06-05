
namespace Soenneker.Quark;

/// <summary>
/// Represents the nav options.
/// </summary>
public sealed class NavOptions : ComponentOptions
{
    public NavOptions()
    {
        Selector = "[data-slot='nav']";
    }
}
