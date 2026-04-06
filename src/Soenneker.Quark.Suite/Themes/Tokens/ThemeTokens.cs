using System.Collections.Generic;

namespace Soenneker.Quark;

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
    public string Background { get; set; } = "";
    public string Foreground { get; set; } = "";
    public string Card { get; set; } = "";
    public string CardForeground { get; set; } = "";
    public string Popover { get; set; } = "";
    public string PopoverForeground { get; set; } = "";
    public string Primary { get; set; } = "";
    public string PrimaryForeground { get; set; } = "";
    public string Secondary { get; set; } = "";
    public string SecondaryForeground { get; set; } = "";
    public string Muted { get; set; } = "";
    public string MutedForeground { get; set; } = "";
    public string Accent { get; set; } = "";
    public string AccentForeground { get; set; } = "";
    public string Destructive { get; set; } = "";
    public string DestructiveForeground { get; set; } = "";
    public string Border { get; set; } = "";
    public string Input { get; set; } = "";
    public string Ring { get; set; } = "";
    public string Radius { get; set; } = "";
    public ThemeChartTokens Chart { get; set; } = new();
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
            Background = "oklch(1 0 0)",
            Foreground = "oklch(0.145 0 0)",
            Card = "oklch(1 0 0)",
            CardForeground = "oklch(0.145 0 0)",
            Popover = "oklch(1 0 0)",
            PopoverForeground = "oklch(0.145 0 0)",
            Primary = "oklch(0.205 0 0)",
            PrimaryForeground = "oklch(0.985 0 0)",
            Secondary = "oklch(0.97 0 0)",
            SecondaryForeground = "oklch(0.205 0 0)",
            Muted = "oklch(0.97 0 0)",
            MutedForeground = "oklch(0.556 0 0)",
            Accent = "oklch(0.97 0 0)",
            AccentForeground = "oklch(0.205 0 0)",
            Destructive = "oklch(0.577 0.245 27.325)",
            DestructiveForeground = "oklch(0.577 0.245 27.325)",
            Border = "oklch(0.922 0 0)",
            Input = "oklch(0.922 0 0)",
            Ring = "oklch(0.708 0 0)",
            Radius = "0.625rem",
            Chart = new ThemeChartTokens
            {
                First = "oklch(0.646 0.222 41.116)",
                Second = "oklch(0.6 0.118 184.704)",
                Third = "oklch(0.398 0.07 227.392)",
                Fourth = "oklch(0.828 0.189 84.429)",
                Fifth = "oklch(0.769 0.188 70.08)"
            },
            Sidebar = new ThemeSidebarTokens
            {
                Background = "oklch(0.985 0 0)",
                Foreground = "oklch(0.145 0 0)",
                Primary = "oklch(0.205 0 0)",
                PrimaryForeground = "oklch(0.985 0 0)",
                Accent = "oklch(0.97 0 0)",
                AccentForeground = "oklch(0.205 0 0)",
                Border = "oklch(0.922 0 0)",
                Ring = "oklch(0.708 0 0)"
            }
        };
    }

    internal static ThemeTokenScheme CreateDefaultDark()
    {
        return new ThemeTokenScheme
        {
            Background = "oklch(0.145 0 0)",
            Foreground = "oklch(0.985 0 0)",
            Card = "oklch(0.145 0 0)",
            CardForeground = "oklch(0.985 0 0)",
            Popover = "oklch(0.145 0 0)",
            PopoverForeground = "oklch(0.985 0 0)",
            Primary = "oklch(0.985 0 0)",
            PrimaryForeground = "oklch(0.205 0 0)",
            Secondary = "oklch(0.269 0 0)",
            SecondaryForeground = "oklch(0.985 0 0)",
            Muted = "oklch(0.269 0 0)",
            MutedForeground = "oklch(0.708 0 0)",
            Accent = "oklch(0.269 0 0)",
            AccentForeground = "oklch(0.985 0 0)",
            Destructive = "oklch(0.396 0.141 25.723)",
            DestructiveForeground = "oklch(0.637 0.237 25.331)",
            Border = "oklch(0.269 0 0)",
            Input = "oklch(0.269 0 0)",
            Ring = "oklch(0.439 0 0)",
            Radius = "0.625rem",
            Chart = new ThemeChartTokens
            {
                First = "oklch(0.488 0.243 264.376)",
                Second = "oklch(0.696 0.17 162.48)",
                Third = "oklch(0.769 0.188 70.08)",
                Fourth = "oklch(0.627 0.265 303.9)",
                Fifth = "oklch(0.645 0.246 16.439)"
            },
            Sidebar = new ThemeSidebarTokens
            {
                Background = "oklch(0.205 0 0)",
                Foreground = "oklch(0.985 0 0)",
                Primary = "oklch(0.488 0.243 264.376)",
                PrimaryForeground = "oklch(0.985 0 0)",
                Accent = "oklch(0.269 0 0)",
                AccentForeground = "oklch(0.985 0 0)",
                Border = "oklch(0.269 0 0)",
                Ring = "oklch(0.439 0 0)"
            }
        };
    }
}

/// <summary>
/// Chart palette tokens used by the shadcn theme contract.
/// </summary>
public sealed class ThemeChartTokens
{
    public string First { get; set; } = "";
    public string Second { get; set; } = "";
    public string Third { get; set; } = "";
    public string Fourth { get; set; } = "";
    public string Fifth { get; set; } = "";
}

/// <summary>
/// Sidebar semantic tokens used by the shadcn theme contract.
/// </summary>
public sealed class ThemeSidebarTokens
{
    public string Background { get; set; } = "";
    public string Foreground { get; set; } = "";
    public string Primary { get; set; } = "";
    public string PrimaryForeground { get; set; } = "";
    public string Accent { get; set; } = "";
    public string AccentForeground { get; set; } = "";
    public string Border { get; set; } = "";
    public string Ring { get; set; } = "";
}
