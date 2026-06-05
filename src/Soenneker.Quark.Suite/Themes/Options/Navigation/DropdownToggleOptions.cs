
namespace Soenneker.Quark;

/// <summary>
/// Represents the dropdown toggle options.
/// </summary>
public sealed class DropdownToggleOptions : ComponentOptions
{
    public DropdownToggleOptions()
    {
        Selector = "[data-slot='dropdown-menu-trigger']";
    }
}
