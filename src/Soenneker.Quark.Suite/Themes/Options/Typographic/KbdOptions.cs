
namespace Soenneker.Quark;

/// <summary>
/// Configuration options for keyboard input component styling.
/// </summary>
public sealed class KbdOptions : ComponentOptions
{
    public KbdOptions()
    {
        Selector = "[data-slot='kbd']";
    }
}
