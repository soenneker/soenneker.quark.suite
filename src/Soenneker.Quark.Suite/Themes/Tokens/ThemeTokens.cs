using System.Collections.Generic;

namespace Soenneker.Quark.Tokens;

/// <summary>
/// Strongly typed semantic and Tailwind theme tokens that can be authored in C# and emitted to CSS at build time.
/// </summary>
public sealed class ThemeTokens
{
    /// <summary>
    /// Gets or sets the light theme semantic token values written to <c>:root</c>.
    /// </summary>
    public ThemeTokenScheme Light { get; set; } = ThemeTokenScheme.CreateDefaultLight();

    /// <summary>
    /// Gets or sets the dark theme semantic token values written to <c>.dark</c>.
    /// </summary>
    public ThemeTokenScheme Dark { get; set; } = ThemeTokenScheme.CreateDefaultDark();

    /// <summary>
    /// Gets or sets additional custom variables written inside the generated <c>@theme inline</c> block.
    /// Keys should omit the <c>--</c> prefix.
    /// </summary>
    public Dictionary<string, string> InlineVariables { get; set; } = new();
}

/// <summary>
/// Semantic token values for one color scheme.
/// </summary>
public sealed class ThemeTokenScheme
{
    /// <summary>
    /// Gets or sets background.
    /// </summary>
    public string Background { get; set; } = "";
    /// <summary>
    /// Gets or sets foreground.
    /// </summary>
    public string Foreground { get; set; } = "";
    /// <summary>
    /// Gets or sets card.
    /// </summary>
    public string Card { get; set; } = "";
    /// <summary>
    /// Gets or sets card foreground.
    /// </summary>
    public string CardForeground { get; set; } = "";
    /// <summary>
    /// Gets or sets popover.
    /// </summary>
    public string Popover { get; set; } = "";
    /// <summary>
    /// Gets or sets popover foreground.
    /// </summary>
    public string PopoverForeground { get; set; } = "";
    /// <summary>
    /// Gets or sets primary.
    /// </summary>
    public string Primary { get; set; } = "";
    /// <summary>
    /// Gets or sets primary foreground.
    /// </summary>
    public string PrimaryForeground { get; set; } = "";
    /// <summary>
    /// Gets or sets secondary.
    /// </summary>
    public string Secondary { get; set; } = "";
    /// <summary>
    /// Gets or sets secondary foreground.
    /// </summary>
    public string SecondaryForeground { get; set; } = "";
    /// <summary>
    /// Gets or sets muted.
    /// </summary>
    public string Muted { get; set; } = "";
    /// <summary>
    /// Gets or sets muted foreground.
    /// </summary>
    public string MutedForeground { get; set; } = "";
    /// <summary>
    /// Gets or sets accent.
    /// </summary>
    public string Accent { get; set; } = "";
    /// <summary>
    /// Gets or sets accent foreground.
    /// </summary>
    public string AccentForeground { get; set; } = "";
    /// <summary>
    /// Gets or sets destructive.
    /// </summary>
    public string Destructive { get; set; } = "";
    /// <summary>
    /// Gets or sets destructive foreground.
    /// </summary>
    public string DestructiveForeground { get; set; } = "";
    /// <summary>
    /// Gets or sets border.
    /// </summary>
    public string Border { get; set; } = "";
    /// <summary>
    /// Gets or sets input.
    /// </summary>
    public string Input { get; set; } = "";
    /// <summary>
    /// Gets or sets ring.
    /// </summary>
    public string Ring { get; set; } = "";
    /// <summary>
    /// Gets or sets radius.
    /// </summary>
    public string Radius { get; set; } = "";
    /// <summary>
    /// Gets or sets series.
    /// </summary>
    public ThemeSeriesTokens Series { get; set; } = new();
    /// <summary>
    /// Gets or sets sidebar.
    /// </summary>
    public ThemeSidebarTokens Sidebar { get; set; } = new();

    /// <summary>
    /// Gets or sets additional custom CSS variables for this scheme.
    /// Keys should omit the <c>--</c> prefix.
    /// </summary>
    public Dictionary<string, string> Variables { get; set; } = new();

