
namespace Soenneker.Quark;

/// <summary>
/// Configuration options for lead text component styling.
/// </summary>
public sealed class LeadOptions : ComponentOptions
{
    public LeadOptions()
    {
        Selector = "[data-slot='lead']";
    }
}
