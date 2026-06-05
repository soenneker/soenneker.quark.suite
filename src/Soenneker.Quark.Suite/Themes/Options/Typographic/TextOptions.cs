
namespace Soenneker.Quark;

/// <summary>
/// Configuration options for text component styling.
/// </summary>
public sealed class TextOptions : ComponentOptions
{
    public TextOptions()
    {
        Selector = "[data-slot='text']";
    }
}
