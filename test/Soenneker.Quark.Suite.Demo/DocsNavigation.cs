using System.Collections.Generic;

namespace Soenneker.Quark.Suite.Demo;

public static class DocsNavigation
{
    private static string ComponentRoute(string route) => $"components/{route.Trim('/')}";

    public static IReadOnlyList<NavigationItem> TopLinks { get; } =
    [
        new("Docs", ".", ExactMatch: true),
        new("Components", "components")
    ];

    public static IReadOnlyList<NavigationItem> SectionLinks { get; } =
    [
        new("Introduction", "introduction"),
        new("Components", "components"),
        new("Installation", "installation"),
        new("Properties", "properties"),
        new("Grid System", "grid-system"),
        new("Themes", "themes"),
        new("Validation", ComponentRoute("validation"))
    ];

    public static IReadOnlyList<NavigationItem> ComponentLinks { get; } =
    [
        new("Accordion", ComponentRoute("accordion")),
        new("Alert", ComponentRoute("alert")),
        new("Alert Dialog", ComponentRoute("alert-dialog")),
        new("Aspect Ratio", ComponentRoute("aspect-ratio")),
        new("Avatar", ComponentRoute("avatar")),
        new("Badge", ComponentRoute("badge")),
        new("Breadcrumb", ComponentRoute("breadcrumb")),
        new("Button", ComponentRoute("button")),
        new("Button Group", ComponentRoute("button-group")),
        new("Calendar", ComponentRoute("calendar")),
        new("Card", ComponentRoute("card")),
        new("Carousel", ComponentRoute("carousel")),
        new("Checkbox", ComponentRoute("checkbox")),
        new("CodeEditor", ComponentRoute("codeeditors"), IsNew: true),
        new("Collapsible", ComponentRoute("collapsible")),
        new("Combobox", ComponentRoute("combobox")),
        new("Command", ComponentRoute("command")),
        new("Context Menu", ComponentRoute("context-menu")),
        new("Data Table", ComponentRoute("data-table")),
        new("Container", ComponentRoute("container")),
        new("Currency Input", ComponentRoute("currency-inputs"), IsNew: true),
        new("Table", ComponentRoute("table")),
        new("Date Picker", ComponentRoute("date-picker")),
        new("Dialog", ComponentRoute("dialog")),
        new("Direction", ComponentRoute("direction")),
        new("Drawer", ComponentRoute("drawer")),
        new("Dropdown Menu", ComponentRoute("dropdown-menu")),
        new("Empty", ComponentRoute("empty")),
        new("Field", ComponentRoute("field")),
        new("Grid", ComponentRoute("grids")),
        new("Header", ComponentRoute("headers")),
        new("Hover Card", ComponentRoute("hover-card")),
        new("Image", ComponentRoute("images"), IsNew: true),
        new("Input", ComponentRoute("input")),
        new("Input Group", ComponentRoute("input-group")),
        new("Input OTP", ComponentRoute("input-otp")),
        new("Item", ComponentRoute("item")),
        new("Kbd", ComponentRoute("kbd")),
        new("Label", ComponentRoute("label")),
        new("Menubar", ComponentRoute("menubar")),
        new("Attachments", ComponentRoute("attachments"), IsNew: true),
        new("Citation", ComponentRoute("citation"), IsNew: true),
        new("Message", ComponentRoute("message"), IsNew: true),
        new("Model Selector", ComponentRoute("model-selectors"), IsNew: true),
        new("Native Select", ComponentRoute("native-select")),
        new("Navigation Menu", ComponentRoute("navigation-menu")),
        new("Pagination", ComponentRoute("pagination")),
        new("Popover", ComponentRoute("popover")),
        new("Prompt Input", ComponentRoute("prompt-inputs"), IsNew: true),
        new("Progress", ComponentRoute("progress")),
        new("Radio Group", ComponentRoute("radio-group")),
        new("Reasoning", ComponentRoute("reasoning"), IsNew: true),
        new("Resizable", ComponentRoute("resizable")),
        new("Scroll Area", ComponentRoute("scroll-area")),
        new("Select", ComponentRoute("select")),
        new("Separator", ComponentRoute("separator")),
        new("Sheet", ComponentRoute("sheet")),
        new("Sidebar", ComponentRoute("sidebar")),
        new("Skeleton", ComponentRoute("skeleton")),
        new("Slider", ComponentRoute("slider")),
        new("Sortable", ComponentRoute("sortables"), IsNew: true),
        new("Sonner", ComponentRoute("sonner")),
        new("Spinner", ComponentRoute("spinner")),
        new("Suggestions", ComponentRoute("suggestions"), IsNew: true),
        new("Stack", ComponentRoute("stack")),
        new("Steps", ComponentRoute("steps")),
        new("Switch", ComponentRoute("switch")),
        new("Tabs", ComponentRoute("tabs")),
        new("Textarea", ComponentRoute("textarea")),
        new("Text Shimmer", ComponentRoute("text-shimmer"), IsNew: true),
        new("Toast", ComponentRoute("toast")),
        new("Thread", ComponentRoute("threads"), IsNew: true),
        new("Timeline", ComponentRoute("timelines")),
        new("Toggle", ComponentRoute("toggle")),
        new("Toggle Group", ComponentRoute("toggle-groups")),
        new("On This Page", ComponentRoute("docs-on-this-page"), IsNew: true),
        new("Tooltip", ComponentRoute("tooltip")),
        new("Typography", ComponentRoute("typography"))
    ];

    public static IReadOnlyList<NavigationItem> PrimitiveLinks { get; } =
    [
        new("Div", ComponentRoute("divs")),
        new("Margin", ComponentRoute("margins")),
        new("Padding", ComponentRoute("padding")),
        new("Anchor", ComponentRoute("anchors")),
        new("Aside", ComponentRoute("asides"), IsNew: true),
        new("Blockquote", ComponentRoute("blockquotes")),
        new("Br", ComponentRoute("br")),
        new("Code", ComponentRoute("codes")),
        new("Fieldset", ComponentRoute("fieldsets")),
        new("Figure", ComponentRoute("figures")),
        new("Icons", ComponentRoute("icons")),
        new("Nav", ComponentRoute("navs"), IsNew: true),
        new("Paragraph", ComponentRoute("paragraphs")),
        new("Section", ComponentRoute("sections")),
        new("Small", ComponentRoute("small")),
        new("Span", ComponentRoute("spans")),
        new("Strong", ComponentRoute("strong")),
        new("Svg", ComponentRoute("svgs"), IsNew: true),
        new("Text", ComponentRoute("texts")),
        new("Semantic HTML", ComponentRoute("semantic-html"))
    ];
}
