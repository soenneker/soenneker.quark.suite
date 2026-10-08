using Soenneker.Quark;
using Soenneker.Quark.Tokens;

namespace Soenneker.Quark.Suite.Tests;

/// <summary>
/// Provides shared operations and values for quark theme.
/// </summary>
internal static class LeadpingThemeFixture
{
    private const string ActiveTabSelector = "&[data-active], &[aria-selected='true'], &[data-state='active']";

    public static Theme Build() => new()
    {
        Name = "Leadping",
        Tokens = new ThemeTokens
        {
            Light =
            {
                Background = "#fdfdfd",
                Foreground = "#191919",
                Card = "#ffffff",
                CardForeground = "#191919",
                Popover = "#ffffff",
                PopoverForeground = "#191919",
                Primary = "#191919",
                PrimaryForeground = "#ffffff",
                Secondary = "#eeeeef",
                SecondaryForeground = "#191919",
                Muted = "#f4f4f5",
                MutedForeground = "#6f6f73",
                Accent = "#eeeeef",
                AccentForeground = "#191919",
                Destructive = "#dc2626",
                Border = "#e5e5e6",
                Input = "#d9d9dc",
                Ring = "#a1a1aa",
                Radius = "8px",
                Sidebar =
                {
                    Background = "#fdfdfd",
                    Foreground = "#303033",
                    Primary = "#191919",
                    PrimaryForeground = "#ffffff",
                    Accent = "#ededee",
                    AccentForeground = "#191919",
                    Border = "#e5e5e6",
                    Ring = "#a1a1aa"
                },
                Variables =
                {
                    ["surface"] = "#ffffff",
                    ["surface-muted"] = "#f4f4f5",
                    ["surface-subtle"] = "#fafafa",
                    ["border-strong"] = "#d6d6d8",
                    ["muted-strong"] = "#303033",
                    ["primary-hover"] = "#303033",
                    ["link"] = "#2563eb",
                    ["danger"] = "#dc2626",
                    ["success-bg"] = "#ecfdf3",
                    ["success-text"] = "#047857",
                    ["warning-bg"] = "#fffbeb",
                    ["warning-text"] = "#a16207",
                    ["danger-bg"] = "#fef2f2",
                    ["danger-text"] = "#dc2626",
                    ["info-bg"] = "#eff6ff",
                    ["info-text"] = "#2563eb",
                    ["control-radius"] = "12px",
                    ["card-radius"] = "8px",
                    ["shadow"] = "none",
                    ["action-bg"] = "#eeeeef",
                    ["action-bg-hover"] = "#e7e7e8",
                    ["action-border"] = "#d9d9dc",
                    ["action-border-hover"] = "#c9c9cc",
                    ["action-text"] = "#191919",
                    ["secondary-hover"] = "#303033",
                    ["secondary-border"] = "#191919",
                    ["control-bg"] = "#eeeeef",
                    ["control-bg-hover"] = "#e7e7e8",
                    ["control-border"] = "#d9d9dc",
                    ["table-border"] = "#ececee",
                    ["table-header-bg"] = "#eeeeef",
                    ["table-row-hover"] = "#fafafa"
                }
            },
            Dark =
            {
                Background = "#111112",
                Foreground = "#f4f4f5",
                Card = "#18181a",
                CardForeground = "#f4f4f5",
                Popover = "#18181a",
                PopoverForeground = "#f4f4f5",
                Primary = "#f4f4f5",
                PrimaryForeground = "#18181a",
                Secondary = "#27272a",
                SecondaryForeground = "#f4f4f5",
                Muted = "#27272a",
                MutedForeground = "#a1a1aa",
                Accent = "#27272a",
                AccentForeground = "#f4f4f5",
                Destructive = "#f87171",
                Border = "#2d2d31",
                Input = "#3f3f46",
                Ring = "#52525b",
                Radius = "8px",
                Sidebar =
                {
                    Background = "#18181a",
                    Foreground = "#f4f4f5",
                    Primary = "#f4f4f5",
                    PrimaryForeground = "#18181a",
                    Accent = "#27272a",
                    AccentForeground = "#f4f4f5",
                    Border = "#2d2d31",
                    Ring = "#52525b"
                },
                Variables =
                {
                    ["surface"] = "#18181a",
                    ["surface-muted"] = "#222225",
                    ["surface-subtle"] = "#1d1d20",
                    ["border-strong"] = "#3a3a40",
                    ["muted-strong"] = "#e4e4e7",
                    ["primary-hover"] = "#d4d4d8",
                    ["link"] = "#60a5fa",
                    ["danger"] = "#f87171",
                    ["success-bg"] = "#052e22",
                    ["success-text"] = "#34d399",
                    ["warning-bg"] = "#3a2a05",
                    ["warning-text"] = "#fbbf24",
                    ["danger-bg"] = "#3f1115",
                    ["danger-text"] = "#f87171",
                    ["info-bg"] = "#0f2747",
                    ["info-text"] = "#60a5fa",
                    ["control-radius"] = "12px",
                    ["card-radius"] = "8px",
                    ["shadow"] = "0 12px 30px rgba(0, 0, 0, 0.24)",
                    ["action-bg"] = "#27272a",
                    ["action-bg-hover"] = "#323238",
                    ["action-border"] = "#3f3f46",
                    ["action-border-hover"] = "#52525b",
                    ["action-text"] = "#f4f4f5",
                    ["secondary-hover"] = "#3f3f46",
                    ["secondary-border"] = "#52525b",
                    ["control-bg"] = "#27272a",
                    ["control-bg-hover"] = "#323238",
                    ["control-border"] = "#3f3f46",
                    ["table-border"] = "#2a2a2e",
                    ["table-header-bg"] = "#222225",
                    ["table-row-hover"] = "#1d1d20"
                }
            }
        },
        DataTableBottomBars = new DataTableBottomBarOptions
        {
            Selector = "[data-slot='datatable-bottom-bar']",
            BackgroundColor = BackgroundColor.Transparent,
            Border = Border.Is0,
            Shadow = Shadow.None
        },
        DataTables = new DataTableThemeOptions
        {
            Width = Width.IsFull,
            MinWidth = Width.Token("[42rem]"),
            Border = Border.Is0,
            BackgroundColor = BackgroundColor.Transparent,
            Anchors = new AnchorOptions
            {
                Display = Display.InlineFlex,
                MaxWidth = Width.IsFull,
                MinWidth = Width.Is0,
                ItemsAlign = Items.Center,
                Gap = Gap.Is2,
                Overflow = Overflow.Hidden,
                TextColor = TextColor.Token("[var(--foreground)]"),
                FontWeight = FontWeight.Normal,
                DecorationLine = DecorationLine.None
            },
            BottomBars = new DataTableBottomBarOptions
            {
                BackgroundColor = BackgroundColor.Transparent,
                Border = Border.Is0,
                Shadow = Shadow.None
            },
            Inputs = new InputOptions
            {
                Shadow = Shadow.None,
                Ring = Ring.OnFocusVisible.None
            },
            AnchorDivs = new DivOptions
            {
                MinWidth = Width.Is0,
                Overflow = Overflow.Hidden
            },
            AnchorLeadingSpans = new SpanOptions
            {
                Shrink = Shrink.Is0
            },
            AnchorSmalls = new SmallOptions
            {
                Display = Display.Block,
                Overflow = Overflow.Hidden,
                TextOverflow = TextOverflow.Ellipsis,
                Whitespace = Whitespace.Nowrap
            },
            AnchorSpans = new SpanOptions
            {
                Display = Display.Block,
                Overflow = Overflow.Hidden,
                TextOverflow = TextOverflow.Ellipsis,
                Whitespace = Whitespace.Nowrap
            },
            Tds = new TdOptions
            {
                Padding = Padding.OnY.Is3,
                VerticalAlign = VerticalAlign.Top
            },
            Ths = new ThOptions
            {
                TextSize = TextSize.Sm,
                FontWeight = FontWeight.Semibold
            },
            Trs = new TrOptions
            {
                Border = Border.FromBottom.Is1,
                BorderColor = BorderColor.Token("[var(--border)]")
            },
            TopBars = new DataTableTopBarOptions
            {
                BackgroundColor = BackgroundColor.Token("[var(--surface)]")
            }
        },
        Sections = new SectionOptions
        {
            Border = Border.Is0.WithSelector("&:has(.q-datatable)")
        },
        Fields = new FieldOptions
        {
            Selector = "[data-scope='setup'] [data-slot='field']",
            Gap = Gap.Token("[0.375rem]")
        },
        Labels = new LabelOptions
        {
            Selector = "[data-scope='setup'] [data-slot='label']",
            TextSize = TextSize.Token("[0.8125rem]"),
            FontWeight = FontWeight.Semibold,
            Leading = Leading.None
        },
        TextInputs = new TextInputOptions
        {
            Selector = "[data-scope='setup'] [data-slot='input']",
            Rounded = Rounded.Md,
            BorderColor = BorderColor.Token("[var(--control-border)]"),
            BackgroundColor = BackgroundColor.Token("[var(--surface)]"),
            Padding = Padding.OnX.Token("[0.875rem]"),
            TextSize = TextSize.Sm,
            Shadow = Shadow.None,
            Transition = Transition.Colors
        },
        Selects = new SelectOptions
        {
            Selector = "[data-scope='setup'] [data-slot='select-trigger'], [data-scope='setup'] [data-combobox-slot='combobox-control']",
            Rounded = Rounded.Md,
            BorderColor = BorderColor.Token("[var(--control-border)]"),
            BackgroundColor = BackgroundColor.Token("[var(--surface)]")
        },
        MemoInputs = new MemoInputOptions
        {
            Selector = "[data-scope='setup'] [data-slot='textarea']",
            Rounded = Rounded.Md,
            BorderColor = BorderColor.Token("[var(--control-border)]"),
            BackgroundColor = BackgroundColor.Token("[var(--surface)]"),
            TextColor = TextColor.Token("[var(--foreground)]")
        },
        ValidationError = new ValidationErrorOptions
        {
            Selector = "[data-scope='setup'] [data-slot='field-error']",
            TextSize = TextSize.Xs
        },
        Checks = new CheckOptions
        {
            Selector = "[data-scope='setup'] [data-slot='checkbox']",
            Margin = Margin.FromTop.Token("[0.125rem]"),
            BorderColor = BorderColor.Token("[var(--control-border)]")
        },
        FieldLabels = new FieldLabelOptions
        {
            Selector = "[data-scope='setup'] label[for^='quark-check']",
            ItemsAlign = Items.Start,
            Gap = Gap.Is3,
            Leading = Leading.Snug,
            FontWeight = FontWeight.Medium
        },
        Dialogs = new DialogOptions
        {
            Selector = "[data-slot='dialog-content']:not(.p-0)",
            Gap = Gap.Is2,
            Headers = new DialogHeaderOptions
            {
                Display = Display.Flex,
                FlexDirection = FlexDirection.Row,
                ItemsAlign = Items.Center,
                Justify = Justify.Between,
                Gap = Gap.Is2,
                Padding = Padding.Is0,
                Border = Border.Is0
            }
        },
        Tabs = new TabsOptions
        {
            Lists = new TabsListOptions
            {
                MaxWidth = Width.IsFull,
                Gap = Gap.Is1,
                BackgroundColor = BackgroundColor.Token("[var(--surface-muted)]"),
                Rounded = Rounded.Xl
            },
            Triggers = new TabOptions
            {
                Padding = Padding.OnY.Is1.OnX.Is3,
                Border = Border.Is0,
                Rounded = Rounded.Lg,
                BackgroundColor = BackgroundColor.Token("[var(--surface)]").WithSelector(ActiveTabSelector),
                TextColor = TextColor.Token("[var(--foreground)]").WithSelector(ActiveTabSelector),
                Shadow = Shadow.None.WithSelector(ActiveTabSelector)
            }
        }
    };
}
