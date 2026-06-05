
namespace Soenneker.Quark;

/// <summary>
/// Represents the dropdown options.
/// </summary>
public sealed class DropdownOptions : ComponentOptions
{
    public DropdownOptions()
    {
        Selector = "[data-slot='dropdown-menu']";
    }
}
