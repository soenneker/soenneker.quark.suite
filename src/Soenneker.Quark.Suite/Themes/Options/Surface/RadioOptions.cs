
namespace Soenneker.Quark;

/// <summary>
/// Represents the radio options.
/// </summary>
public sealed class RadioOptions : ComponentOptions
{
    public RadioOptions()
    {
        Selector = "[data-slot='radio-group-item']";
    }
}
