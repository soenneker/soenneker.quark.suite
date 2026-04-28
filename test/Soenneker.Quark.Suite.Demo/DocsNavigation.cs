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
        new("Currency Input", "currency-inputs", IsNew: true),
        new("Table", "tables-basic"),
        new("Date Picker", "datepickers"),
        new("Dialog", "dialogs"),
        new("Direction", "directions"),
        new("Drawer", "drawers"),
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
        new("Attachments", "attachments", IsNew: true),
        new("Model Selector", "model-selectors", IsNew: true),
        new("Native Select", "native-selects"),
        new("Navigation Menu", "navigation-menu"),
        new("Pagination", "paginations"),
        new("Popover", "popovers"),
        new("Prompt Input", "prompt-inputs", IsNew: true),
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
        new("Sortable", "sortables", IsNew: true),
        new("Sonner", "sonner"),
        new("Spinner", "spinners"),
        new("Suggestions", "suggestions", IsNew: true),
        new("Stack", "stack"),
        new("Steps", "stepsdemo"),
        new("Switch", "switches"),
        new("Tabs", "tabs"),
        new("Textarea", "textareas"),
        new("Thread", "threads", IsNew: true),
        new("Timeline", "timelines"),
        new("Toggle", "toggles"),
        new("Toggle Group", "toggle-groups"),
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
