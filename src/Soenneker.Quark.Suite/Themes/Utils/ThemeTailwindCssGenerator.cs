using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;
using Soenneker.Quark.Tokens;

namespace Soenneker.Quark;

/// <summary>
/// Generates the shadcn-style CSS variable and <c>@theme inline</c> blocks from a <see cref="Theme"/>.
/// </summary>
public static class ThemeTailwindCssGenerator
{
    /// <summary>
    /// Generates theme Tailwind CSS Generator.
    /// </summary>
    /// <param name="theme">Theme for the generate operation.</param>
    /// <returns>The text produced by generate.</returns>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static string Generate(Theme theme)
    {
        if (theme is null)
            return string.Empty;

        var tokens = theme.Tokens ?? new ThemeTokens();

        var builder = new PooledStringBuilder(stackalloc char[128]);
        try
        {
            AppendScheme(ref builder, ":root", tokens.Light);
            builder.Append(Environment.NewLine);
            AppendScheme(ref builder, ".dark", tokens.Dark);
            builder.Append(Environment.NewLine);
            AppendInlineTheme(ref builder, tokens.InlineVariables);
            return builder.AsSpan().TrimEnd().ToString();
        }
        finally
        {
            builder.Dispose();
        }
    }

    private static void AppendScheme(ref PooledStringBuilder builder, string selector, ThemeTokenScheme scheme)
    {
        builder.Append(selector);
        builder.Append(Environment.NewLine);
        builder.Append("{");
        builder.Append(Environment.NewLine);

        AppendVariable(ref builder, "background", scheme.Background);
        AppendVariable(ref builder, "foreground", scheme.Foreground);
        AppendVariable(ref builder, "card", scheme.Card);
        AppendVariable(ref builder, "card-foreground", scheme.CardForeground);
        AppendVariable(ref builder, "popover", scheme.Popover);
        AppendVariable(ref builder, "popover-foreground", scheme.PopoverForeground);
        AppendVariable(ref builder, "primary", scheme.Primary);
        AppendVariable(ref builder, "primary-foreground", scheme.PrimaryForeground);
        AppendVariable(ref builder, "secondary", scheme.Secondary);
        AppendVariable(ref builder, "secondary-foreground", scheme.SecondaryForeground);
        AppendVariable(ref builder, "muted", scheme.Muted);
        AppendVariable(ref builder, "muted-foreground", scheme.MutedForeground);
        AppendVariable(ref builder, "accent", scheme.Accent);
        AppendVariable(ref builder, "accent-foreground", scheme.AccentForeground);
        AppendVariable(ref builder, "destructive", scheme.Destructive);
        AppendVariable(ref builder, "destructive-foreground", scheme.DestructiveForeground);
        AppendVariable(ref builder, "border", scheme.Border);
        AppendVariable(ref builder, "input", scheme.Input);
        AppendVariable(ref builder, "ring", scheme.Ring);
        AppendVariable(ref builder, "series-1", scheme.Series.First);
        AppendVariable(ref builder, "series-2", scheme.Series.Second);
        AppendVariable(ref builder, "series-3", scheme.Series.Third);
        AppendVariable(ref builder, "series-4", scheme.Series.Fourth);
        AppendVariable(ref builder, "series-5", scheme.Series.Fifth);
        AppendVariable(ref builder, "radius", scheme.Radius);
        AppendVariable(ref builder, "sidebar", scheme.Sidebar.Background);
        AppendVariable(ref builder, "sidebar-foreground", scheme.Sidebar.Foreground);
        AppendVariable(ref builder, "sidebar-primary", scheme.Sidebar.Primary);
        AppendVariable(ref builder, "sidebar-primary-foreground", scheme.Sidebar.PrimaryForeground);
        AppendVariable(ref builder, "sidebar-accent", scheme.Sidebar.Accent);
        AppendVariable(ref builder, "sidebar-accent-foreground", scheme.Sidebar.AccentForeground);
        AppendVariable(ref builder, "sidebar-border", scheme.Sidebar.Border);
        AppendVariable(ref builder, "sidebar-ring", scheme.Sidebar.Ring);

        AppendCustomVariables(ref builder, scheme.Variables);

        builder.Append("}");
        builder.Append(Environment.NewLine);
    }

