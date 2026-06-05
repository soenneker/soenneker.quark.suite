
namespace Soenneker.Quark;

/// <summary>
/// Configuration options for span component styling.
/// </summary>
public sealed class SpanOptions : ComponentOptions
{
    public SpanOptions()
    {
        Selector = "[data-slot='span']";
    }
}
