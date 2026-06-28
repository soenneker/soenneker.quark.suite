namespace Soenneker.Quark;

/// <summary>
/// Represents the dropdown shortcut options.
/// </summary>
public sealed class DropdownShortcutOptions : ComponentOptions
{
    public DropdownShortcutOptions()
    {
        Selector = "[data-slot='dropdown-menu-shortcut']";
    }
}
