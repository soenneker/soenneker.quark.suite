using System.Collections.Generic;

namespace Soenneker.Quark.Suite.Demo;

public static class DocsNavigation
{
    public static IReadOnlyList<NavigationItem> TopLinks { get; } =
    [
        new("Docs", "installation", ExactMatch: true),
        new("Components", "components")
    ];

    public static IReadOnlyList<NavigationItem> SectionLinks { get; } =
    [
        new("Introduction", "introduction"),
        new("Components", "components"),
        new("Installation", "installation"),
        new("Validation", "validation")
    ];

    public static IReadOnlyList<NavigationItem> ComponentLinks { get; } =
    [
        new("Accordion", "accordion"),
        new("Alert", "alert"),
        new("Alert Dialog", "alert-dialog"),
        new("Aspect Ratio", "aspect-ratio"),
        new("Avatar", "avatar"),
        new("Badge", "badge"),
        new("Breadcrumb", "breadcrumb"),
        new("Button", "button"),
        new("Button Group", "button-group"),
        new("Calendar", "calendar"),
        new("Card", "card"),
        new("Carousel", "carousel"),
        new("Chart", "chart"),
        new("Checkbox", "checkbox"),
        new("Collapsible", "collapsible"),
        new("Combobox", "combobox"),
        new("Command", "command"),
        new("Context Menu", "context-menu"),
        new("Data Table", "data-table"),
        new("Container", "container"),
        new("Currency Input", "currency-inputs", IsNew: true),
        new("Table", "table"),
        new("Date Picker", "date-picker"),
        new("Dialog", "dialog"),
        new("Direction", "direction"),
        new("Drawer", "drawer"),
        new("Dropdown Menu", "dropdown-menu"),
        new("Empty", "empty"),
        new("Field", "field"),
        new("Grid", "grids"),
        new("Header", "headers"),
        new("Hover Card", "hover-card"),
        new("Input", "input"),
        new("Input Group", "input-group"),
        new("Input OTP", "input-otp"),
        new("Item", "item"),
        new("Kbd", "kbd"),
        new("Label", "label"),
        new("Menubar", "menubar"),
        new("Attachments", "attachments", IsNew: true),
        new("Model Selector", "model-selectors", IsNew: true),
        new("Native Select", "native-select"),
        new("Navigation Menu", "navigation-menu"),
        new("Pagination", "pagination"),
        new("Popover", "popover"),
        new("Prompt Input", "prompt-inputs", IsNew: true),
        new("Progress", "progress"),
        new("Radio Group", "radio-group"),
        new("Resizable", "resizable"),
        new("Scroll Area", "scroll-area"),
        new("Select", "select"),
        new("Separator", "separator"),
        new("Sheet", "sheet"),
        new("Sidebar", "sidebar"),
        new("Skeleton", "skeleton"),
        new("Slider", "slider"),
        new("Sortable", "sortables", IsNew: true),
        new("Sonner", "sonner"),
        new("Spinner", "spinner"),
        new("Suggestions", "suggestions", IsNew: true),
        new("Stack", "stack"),
        new("Steps", "steps"),
        new("Switch", "switch"),
        new("Tabs", "tabs"),
        new("Textarea", "textarea"),
        new("Toast", "toast"),
        new("Thread", "threads", IsNew: true),
        new("Timeline", "timelines"),
        new("Toggle", "toggle"),
        new("Toggle Group", "toggle-groups"),
        new("On This Page", "docs-on-this-page", IsNew: true),
        new("Tooltip", "tooltip"),
        new("Typography", "typography")
    ];

    public static IReadOnlyList<NavigationItem> PrimitiveLinks { get; } =
    [
        new("Div", "divs"),
        new("Margin", "margins"),
        new("Padding", "padding"),
        new("Anchor", "anchors"),
        new("Aside", "asides", IsNew: true),
        new("Blockquote", "blockquotes"),
        new("Br", "br"),
        new("Code", "codes"),
        new("Fieldset", "fieldsets"),
        new("Figure", "figures"),
        new("Icons", "icons"),
        new("Nav", "navs", IsNew: true),
        new("Paragraph", "paragraphs"),
        new("Section", "sections"),
        new("Small", "small"),
        new("Span", "spans"),
        new("Strong", "strong"),
        new("Svg", "svgs", IsNew: true),
        new("Text", "texts"),
        new("Semantic HTML", "semantic-html")
    ];
}
