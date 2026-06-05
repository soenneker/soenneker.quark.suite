
namespace Soenneker.Quark;

/// <summary>
/// Represents the dropdown menu options.
/// </summary>
public sealed class DropdownMenuOptions : ComponentOptions
{
    public DropdownMenuOptions()
    {
        Selector = "[data-slot='dropdown-menu-content']";
    }
}
