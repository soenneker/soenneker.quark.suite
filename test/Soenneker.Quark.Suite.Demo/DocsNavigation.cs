using System.Collections.Generic;

namespace Soenneker.Quark.Suite.Demo;

public sealed record DocsNavLink(string Title, string? Href = null, bool IsNew = false, bool ExactMatch = false);

public static class DocsNavigation
{
    public static IReadOnlyList<DocsNavLink> TopLinks { get; } =
    [
        new("Docs", "/", ExactMatch: true),
        new("Components", "/components"),
        new("Blocks"),
        new("Charts"),
        new("Directory"),
        new("Create")
    ];

    public static IReadOnlyList<DocsNavLink> SectionLinks { get; } =
    [
        new("Introduction", "/", ExactMatch: true),
        new("Components", "/components"),
        new("Installation"),
        new("Theming"),
        new("CLI", IsNew: true),
        new("RTL"),
        new("Skills", IsNew: true),
        new("MCP Server"),
        new("Registry"),
        new("Forms"),
        new("Changelog", IsNew: true)
    ];

    public static IReadOnlyList<DocsNavLink> ComponentLinks { get; } =
    [
        new("Accordion", "/surfaces/accordions"),
        new("Alert", "/surfaces/alerts"),
        new("Alert Dialog"),
        new("Aspect Ratio"),
        new("Avatar", "/media/avatars"),
        new("Badge", "/typography/badges"),
        new("Breadcrumb", "/navigation/breadcrumbs"),
        new("Button", "/surfaces/buttons"),
        new("Button Group", "/surfaces/button-groups"),
        new("Calendar", "/forms/date-pickers"),
        new("Card", "/surfaces/cards"),
        new("Carousel"),
        new("Chart"),
        new("Checkbox", "/forms/checks"),
        new("Collapsible", "/overlays/collapses"),
        new("Combobox"),
        new("Command"),
        new("Context Menu", "/overlays/context-menus"),
        new("Data Table", "/data/tables/basic"),
        new("Date Picker", "/forms/date-pickers"),
        new("Dialog", "/overlays/modals"),
        new("Direction"),
        new("Drawer", "/overlays/sheets"),
        new("Dropdown Menu", "/overlays/dropdowns"),
        new("Empty", "/surfaces/empties"),
        new("Field", "/forms/fields"),
        new("Hover Card"),
        new("Input", "/forms/input-demo"),
        new("Input Group", "/forms/input-groups"),
        new("Input OTP"),
        new("Item", "/surfaces/items"),
        new("Kbd"),
        new("Label", "/typography/labels"),
        new("Menubar", "/navigation/menubar"),
        new("Native Select", "/forms/native-selects"),
        new("Navigation Menu", "/navigation/navigation-menu"),
        new("Pagination", "/navigation/paginations"),
        new("Popover", "/overlays/popovers"),
        new("Progress"),
        new("Radio Group", "/forms/radios"),
        new("Resizable"),
        new("Scroll Area"),
        new("Select", "/forms/selects"),
        new("Separator", "/surfaces/separators"),
        new("Sheet", "/overlays/sheets"),
        new("Sidebar", "/navigation/sidebar"),
        new("Skeleton", "/surfaces/skeletons"),
        new("Switch", "/forms/switches"),
        new("Tabs", "/navigation/tabs"),
        new("Textarea", "/forms/textareas"),
        new("Toggle", "/forms/toggles")
    ];
}