    internal static ThemeTokenScheme CreateDefaultLight()
    {
        return new ThemeTokenScheme
        {
            Background = "#fff",
            Foreground = "#0a0a0a",
            Card = "#fff",
            CardForeground = "#0a0a0a",
            Popover = "#fff",
            PopoverForeground = "#0a0a0a",
            Primary = "#171717",
            PrimaryForeground = "#fafafa",
            Secondary = "#f5f5f5",
            SecondaryForeground = "#171717",
            Muted = "#f5f5f5",
            MutedForeground = "#737373",
            Accent = "#f5f5f5",
            AccentForeground = "#171717",
            Destructive = "#e40014",
            DestructiveForeground = "#fcf3f3",
            Border = "#e5e5e5",
            Input = "#e5e5e5",
            Ring = "#a1a1a1",
            Radius = ".625rem",
            Series = new ThemeSeriesTokens
            {
                First = "var(--color-blue-300)",
                Second = "var(--color-blue-500)",
                Third = "var(--color-blue-600)",
                Fourth = "var(--color-blue-700)",
                Fifth = "var(--color-blue-800)"
            },
            Sidebar = new ThemeSidebarTokens
            {
                Background = "#fafafa",
                Foreground = "#0a0a0a",
                Primary = "#171717",
                PrimaryForeground = "#fafafa",
                Accent = "#f5f5f5",
                AccentForeground = "#171717",
                Border = "#e5e5e5",
                Ring = "#a1a1a1"
            },
            Variables = new Dictionary<string, string>
            {
                ["surface"] = "#f8f8f8",
                ["surface-foreground"] = "var(--foreground)",
                ["code"] = "var(--surface)",
                ["code-foreground"] = "var(--surface-foreground)",
                ["code-highlight"] = "#f2f2f2",
                ["code-number"] = "#747474",
                ["selection"] = "#0a0a0a"
            }
        };
    }

    internal static ThemeTokenScheme CreateDefaultDark()
    {
        return new ThemeTokenScheme
        {
            Background = "#0a0a0a",
            Foreground = "#fafafa",
            Card = "#171717",
            CardForeground = "#fafafa",
            Popover = "#171717",
            PopoverForeground = "#fafafa",
            Primary = "#e5e5e5",
            PrimaryForeground = "#171717",
            Secondary = "#262626",
            SecondaryForeground = "#fafafa",
            Muted = "#262626",
            MutedForeground = "#a1a1a1",
            Accent = "#404040",
            AccentForeground = "#fafafa",
            Destructive = "#ff6568",
            DestructiveForeground = "#df2225",
            Border = "#ffffff1a",
            Input = "#ffffff26",
            Ring = "#737373",
            Radius = ".625rem",
            Series = new ThemeSeriesTokens
            {
                First = "var(--color-blue-300)",
                Second = "var(--color-blue-500)",
                Third = "var(--color-blue-600)",
                Fourth = "var(--color-blue-700)",
                Fifth = "var(--color-blue-800)"
            },
            Sidebar = new ThemeSidebarTokens
            {
                Background = "#171717",
                Foreground = "#fafafa",
                Primary = "#1447e6",
                PrimaryForeground = "#fafafa",
                Accent = "#262626",
                AccentForeground = "#fafafa",
                Border = "#ffffff1a",
                Ring = "#525252"
            },
            Variables = new Dictionary<string, string>
            {
                ["surface"] = "#161616",
                ["surface-foreground"] = "#a1a1a1",
                ["code"] = "var(--surface)",
                ["code-foreground"] = "var(--surface-foreground)",
                ["code-highlight"] = "#262626",
                ["code-number"] = "#a4a4a4",
                ["selection"] = "#e5e5e5"
            }
        };
    }
}

/// <summary>
/// Sequential palette tokens used by visual components.
/// </summary>
public sealed class ThemeSeriesTokens
{
    /// <summary>
    /// Gets or sets first.
    /// </summary>
    public string First { get; set; } = "";
    /// <summary>
    /// Gets or sets second.
    /// </summary>
    public string Second { get; set; } = "";
    /// <summary>
    /// Gets or sets third.
    /// </summary>
    public string Third { get; set; } = "";
    /// <summary>
    /// Gets or sets fourth.
    /// </summary>
    public string Fourth { get; set; } = "";
    /// <summary>
    /// Gets or sets fifth.
    /// </summary>
    public string Fifth { get; set; } = "";
}

/// <summary>
/// Sidebar semantic tokens used by the shadcn theme contract.
/// </summary>
public sealed class ThemeSidebarTokens
{
    /// <summary>
    /// Gets or sets background.
    /// </summary>
    public string Background { get; set; } = "";
    /// <summary>
    /// Gets or sets foreground.
    /// </summary>
    public string Foreground { get; set; } = "";
    /// <summary>
    /// Gets or sets primary.
    /// </summary>
    public string Primary { get; set; } = "";
    /// <summary>
    /// Gets or sets primary foreground.
    /// </summary>
    public string PrimaryForeground { get; set; } = "";
    /// <summary>
    /// Gets or sets accent.
    /// </summary>
    public string Accent { get; set; } = "";
    /// <summary>
    /// Gets or sets accent foreground.
    /// </summary>
    public string AccentForeground { get; set; } = "";
    /// <summary>
    /// Gets or sets border.
    /// </summary>
    public string Border { get; set; } = "";
    /// <summary>
    /// Gets or sets ring.
    /// </summary>
    public string Ring { get; set; } = "";
}
