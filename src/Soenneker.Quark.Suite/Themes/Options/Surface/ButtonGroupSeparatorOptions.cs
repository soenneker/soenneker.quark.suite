namespace Soenneker.Quark;

/// <summary>
/// Represents the button group separator options.
/// </summary>
public sealed class ButtonGroupSeparatorOptions : ComponentOptions
{
    public ButtonGroupSeparatorOptions()
    {
        Selector = "[data-slot='button-group-separator']";
    }
}
