
using System.Collections.Generic;

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

    /// <summary>
    /// Gets or sets divider styling scoped to the dropdown.
    /// </summary>
    public DropdownDividerOptions? Dividers { get; set; }

    /// <summary>
    /// Gets or sets item styling scoped to the dropdown.
    /// </summary>
    public DropdownItemOptions? Items { get; set; }

    /// <summary>
    /// Gets or sets label styling scoped to the dropdown.
    /// </summary>
    public DropdownLabelOptions? Labels { get; set; }

    /// <summary>
    /// Gets or sets menu content styling scoped to the dropdown.
    /// </summary>
    public DropdownMenuOptions? Menus { get; set; }

    /// <summary>
    /// Gets or sets shortcut styling scoped to the dropdown.
    /// </summary>
    public DropdownShortcutOptions? Shortcuts { get; set; }

    /// <summary>
    /// Gets or sets trigger styling scoped to the dropdown.
    /// </summary>
    public DropdownToggleOptions? Toggles { get; set; }

    private protected override void CollectChildCssRules(List<ComponentCssRule> buffer, string baseSelector)
    {
        string dropdownScope = baseSelector == "[data-slot='dropdown-menu']" ? string.Empty : baseSelector;

        AddChildCssRules(buffer, Dividers, "[data-slot='dropdown-menu-separator']", "[data-slot='dropdown-menu-separator']", dropdownScope);
        AddChildCssRules(buffer, Items, "[data-slot='dropdown-menu-item']", "[data-slot='dropdown-menu-item']", dropdownScope);
        AddChildCssRules(buffer, Labels, "[data-slot='dropdown-menu-label']", "[data-slot='dropdown-menu-label']", dropdownScope);
        AddChildCssRules(buffer, Menus, "[data-slot='dropdown-menu-content']", "[data-slot='dropdown-menu-content']", dropdownScope);
        AddChildCssRules(buffer, Shortcuts, "[data-slot='dropdown-menu-shortcut']", "[data-slot='dropdown-menu-shortcut']", dropdownScope);
        AddChildCssRules(buffer, Toggles, "[data-slot='dropdown-menu-trigger']", "[data-slot='dropdown-menu-trigger']", dropdownScope);
    }
}
