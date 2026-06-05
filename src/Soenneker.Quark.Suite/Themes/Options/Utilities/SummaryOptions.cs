
namespace Soenneker.Quark;

/// <summary>
/// Configuration options for summary component styling.
/// </summary>
public sealed class SummaryOptions : ComponentOptions
{
    public SummaryOptions()
    {
        Selector = "[data-slot='summary']";
    }
}
