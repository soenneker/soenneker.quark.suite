using System.Collections.Generic;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

internal static class QuarkComponentSpecs
{
    // One row per demo URL; first column is a logging label only. "/" is covered by Landing_page_loads.
    public static IEnumerable<object[]> All()
    {
        yield return new object[] { "Accordion", "/accordions", "Accordion - Quark Suite" };
        yield return new object[] { "Alert", "/alerts", "Alerts - Quark Suite" };
        yield return new object[] { "AlertDialog", "/alert-dialogs", "Alert Dialogs - Quark Suite" };
        yield return new object[] { "Anchor", "/components", "Components - Quark Suite" };
        yield return new object[] { "Article", "/figures", "Figures & Figcaptions - Quark Suite" };
        yield return new object[] { "Aside", "/semantic-html", "Semantic HTML - Quark Suite" };
        yield return new object[] { "AspectRatio", "/aspect-ratios", "Aspect Ratio - Quark Suite" };
        yield return new object[] { "Audio", "/videos", "Video & Audio - Quark Suite" };
        yield return new object[] { "AvatarBadge", "/avatars", "Avatars - Quark Suite" };
        yield return new object[] { "Blockquote", "/blockquotes", "Blockquotes - Quark Suite" };
        yield return new object[] { "Br", "/br-demo", "Br (Line Break) - Quark Suite" };
        yield return new object[] { "Breadcrumb", "/breadcrumbs", "Breadcrumbs - Quark Suite" };
        yield return new object[] { "ButtonGroupSeparator", "/button-groups", "Button Groups - Quark Suite" };
        yield return new object[] { "Calendar", "/calendars", "Calendar - Quark Suite" };
        yield return new object[] { "CardAction", "/cards", "Card - Quark Suite" };
        yield return new object[] { "CardBody", "/bar-layout-demo", "Bar Layout Demo - Quark Suite" };
        yield return new object[] { "Carousel", "/carousels", "Carousel - Quark Suite" };
        yield return new object[] { "CheckboxGroup", "/checks", "Checkbox Component - Quark Suite" };
        yield return new object[] { "Code", "/codes", "Code - Quark Suite" };
        yield return new object[] { "CodeChip", "/typography", "Typography - Quark Suite" };
        yield return new object[] { "Collapse", "/collapses", "Collapses - Quark Suite" };
        yield return new object[] { "Collapsible", "/collapsibles", "Collapsible - Quark Suite" };
        yield return new object[] { "Combobox", "/comboboxes", "Combobox - Quark Suite" };
        yield return new object[] { "CommandDialog", "/commands", "Command - Quark Suite" };
        yield return new object[] { "Container", "/container", "Container - Quark Suite" };
        yield return new object[] { "ContextMenu", "/context-menus", "Context Menus - Quark Suite" };
        yield return new object[] { "CurrencyInput", "/currency-inputs", "CurrencyInput - Quark Suite" };
        yield return new object[] { "DateInput", "/dateinputs", "DateInputs - Quark Suite" };
        yield return new object[] { "DatePicker", "/datepickers", "Date Pickers - Quark Suite" };
        yield return new object[] { "Details", "/details-demo", "Details & Summary - Quark Suite" };
        yield return new object[] { "DialogBody", "/dialogs", "Dialog - Quark Suite" };
        yield return new object[] { "Drawer", "/drawers", "Drawer - Quark Suite" };
        yield return new object[] { "DropdownCheckboxItem", "/dropdowns", "Dropdowns - Quark Suite" };
        yield return new object[] { "FieldError", "/fields", "Fields - Quark Suite" };
        yield return new object[] { "FormControl", "/validation-demo", "Validation - Quark Suite" };
        yield return new object[] { "GridItem", "/grids", "Grid - Quark Suite" };
        yield return new object[] { "H5", "/code-editor", "Code Editor - Quark Suite" };
        yield return new object[] { "Header", "/header", "Header - Quark Suite" };
        yield return new object[] { "Heading", "/headings-basic", "" };
        yield return new object[] { "HoverCard", "/hover-cards", "Hover Cards - Quark Suite" };
        yield return new object[] { "IFrame", "/iframes", "IFrames - Quark Suite" };
        yield return new object[] { "InputOtp", "/input-otp", "Input OTP - Quark Suite" };
        yield return new object[] { "ItemFooter", "/items", "Item - Quark Suite" };
        yield return new object[] { "Kbd", "/kbd", "Kbd - Quark Suite" };
        yield return new object[] { "Legend", "/fieldsets-demo", "Fieldsets & Legends - Quark Suite" };
        yield return new object[] { "Menubar", "/menubar", "Menubar - Quark Suite" };
        yield return new object[] { "NativeSelect", "/native-selects", "Native Select - Quark Suite" };
        yield return new object[] { "NavigationMenu", "/navigation-menu", "Navigation Menu - Quark Suite" };
        yield return new object[] { "OrderedList", "/orderedlists", "Ordered Lists - Quark Suite" };
        yield return new object[] { "Pagination", "/paginations", "Paginations - Quark Suite" };
        yield return new object[] { "PopoverAnchor", "/popovers", "Popovers - Quark Suite" };
        yield return new object[] { "Progress", "/progresses", "Progress - Quark Suite" };
        yield return new object[] { "ResizableHandle", "/resizable", "Resizable - Quark Suite" };
        yield return new object[] { "Score", "/scores", "Scores - Quark Suite" };
        yield return new object[] { "ScrollArea", "/scroll-area", "Scroll Area - Quark Suite" };
        yield return new object[] { "SelectLabel", "/selects", "Select Component - Quark Suite" };
        yield return new object[] { "Sheet", "/sheets", "Sheet - Quark Suite" };
        yield return new object[] { "SidebarFooter", "/sidebar", "Sidebar - Quark Suite" };
        yield return new object[] { "Skeleton", "/skeletons", "Skeleton - Quark Suite" };
        yield return new object[] { "Sonner", "/sonner", "Sonner - Quark Suite" };
        yield return new object[] { "SortableHandle", "/sortables", "Sortable - Quark Suite" };
        yield return new object[] { "Spinner", "/badges", "Badge Component - Quark Suite" };
        yield return new object[] { "Stack", "/stack", "Stack - Quark Suite" };
        yield return new object[] { "Step", "/stepsdemo", "Steps - Quark Suite" };
        yield return new object[] { "TableCaption", "/tables-basic", "Table - Quark Suite" };
        yield return new object[] { "TableLoader", "/loading-demo", "Loading Demo - Quark Suite" };
        yield return new object[] { "Timeline", "/timelines", "Timeline - Quark Suite" };
        yield return new object[] { "Toggle", "/toggles", "Toggle - Quark Suite" };
        yield return new object[] { "ToggleGroup", "/toggle-groups", "Toggle Group - Quark Suite" };
        yield return new object[] { "TreeView", "/treeview", "TreeView - Quark Suite" };
    }
}
