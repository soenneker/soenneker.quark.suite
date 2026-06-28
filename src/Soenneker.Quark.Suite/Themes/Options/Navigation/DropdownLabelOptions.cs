namespace Soenneker.Quark;

/// <summary>
/// Represents the dropdown label options.
/// </summary>
public sealed class DropdownLabelOptions : ComponentOptions
{
    public DropdownLabelOptions()
    {
        Selector = "[data-slot='dropdown-menu-label']";
    }
}
