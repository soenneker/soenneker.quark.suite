using System.Collections.Generic;

namespace Soenneker.Quark.Suite.Demo;

public sealed record DocsNavLink(string Title, string? Href = null, bool IsNew = false, bool ExactMatch = false);

public static class DocsNavigation
{
    public static IReadOnlyList<DocsNavLink> TopLinks { get; } =
    [
        new("Docs", "", ExactMatch: true),
        new("Components", "components")
    ];

    public static IReadOnlyList<DocsNavLink> SectionLinks { get; } =
    [
        new("Introduction", "", ExactMatch: true),
        new("Components", "components"),
        new("Theming"),
    ];

    public static IReadOnlyList<DocsNavLink> ComponentLinks { get; } =
    [
        new("Accordion", "surfaces/accordions"),
        new("Alert", "surfaces/alerts"),
        new("Alert Dialog", "overlays/alert-dialogs"),
        new("Aspect Ratio", "layout/aspect-ratios"),
        new("Avatar", "media/avatars"),
        new("Badge", "typography/badges"),
        new("Breadcrumb", "navigation/breadcrumbs"),
        new("Button", "surfaces/buttons"),
        new("Button Group", "surfaces/button-groups"),
        new("Calendar", "forms/datepickers"),
        new("Card", "surfaces/cards"),
        new("Carousel", "media/carousels"),
        new("Chart", "data/charts"),
        new("Checkbox", "forms/checks"),
        new("Collapsible", "overlays/collapses"),
        new("Combobox", "forms/comboboxes"),
        new("Command", "overlays/commands"),
        new("Context Menu", "overlays/context-menus"),
        new("Data Table", "data/tables/basic"),
        new("Date Picker", "forms/datepickers"),
        new("Dialog", "overlays/modals"),
        new("Direction", "layout/directions"),
        new("Drawer", "overlays/sheets"),
        new("Dropdown Menu", "overlays/dropdowns"),
        new("Empty", "surfaces/empties"),
        new("Field", "forms/fields"),
        new("Hover Card", "overlays/hover-cards"),
        new("Input", "forms/input-demo"),
        new("Input Group", "forms/input-groups"),
        new("Input OTP", "forms/input-otp"),
        new("Item", "surfaces/items"),
        new("Kbd", "typography/kbd"),
        new("Label", "typography/labels"),
        new("Menubar", "navigation/menubar"),
        new("Native Select", "forms/native-selects"),
        new("Navigation Menu", "navigation/navigation-menu"),
        new("Pagination", "navigation/paginations"),
        new("Popover", "overlays/popovers"),
        new("Progress", "feedback/progresses"),
        new("Radio Group", "forms/radios"),
        new("Resizable", "layout/resizable"),
        new("Scroll Area"),
        new("Select", "forms/selects"),
        new("Separator", "surfaces/separators"),
        new("Sheet", "overlays/sheets"),
        new("Sidebar", "navigation/sidebar"),
        new("Skeleton", "surfaces/skeletons"),
        new("Slider", "forms/sliders"),
        new("Sonner", "feedback/sonner"),
        new("Spinner", "feedback/spinners"),
        new("Switch", "forms/switches"),
        new("Tabs", "navigation/tabs"),
        new("Textarea", "forms/textareas"),
        new("Toggle", "forms/toggles"),
        new("Tooltip", "overlays/tooltips"),
        new("Typography", "typography/typography")
    ];
}
