
namespace Soenneker.Quark;

/// <summary>
/// Configuration options for muted text component styling.
/// </summary>
public sealed class MutedOptions : ComponentOptions
{
    public MutedOptions()
    {
        Selector = "[data-slot='muted']";
    }
}
