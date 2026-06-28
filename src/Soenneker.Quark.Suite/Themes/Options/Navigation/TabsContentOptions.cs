namespace Soenneker.Quark;

/// <summary>
/// Represents the tabs content options.
/// </summary>
public sealed class TabsContentOptions : ComponentOptions
{
    public TabsContentOptions()
    {
        Selector = "[data-slot='tabs-content']";
    }
}
