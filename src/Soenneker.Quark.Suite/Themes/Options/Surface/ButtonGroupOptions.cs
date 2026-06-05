namespace Soenneker.Quark;

/// <summary>
/// Represents the button group options.
/// </summary>
public sealed class ButtonGroupOptions : ComponentOptions
{
    public ButtonGroupOptions()
    {
        Selector = "[data-slot='button-group']";
    }
}
