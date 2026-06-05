
namespace Soenneker.Quark;

/// <summary>
/// Represents the dropdown item options.
/// </summary>
public sealed class DropdownItemOptions : ComponentOptions
{
    public DropdownItemOptions()
    {
        Selector = "[data-slot='dropdown-menu-item']";
    }
}
