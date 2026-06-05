
namespace Soenneker.Quark;

/// <summary>
/// Represents the tab options.
/// </summary>
public sealed class TabOptions : ComponentOptions
{
    public TabOptions()
    {
        Selector = "[data-slot='tabs-trigger']";
    }
}
