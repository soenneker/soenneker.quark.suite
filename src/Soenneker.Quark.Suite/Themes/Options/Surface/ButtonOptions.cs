namespace Soenneker.Quark;

/// <summary>
/// Represents the button options.
/// </summary>
public sealed class ButtonOptions : ComponentOptions
{
    public ButtonOptions()
    {
        Selector = "[data-slot='button']";
    }
}