    private static void AppendInlineTheme(ref PooledStringBuilder builder, IReadOnlyDictionary<string, string> inlineVariables)
    {
        builder.Append("@theme inline");
        builder.Append(Environment.NewLine);
        builder.Append("{");
        builder.Append(Environment.NewLine);

        AppendVariable(ref builder, "color-background", "var(--background)");
        AppendVariable(ref builder, "color-foreground", "var(--foreground)");
        AppendVariable(ref builder, "color-card", "var(--card)");
        AppendVariable(ref builder, "color-card-foreground", "var(--card-foreground)");
        AppendVariable(ref builder, "color-popover", "var(--popover)");
        AppendVariable(ref builder, "color-popover-foreground", "var(--popover-foreground)");
        AppendVariable(ref builder, "color-primary", "var(--primary)");
        AppendVariable(ref builder, "color-primary-foreground", "var(--primary-foreground)");
        AppendVariable(ref builder, "color-secondary", "var(--secondary)");
        AppendVariable(ref builder, "color-secondary-foreground", "var(--secondary-foreground)");
        AppendVariable(ref builder, "color-muted", "var(--muted)");
        AppendVariable(ref builder, "color-muted-foreground", "var(--muted-foreground)");
        AppendVariable(ref builder, "color-accent", "var(--accent)");
        AppendVariable(ref builder, "color-accent-foreground", "var(--accent-foreground)");
        AppendVariable(ref builder, "color-destructive", "var(--destructive)");
        AppendVariable(ref builder, "color-destructive-foreground", "var(--destructive-foreground)");
        AppendVariable(ref builder, "color-border", "var(--border)");
        AppendVariable(ref builder, "color-input", "var(--input)");
        AppendVariable(ref builder, "color-ring", "var(--ring)");
        AppendVariable(ref builder, "color-surface", "var(--surface)");
        AppendVariable(ref builder, "color-surface-foreground", "var(--surface-foreground)");
        AppendVariable(ref builder, "color-code", "var(--code)");
        AppendVariable(ref builder, "color-code-foreground", "var(--code-foreground)");
        AppendVariable(ref builder, "color-code-highlight", "var(--code-highlight)");
        AppendVariable(ref builder, "color-code-number", "var(--code-number)");
        AppendVariable(ref builder, "color-selection", "var(--selection)");
        AppendVariable(ref builder, "color-series-1", "var(--series-1)");
        AppendVariable(ref builder, "color-series-2", "var(--series-2)");
        AppendVariable(ref builder, "color-series-3", "var(--series-3)");
        AppendVariable(ref builder, "color-series-4", "var(--series-4)");
        AppendVariable(ref builder, "color-series-5", "var(--series-5)");
        AppendVariable(ref builder, "radius-sm", "calc(var(--radius) - 4px)");
        AppendVariable(ref builder, "radius-md", "calc(var(--radius) - 2px)");
        AppendVariable(ref builder, "radius-lg", "var(--radius)");
        AppendVariable(ref builder, "radius-xl", "calc(var(--radius) + 4px)");
        AppendVariable(ref builder, "radius-2xl", "calc(var(--radius) + 8px)");
        AppendVariable(ref builder, "radius-3xl", "calc(var(--radius) + 12px)");
        AppendVariable(ref builder, "radius-4xl", "calc(var(--radius) * 2.6)");
        AppendVariable(ref builder, "color-sidebar", "var(--sidebar)");
        AppendVariable(ref builder, "color-sidebar-foreground", "var(--sidebar-foreground)");
        AppendVariable(ref builder, "color-sidebar-primary", "var(--sidebar-primary)");
        AppendVariable(ref builder, "color-sidebar-primary-foreground", "var(--sidebar-primary-foreground)");
        AppendVariable(ref builder, "color-sidebar-accent", "var(--sidebar-accent)");
        AppendVariable(ref builder, "color-sidebar-accent-foreground", "var(--sidebar-accent-foreground)");
        AppendVariable(ref builder, "color-sidebar-border", "var(--sidebar-border)");
        AppendVariable(ref builder, "color-sidebar-ring", "var(--sidebar-ring)");

        AppendCustomVariables(ref builder, inlineVariables);

        builder.Append("}");
        builder.Append(Environment.NewLine);
    }

    private static void AppendCustomVariables(ref PooledStringBuilder builder, IReadOnlyDictionary<string, string>? values)
    {
        if (values is null || values.Count == 0)
            return;

        foreach ((var key, var value) in values)
        {
            AppendVariable(ref builder, key, value);
        }
    }

    private static void AppendVariable(ref PooledStringBuilder builder, string name, string? value)
    {
        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(value))
            return;

        builder.Append("  --");
        builder.Append(name.AsSpan().Trim());
        builder.Append(": ");
        builder.Append(value.AsSpan().Trim());
        builder.AppendLine(";");
    }
}
