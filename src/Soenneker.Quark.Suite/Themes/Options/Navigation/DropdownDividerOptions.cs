
namespace Soenneker.Quark;

/// <summary>
/// Represents the dropdown divider options.
/// </summary>
public sealed class DropdownDividerOptions : ComponentOptions
{
    public DropdownDividerOptions()
    {
        Selector = "[data-slot='dropdown-menu-separator']";
    }
}
