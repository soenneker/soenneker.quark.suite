
namespace Soenneker.Quark;

/// <summary>
/// Represents the select options.
/// </summary>
public sealed class SelectOptions : ComponentOptions
{
    public SelectOptions()
    {
        Selector = "[data-slot='select']";
    }
}
