using System.Collections.Generic;
using System.Text;
using Soenneker.Quark.Tokens;

namespace Soenneker.Quark;

/// <summary>
/// Generates the shadcn-style CSS variable and <c>@theme inline</c> blocks from a <see cref="Theme"/>.
/// </summary>
public static class ThemeTailwindCssGenerator
{
    public static string Generate(Theme theme)
    {
        if (theme is null)
            return string.Empty;

        var tokens = theme.Tokens ?? new ThemeTokens();

        var builder = new StringBuilder(1024);

        AppendScheme(builder, ":root", tokens.Light);
        builder.AppendLine();
        AppendScheme(builder, ".dark", tokens.Dark);
        builder.AppendLine();
        AppendInlineTheme(builder, tokens.InlineVariables);

        return builder.ToString().TrimEnd();
    }

    private static void AppendScheme(StringBuilder builder, string selector, ThemeTokenScheme scheme)
    {
        builder.AppendLine(selector);
        builder.AppendLine("{");

        AppendVariable(builder, "background", scheme.Background);
        AppendVariable(builder, "foreground", scheme.Foreground);
        AppendVariable(builder, "card", scheme.Card);
        AppendVariable(builder, "card-foreground", scheme.CardForeground);
        AppendVariable(builder, "popover", scheme.Popover);
        AppendVariable(builder, "popover-foreground", scheme.PopoverForeground);
        AppendVariable(builder, "primary", scheme.Primary);
        AppendVariable(builder, "primary-foreground", scheme.PrimaryForeground);
        AppendVariable(builder, "secondary", scheme.Secondary);
        AppendVariable(builder, "secondary-foreground", scheme.SecondaryForeground);
        AppendVariable(builder, "muted", scheme.Muted);
        AppendVariable(builder, "muted-foreground", scheme.MutedForeground);
        AppendVariable(builder, "accent", scheme.Accent);
        AppendVariable(builder, "accent-foreground", scheme.AccentForeground);
        AppendVariable(builder, "destructive", scheme.Destructive);
        AppendVariable(builder, "destructive-foreground", scheme.DestructiveForeground);
        AppendVariable(builder, "border", scheme.Border);
        AppendVariable(builder, "input", scheme.Input);
        AppendVariable(builder, "ring", scheme.Ring);
        AppendVariable(builder, "chart-1", scheme.Chart.First);
        AppendVariable(builder, "chart-2", scheme.Chart.Second);
        AppendVariable(builder, "chart-3", scheme.Chart.Third);
        AppendVariable(builder, "chart-4", scheme.Chart.Fourth);
        AppendVariable(builder, "chart-5", scheme.Chart.Fifth);
        AppendVariable(builder, "radius", scheme.Radius);
        AppendVariable(builder, "sidebar", scheme.Sidebar.Background);
        AppendVariable(builder, "sidebar-foreground", scheme.Sidebar.Foreground);
        AppendVariable(builder, "sidebar-primary", scheme.Sidebar.Primary);
        AppendVariable(builder, "sidebar-primary-foreground", scheme.Sidebar.PrimaryForeground);
        AppendVariable(builder, "sidebar-accent", scheme.Sidebar.Accent);
        AppendVariable(builder, "sidebar-accent-foreground", scheme.Sidebar.AccentForeground);
        AppendVariable(builder, "sidebar-border", scheme.Sidebar.Border);
        AppendVariable(builder, "sidebar-ring", scheme.Sidebar.Ring);

        AppendCustomVariables(builder, scheme.Variables);

        builder.AppendLine("}");
    }

    private static void AppendInlineTheme(StringBuilder builder, IReadOnlyDictionary<string, string> inlineVariables)
    {
        builder.AppendLine("@theme inline");
        builder.AppendLine("{");

        AppendVariable(builder, "color-background", "var(--background)");
        AppendVariable(builder, "color-foreground", "var(--foreground)");
        AppendVariable(builder, "color-card", "var(--card)");
        AppendVariable(builder, "color-card-foreground", "var(--card-foreground)");
        AppendVariable(builder, "color-popover", "var(--popover)");
        AppendVariable(builder, "color-popover-foreground", "var(--popover-foreground)");
        AppendVariable(builder, "color-primary", "var(--primary)");
        AppendVariable(builder, "color-primary-foreground", "var(--primary-foreground)");
        AppendVariable(builder, "color-secondary", "var(--secondary)");
        AppendVariable(builder, "color-secondary-foreground", "var(--secondary-foreground)");
        AppendVariable(builder, "color-muted", "var(--muted)");
        AppendVariable(builder, "color-muted-foreground", "var(--muted-foreground)");
        AppendVariable(builder, "color-accent", "var(--accent)");
        AppendVariable(builder, "color-accent-foreground", "var(--accent-foreground)");
        AppendVariable(builder, "color-destructive", "var(--destructive)");
        AppendVariable(builder, "color-destructive-foreground", "var(--destructive-foreground)");
        AppendVariable(builder, "color-border", "var(--border)");
        AppendVariable(builder, "color-input", "var(--input)");
        AppendVariable(builder, "color-ring", "var(--ring)");
        AppendVariable(builder, "color-chart-1", "var(--chart-1)");
        AppendVariable(builder, "color-chart-2", "var(--chart-2)");
        AppendVariable(builder, "color-chart-3", "var(--chart-3)");
        AppendVariable(builder, "color-chart-4", "var(--chart-4)");
        AppendVariable(builder, "color-chart-5", "var(--chart-5)");
        AppendVariable(builder, "radius-sm", "calc(var(--radius) - 4px)");
        AppendVariable(builder, "radius-md", "calc(var(--radius) - 2px)");
        AppendVariable(builder, "radius-lg", "var(--radius)");
        AppendVariable(builder, "radius-xl", "calc(var(--radius) + 4px)");
        AppendVariable(builder, "color-sidebar", "var(--sidebar)");
        AppendVariable(builder, "color-sidebar-foreground", "var(--sidebar-foreground)");
        AppendVariable(builder, "color-sidebar-primary", "var(--sidebar-primary)");
        AppendVariable(builder, "color-sidebar-primary-foreground", "var(--sidebar-primary-foreground)");
        AppendVariable(builder, "color-sidebar-accent", "var(--sidebar-accent)");
        AppendVariable(builder, "color-sidebar-accent-foreground", "var(--sidebar-accent-foreground)");
        AppendVariable(builder, "color-sidebar-border", "var(--sidebar-border)");
        AppendVariable(builder, "color-sidebar-ring", "var(--sidebar-ring)");

        AppendCustomVariables(builder, inlineVariables);

        builder.AppendLine("}");
    }

    private static void AppendCustomVariables(StringBuilder builder, IReadOnlyDictionary<string, string>? values)
    {
        if (values is null || values.Count == 0)
            return;

        foreach ((var key, var value) in values)
        {
            AppendVariable(builder, key, value);
        }
    }

    private static void AppendVariable(StringBuilder builder, string name, string? value)
    {
        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(value))
            return;

        builder.Append("  --");
        builder.Append(name.Trim());
        builder.Append(": ");
        builder.Append(value.Trim());
        builder.AppendLine(";");
    }
}
