namespace Soenneker.Quark;

/// <summary>
/// Represents the thead options.
/// </summary>
public sealed class TheadOptions : ComponentOptions
{
    public TheadOptions()
    {
        Selector = "[data-slot='table-header']";
    }
}
