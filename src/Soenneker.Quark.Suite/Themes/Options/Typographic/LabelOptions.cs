
namespace Soenneker.Quark;

/// <summary>
/// Configuration options for label component styling.
/// </summary>
public sealed class LabelOptions : ComponentOptions
{
    public LabelOptions()
    {
        Selector = "[data-slot='label']";
    }
}
