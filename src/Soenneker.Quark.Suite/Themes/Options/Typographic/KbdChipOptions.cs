
namespace Soenneker.Quark;

/// <summary>
/// Configuration options for keyboard chip component styling.
/// </summary>
public sealed class KbdChipOptions : ComponentOptions
{
    public KbdChipOptions()
    {
        Selector = "[data-slot='kbd-chip']";
    }
}
