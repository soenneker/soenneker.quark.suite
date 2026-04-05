using System.Collections.Generic;

namespace Soenneker.Quark.Suite.Demo;

public static class DocsNavigation
{
    public static IReadOnlyList<NavigationItem> TopLinks { get; } =
    [
        new("Docs", "", ExactMatch: true),
        new("Components", "components")
    ];

    public static IReadOnlyList<NavigationItem> SectionLinks { get; } =
    [
        new("Introduction", "", ExactMatch: true),
        new("Components", "components"),
        new("Validation", "validation-demo")
    ];

    public static IReadOnlyList<NavigationItem> ComponentLinks { get; } =
    [
        new("Accordion", "accordions"),
        new("Alert", "alerts"),
        new("Alert Dialog", "alert-dialogs"),
        new("Aspect Ratio", "aspect-ratios"),
        new("Avatar", "avatars"),
        new("Badge", "badges"),
        new("Breadcrumb", "breadcrumbs"),
        new("Button", "buttons"),
        new("Button Group", "button-groups"),
        new("Calendar", "calendars"),
        new("Card", "cards"),
        new("Carousel", "carousels"),
        new("Checkbox", "checks"),
        new("Collapsible", "collapsibles"),
        new("Combobox", "comboboxes"),
        new("Command", "commands"),
        new("Context Menu", "context-menus"),
        new("Container", "container"),
        new("Table", "tables-basic"),
        new("Date Picker", "datepickers"),
        new("Dialog", "dialogs"),
        new("Direction", "directions"),
        new("Drawer", "sheets"),
        new("Dropdown Menu", "dropdowns"),
        new("Empty", "empties"),
        new("Field", "fields"),
        new("Grid", "grids"),
        new("Header", "header"),
        new("Hover Card", "hover-cards"),
        new("Input", "input-demo"),
        new("Input Group", "input-groups"),
        new("Input OTP", "input-otp"),
        new("Item", "items"),
        new("Kbd", "kbd"),
        new("Label", "labels"),
        new("Menubar", "menubar"),
        new("Native Select", "native-selects"),
        new("Navigation Menu", "navigation-menu"),
        new("Pagination", "paginations"),
        new("Popover", "popovers"),
        new("Progress", "progresses"),
        new("Radio Group", "radiogroups"),
        new("Resizable", "resizable"),
        new("Scroll Area", "scroll-area"),
        new("Select", "selects"),
        new("Separator", "separators"),
        new("Sheet", "sheets"),
        new("Sidebar", "sidebar"),
        new("Skeleton", "skeletons"),
        new("Slider", "sliders"),
        new("Sonner", "sonner"),
        new("Spinner", "spinners"),
        new("Stack", "stack"),
        new("Steps", "stepsdemo"),
        new("Switch", "switches"),
        new("Tabs", "tabs"),
        new("Textarea", "textareas"),
        new("Timeline", "timelines"),
        new("Toggle", "toggles"),
        new("Tooltip", "tooltips"),
        new("Typography", "typography")
    ];

    public static IReadOnlyList<NavigationItem> PrimitiveLinks { get; } =
    [
        new("Div", "divs"),
        new("Margin", "margin-demo"),
        new("Padding", "padding-demo"),
        new("Anchor", "anchors"),
        new("Blockquote", "blockquotes"),
        new("Br", "br-demo"),
        new("Code", "codes"),
        new("Fieldset", "fieldsets-demo"),
        new("Figure", "figures"),
        new("Icons", "icons"),
        new("Paragraph", "paragraph-demo"),
        new("Section", "section-demo"),
        new("Small", "small-demo"),
        new("Span", "span-demo"),
        new("Strong", "strong-demo"),
        new("Text", "texts"),
        new("Semantic HTML", "semantic-html")
    ];
}
