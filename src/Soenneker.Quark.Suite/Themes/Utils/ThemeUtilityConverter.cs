using System;
using System.Globalization;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

internal static class ThemeUtilityConverter
{
    internal static void AddRules<TBuilder>(ref ComponentCssRuleCollector buffer, string selector, string property, ThemeValue<TBuilder>? value)
        where TBuilder : class, ICssBuilder
    {
        if (value is not { } configured || configured.Value is null)
            return;
        selector = configured.ResolveSelector(selector);
        if (!configured.IsUtility)
        {
            AddDeclaration(ref buffer, selector, property, configured.Value);
            return;
        }
        foreach (var utility in configured.Value.Split(' ', StringSplitOptions.RemoveEmptyEntries))
        {
            var target = selector;
            var depth = 0;
            var start = 0;
            for (var i = 0; i < utility.Length; i++)
            {
                if (utility[i] is '[' or '(') depth++;
                else if (utility[i] is ']' or ')') depth--;
                else if (utility[i] == ':' && depth == 0)
                {
                    var variant = utility[start..i];
                    target = ThemeSelector.Resolve(target, variant switch
                    {
                        "hover" => "&:hover", "focus" => "&:focus", "focus-visible" => "&:focus-visible",
                        "active" => "&:active", "disabled" => "&:disabled", "checked" => "&:checked",
                        _ when variant.StartsWith('[') && variant.EndsWith(']') => variant[1..^1].Replace('_', ' '),
                        _ => throw new NotSupportedException($"Theme variant '{variant}' is not supported. Use an explicit theme rule.")
                    });
                    start = i + 1;
                }
            }
            var token = utility[start..];
            CollectClassOnlyDeclarations<TBuilder>(ref buffer, target, token, property);
        }
    }

    private static void AddDeclaration(ref ComponentCssRuleCollector buffer, string selector, string property, string? value)
    {
        if (value is null)
            throw new NotSupportedException($"Unable to resolve a theme utility for CSS property '{property}'.");
        buffer.Add(selector, new ThemeCssDeclaration(property, value));
    }

    private static void CollectClassOnlyDeclarations<TBuilder>(ref ComponentCssRuleCollector buffer, string selector, string rawValue, string fallbackProperty)
        where TBuilder : class, ICssBuilder
    {
        var resolved = rawValue.Trim();

        if (typeof(TBuilder) == typeof(RingBuilder))
        {
            var width = resolved == "ring" ? "1px" : resolved.StartsWith("ring-", StringComparison.Ordinal)
                ? ConvertArbitraryToken(resolved.AsSpan(5)) ?? (decimal.TryParse(resolved.AsSpan(5), CultureInfo.InvariantCulture, out _) ? resolved[5..] + "px" : null) : null;
            if (width is null)
                throw new NotSupportedException($"Unsupported theme ring utility '{resolved}'.");
            AddDeclaration(ref buffer, selector, "--tw-ring-shadow", $"var(--tw-ring-inset,) 0 0 0 calc({width} + var(--tw-ring-offset-width, 0px)) var(--tw-ring-color, currentColor)");
            AddDeclaration(ref buffer, selector, "box-shadow", "var(--tw-inset-shadow, 0 0 #0000), var(--tw-inset-ring-shadow, 0 0 #0000), var(--tw-ring-offset-shadow, 0 0 #0000), var(--tw-ring-shadow), var(--tw-shadow, 0 0 #0000)");
            return;
        }
        if (typeof(TBuilder) == typeof(LeadingBuilder))
        {
            var token = resolved["leading-".Length..];
            AddDeclaration(ref buffer, selector, fallbackProperty, token switch
            {
                "none" => "1", "tight" => "1.25", "snug" => "1.375", "normal" => "1.5", "relaxed" => "1.625", "loose" => "2",
                _ => ConvertArbitraryToken(token) ?? ConvertSpacingScaleToken(token)
            });
            return;
        }
        if (typeof(TBuilder) == typeof(JustifyBuilder))
        {
            var token = resolved["justify-".Length..];
            AddDeclaration(ref buffer, selector, fallbackProperty, token switch
            {
                "start" => "flex-start", "end" => "flex-end", "between" => "space-between",
                "around" => "space-around", "evenly" => "space-evenly", _ => token
            });
            return;
        }
        if (typeof(TBuilder) == typeof(MarginBuilder))
        {
            CollectSpacingUtility(ref buffer, selector, resolved, "m", "margin");
            return;
        }
        if (typeof(TBuilder) == typeof(TransitionBuilder))
        {
            var token = resolved == "transition" ? "all" : resolved["transition-".Length..];
            AddDeclaration(ref buffer, selector, "transition-property", token switch
            {
                "colors" => "color, background-color, border-color, outline-color, text-decoration-color, fill, stroke",
                "shadow" => "box-shadow", _ => ConvertArbitraryToken(token) ?? token
            });
            if (token != "none")
            {
                AddDeclaration(ref buffer, selector, "transition-timing-function", "var(--default-transition-timing-function, ease)");
                AddDeclaration(ref buffer, selector, "transition-duration", "var(--default-transition-duration, 0s)");
            }
            return;
        }

        if (typeof(TBuilder) == typeof(UnderlineOffsetBuilder) &&
            fallbackProperty.Equals("text-underline-offset", StringComparison.Ordinal))
        {
            var offset = resolved switch
            {
                "underline-offset-auto" => "auto",
                "underline-offset-0" => "0px",
                "underline-offset-1" => "1px",
                "underline-offset-2" => "2px",
                "underline-offset-4" => "4px",
                "underline-offset-8" => "8px",
                _ => null
            };

            AddDeclaration(ref buffer, selector, "text-underline-offset", offset);
            return;
        }

        if (typeof(TBuilder) == typeof(DecorationLineBuilder) &&
            fallbackProperty.Equals("text-decoration", StringComparison.Ordinal))
        {
            var decoration = resolved switch
            {
                "no-underline" => "none",
                "underline" => "underline",
                "line-through" => "line-through",
                "overline" => "overline",
                _ => null
            };

            AddDeclaration(ref buffer, selector, fallbackProperty, decoration);
            return;
        }

        if (typeof(TBuilder) == typeof(DisplayBuilder) &&
            fallbackProperty.Equals("display", StringComparison.Ordinal))
        {
            var display = ConvertSingleTokenUtility(resolved, static token => token switch
            {
                "hidden" => "none",
                "block" => "block",
                "inline" => "inline",
                "inline-block" => "inline-block",
                "flow-root" => "flow-root",
                "flex" => "flex",
                "inline-flex" => "inline-flex",
                "grid" => "grid",
                "inline-grid" => "inline-grid",
                "table" => "table",
                "table-caption" => "table-caption",
                "table-cell" => "table-cell",
                "table-column" => "table-column",
                "table-column-group" => "table-column-group",
                "table-footer-group" => "table-footer-group",
                "table-header-group" => "table-header-group",
                "table-row" => "table-row",
                "table-row-group" => "table-row-group",
                "contents" => "contents",
                "list-item" => "list-item",
                _ => null
            });

            AddDeclaration(ref buffer, selector, fallbackProperty, display);
            return;
        }

        if (typeof(TBuilder) == typeof(TextColorBuilder) &&
            fallbackProperty.Equals("color", StringComparison.Ordinal))
        {
            var color = ConvertColorUtility(resolved, "text-");
            AddDeclaration(ref buffer, selector, fallbackProperty, color);
            return;
        }

        if (typeof(TBuilder) == typeof(BackgroundColorBuilder) &&
            fallbackProperty.Equals("background-color", StringComparison.Ordinal))
        {
            var color = ConvertColorUtility(resolved, "bg-");
            AddDeclaration(ref buffer, selector, fallbackProperty, color);
            return;
        }

        if (typeof(TBuilder) == typeof(BorderColorBuilder) &&
            fallbackProperty.Equals("border-color", StringComparison.Ordinal))
        {
            var color = ConvertColorUtility(resolved, "border-");
            AddDeclaration(ref buffer, selector, fallbackProperty, color);
            return;
        }

        if (typeof(TBuilder) == typeof(PaddingBuilder) &&
            fallbackProperty.Equals("padding", StringComparison.Ordinal))
        {
            CollectPaddingUtilities(ref buffer, selector, resolved);
            return;
        }

        if (typeof(TBuilder) == typeof(WidthBuilder) &&
            (fallbackProperty.Equals("width", StringComparison.Ordinal) ||
             fallbackProperty.Equals("min-width", StringComparison.Ordinal) ||
             fallbackProperty.Equals("max-width", StringComparison.Ordinal)))
        {
            var width = ConvertWidthUtility(resolved);
            AddDeclaration(ref buffer, selector, fallbackProperty, width);
            return;
        }

        if ((typeof(TBuilder) == typeof(HeightBuilder) ||
             typeof(TBuilder) == typeof(MinHeightBuilder) ||
             typeof(TBuilder) == typeof(MaxHeightBuilder)) &&
            (fallbackProperty.Equals("height", StringComparison.Ordinal) ||
             fallbackProperty.Equals("min-height", StringComparison.Ordinal) ||
             fallbackProperty.Equals("max-height", StringComparison.Ordinal)))
        {
            var height = ConvertHeightUtility(resolved);
            AddDeclaration(ref buffer, selector, fallbackProperty, height);
            return;
        }

        if (typeof(TBuilder) == typeof(OverflowBuilder) &&
            (fallbackProperty.Equals("overflow", StringComparison.Ordinal) ||
             fallbackProperty.Equals("overflow-x", StringComparison.Ordinal) ||
             fallbackProperty.Equals("overflow-y", StringComparison.Ordinal)))
        {
            var overflow = ConvertSingleTokenUtility(resolved, static token => token switch
            {
                "overflow-auto" => "auto",
                "overflow-hidden" => "hidden",
                "overflow-clip" => "clip",
                "overflow-visible" => "visible",
                "overflow-scroll" => "scroll",
                _ => null
            });

            AddDeclaration(ref buffer, selector, fallbackProperty, overflow);
            return;
        }

        if (typeof(TBuilder) == typeof(TextSizeBuilder) &&
            fallbackProperty.Equals("font-size", StringComparison.Ordinal))
        {
            var textSize = ConvertTextSizeUtility(resolved);
            AddDeclaration(ref buffer, selector, fallbackProperty, textSize);
            return;
        }

        if (typeof(TBuilder) == typeof(ItemsBuilder) &&
            fallbackProperty.Equals("align-items", StringComparison.Ordinal))
        {
            var alignItems = ConvertSingleTokenUtility(resolved, static token => token switch
            {
                "items-start" => "flex-start",
                "items-end" => "flex-end",
                "items-center" => "center",
                "items-baseline" => "baseline",
                "items-stretch" => "stretch",
                _ => null
            });

            AddDeclaration(ref buffer, selector, fallbackProperty, alignItems);
            return;
        }

        if (typeof(TBuilder) == typeof(GapBuilder) &&
            fallbackProperty.Equals("gap", StringComparison.Ordinal))
        {
            var gap = ConvertSpacingUtility(resolved, "gap-");
            AddDeclaration(ref buffer, selector, fallbackProperty, gap);
            return;
        }

        if (typeof(TBuilder) == typeof(BorderBuilder) &&
            fallbackProperty.Equals("border", StringComparison.Ordinal))
        {
            CollectBorderUtilities(ref buffer, selector, resolved);
            return;
        }

        if (typeof(TBuilder) == typeof(VerticalAlignBuilder) &&
            fallbackProperty.Equals("vertical-align", StringComparison.Ordinal))
        {
            var verticalAlign = ConvertSingleTokenUtility(resolved, static token => token switch
            {
                "align-baseline" => "baseline",
                "align-top" => "top",
                "align-middle" => "middle",
                "align-bottom" => "bottom",
                "align-text-top" => "text-top",
                "align-text-bottom" => "text-bottom",
                "align-sub" => "sub",
                "align-super" => "super",
                _ => null
            });

            AddDeclaration(ref buffer, selector, fallbackProperty, verticalAlign);
            return;
        }

        if (typeof(TBuilder) == typeof(TextOverflowBuilder) &&
            fallbackProperty.Equals("text-overflow", StringComparison.Ordinal))
        {
            var textOverflow = ConvertSingleTokenUtility(resolved, static token => token switch
            {
                "text-ellipsis" => "ellipsis",
                "text-clip" => "clip",
                _ => null
            });

            AddDeclaration(ref buffer, selector, fallbackProperty, textOverflow);
            return;
        }

        if (typeof(TBuilder) == typeof(RoundedBuilder) &&
            fallbackProperty.Equals("border-radius", StringComparison.Ordinal))
        {
            var radius = ConvertRoundedUtility(resolved);
            AddDeclaration(ref buffer, selector, fallbackProperty, radius);
            return;
        }

        if (typeof(TBuilder) == typeof(ShadowBuilder) &&
            fallbackProperty.Equals("box-shadow", StringComparison.Ordinal))
        {
            var shadow = ConvertShadowUtility(resolved);
            // Ring utilities compose with this variable, including when applied by a state selector.
            AddDeclaration(ref buffer, selector, "--tw-shadow", shadow == "none" ? "0 0 #0000" : shadow);
            AddDeclaration(ref buffer, selector, fallbackProperty, shadow);
            return;
        }

        if (typeof(TBuilder) == typeof(FontWeightBuilder) &&
            fallbackProperty.Equals("font-weight", StringComparison.Ordinal))
        {
            var fontWeight = ConvertSingleTokenUtility(resolved, static token => token switch
            {
                "font-thin" => "100",
                "font-extralight" => "200",
                "font-light" => "300",
                "font-normal" => "400",
                "font-medium" => "500",
                "font-semibold" => "600",
                "font-bold" => "700",
                "font-extrabold" => "800",
                "font-black" => "900",
                _ => null
            });

            AddDeclaration(ref buffer, selector, fallbackProperty, fontWeight);
            return;
        }

        if (typeof(TBuilder) == typeof(WhitespaceBuilder) &&
            fallbackProperty.Equals("white-space", StringComparison.Ordinal))
        {
            var whitespace = ConvertSingleTokenUtility(resolved, static token => token switch
            {
                "whitespace-normal" => "normal",
                "whitespace-nowrap" => "nowrap",
                "whitespace-pre" => "pre",
                "whitespace-pre-line" => "pre-line",
                "whitespace-pre-wrap" => "pre-wrap",
                "whitespace-break-spaces" => "break-spaces",
                _ => null
            });

            AddDeclaration(ref buffer, selector, fallbackProperty, whitespace);
            return;
        }

        if (typeof(TBuilder) == typeof(FlexDirectionBuilder) &&
            fallbackProperty.Equals("flex-direction", StringComparison.Ordinal))
        {
            CollectFlexComposite(ref buffer, selector, resolved, fallbackProperty, static token => token switch
            {
                "flex-row" => "row",
                "flex-row-reverse" => "row-reverse",
                "flex-col" => "column",
                "flex-col-reverse" => "column-reverse",
                _ => null
            });
            return;
        }

        if (typeof(TBuilder) == typeof(FlexWrapBuilder) &&
            fallbackProperty.Equals("flex-wrap", StringComparison.Ordinal))
        {
            CollectFlexComposite(ref buffer, selector, resolved, fallbackProperty, static token => token switch
            {
                "flex-wrap" => "wrap",
                "flex-wrap-reverse" => "wrap-reverse",
                "flex-nowrap" => "nowrap",
                _ => null
            });
            return;
        }

        if (typeof(TBuilder) == typeof(GrowBuilder) &&
            fallbackProperty.Equals("flex-grow", StringComparison.Ordinal))
        {
            var grow = resolved switch
            {
                "grow" => "1",
                "grow-0" => "0",
                _ => null
            };

            AddDeclaration(ref buffer, selector, fallbackProperty, grow);
            return;
        }

        if (typeof(TBuilder) == typeof(ShrinkBuilder) &&
            fallbackProperty.Equals("flex-shrink", StringComparison.Ordinal))
        {
            var shrink = resolved switch
            {
                "shrink" => "1",
                "shrink-0" => "0",
                _ => null
            };

            AddDeclaration(ref buffer, selector, fallbackProperty, shrink);
            return;
        }

        if (typeof(TBuilder) == typeof(DurationBuilder) &&
            fallbackProperty.Equals("transition-duration", StringComparison.Ordinal))
        {
            var duration = ConvertDurationUtility(resolved);
            AddDeclaration(ref buffer, selector, fallbackProperty, duration);
            return;
        }

        throw new NotSupportedException($"Theme utility '{resolved}' for {typeof(TBuilder).Name} is not supported. Supply literal CSS or explicit declarations instead.");
    }

    private static void CollectSpacingUtility(ref ComponentCssRuleCollector buffer, string selector, string utility, string prefix, string property)
    {
        var negative = utility.StartsWith('-');
        if (negative) utility = utility[1..];
        var dash = utility.IndexOf('-');
        if (dash < 0) throw new NotSupportedException($"Unsupported spacing utility '{utility}'.");
        var side = utility[prefix.Length..dash];
        var token = utility[(dash + 1)..];
        var value = token == "auto" ? "auto" : ConvertArbitraryToken(token) ?? ConvertSpacingScaleToken(token);
        if (value is null) throw new NotSupportedException($"Unsupported spacing utility '{utility}'.");
        if (negative) value = $"calc({value} * -1)";
        var suffix = side switch
        {
            "" => "", "x" => "-inline", "y" => "-block", "t" => "-top", "r" => "-right", "b" => "-bottom", "l" => "-left",
            "s" => "-inline-start", "e" => "-inline-end", _ => throw new NotSupportedException($"Unsupported spacing side '{side}'.")
        };
        AddDeclaration(ref buffer, selector, property + suffix, value);
    }

    private static string? ConvertSingleTokenUtility(string value, Func<string, string?> converter)
    {
        if (value.Contains(':') || value.Contains(' '))
            return null;

        return converter(value);
    }

    private static string? ConvertColorUtility(ReadOnlySpan<char> value, string prefix)
    {
        if (!value.StartsWith(prefix, StringComparison.Ordinal) || value.Contains(':') || value.Contains('/'))
            return null;

        var token = value[prefix.Length..];
        var arbitrary = ConvertArbitraryToken(token);

        if (arbitrary is not null)
            return arbitrary;

        return token switch
        {
            "black" => "#000",
            "white" => "#fff",
            "transparent" => "transparent",
            "inherit" => "inherit",
            "current" => "currentColor",
            "background" => "var(--background)",
            "foreground" => "var(--foreground)",
            "card" => "var(--card)",
            "card-foreground" => "var(--card-foreground)",
            "popover" => "var(--popover)",
            "popover-foreground" => "var(--popover-foreground)",
            "primary" => "var(--primary)",
            "primary-foreground" => "var(--primary-foreground)",
            "secondary" => "var(--secondary)",
            "secondary-foreground" => "var(--secondary-foreground)",
            "muted" => "var(--muted)",
            "muted-foreground" => "var(--muted-foreground)",
            "accent" => "var(--accent)",
            "accent-foreground" => "var(--accent-foreground)",
            "destructive" => "var(--destructive)",
            "destructive-foreground" => "var(--destructive-foreground)",
            "border" => "var(--border)",
            _ => IsPaletteColorToken(token) ? $"var(--color-{token})" : $"var(--{token})"
        };
    }

    private static string? ConvertWidthUtility(ReadOnlySpan<char> value)
    {
        if (value.Contains(':') || value.Contains(' '))
            return null;

        var token = value;

        if (token.StartsWith("min-w-", StringComparison.Ordinal))
            token = token["min-w-".Length..];
        else if (token.StartsWith("max-w-", StringComparison.Ordinal))
            token = token["max-w-".Length..];
        else if (token.StartsWith("w-", StringComparison.Ordinal))
            token = token["w-".Length..];
        else
            return null;

        var arbitrary = ConvertArbitraryToken(token);
        if (arbitrary is not null)
            return arbitrary;

        var spacing = ConvertSpacingScaleToken(token);
        if (spacing is not null)
            return spacing;

        var fraction = ConvertFractionToken(token);
        if (fraction is not null)
            return fraction;

        return token switch
        {
            "auto" => "auto",
            "full" => "100%",
            "screen" => "100vw",
            "svw" => "100svw",
            "lvw" => "100lvw",
            "dvw" => "100dvw",
            "min" => "min-content",
            "max" => "max-content",
            "fit" => "fit-content",
            _ => null
        };
    }

    private static string? ConvertHeightUtility(ReadOnlySpan<char> value)
    {
        if (value.Contains(':') || value.Contains(' '))
            return null;

        var token = value;

        if (token.StartsWith("min-h-", StringComparison.Ordinal))
            token = token["min-h-".Length..];
        else if (token.StartsWith("max-h-", StringComparison.Ordinal))
            token = token["max-h-".Length..];
        else if (token.StartsWith("h-", StringComparison.Ordinal))
            token = token["h-".Length..];
        else
            return null;

        var arbitrary = ConvertArbitraryToken(token);
        if (arbitrary is not null)
            return arbitrary;

        var spacing = ConvertSpacingScaleToken(token);
        if (spacing is not null)
            return spacing;

        var fraction = ConvertFractionToken(token);
        if (fraction is not null)
            return fraction;

        return token switch
        {
            "auto" => "auto",
            "full" => "100%",
            "screen" => "100vh",
            "svh" => "100svh",
            "lvh" => "100lvh",
            "dvh" => "100dvh",
            "min" => "min-content",
            "max" => "max-content",
            "fit" => "fit-content",
            "none" => "none",
            _ => null
        };
    }

    private static string? ConvertTextSizeUtility(ReadOnlySpan<char> value)
    {
        if (!value.StartsWith("text-", StringComparison.Ordinal) || value.Contains(':') || value.Contains(' '))
            return null;

        var token = value["text-".Length..];

        var arbitrary = ConvertArbitraryToken(token);
        if (arbitrary is not null)
            return arbitrary;

        return token switch
        {
            "xs" => "var(--text-xs)",
            "sm" => "var(--text-sm)",
            "base" => "var(--text-base)",
            "lg" => "var(--text-lg)",
            "xl" => "var(--text-xl)",
            "2xl" => "var(--text-2xl)",
            "3xl" => "var(--text-3xl)",
            "4xl" => "var(--text-4xl)",
            "5xl" => "var(--text-5xl)",
            "6xl" => "var(--text-6xl)",
            "7xl" => "var(--text-7xl)",
            "8xl" => "var(--text-8xl)",
            "9xl" => "var(--text-9xl)",
            _ => null
        };
    }

    private static string? ConvertSpacingUtility(ReadOnlySpan<char> value, string prefix)
    {
        if (!value.StartsWith(prefix, StringComparison.Ordinal) || value.Contains(':') || value.Contains(' '))
            return null;

        var token = value[prefix.Length..];
        return ConvertArbitraryToken(token) ?? ConvertSpacingScaleToken(token);
    }

    private static void CollectBorderUtilities(ref ComponentCssRuleCollector buffer, string selector, ReadOnlySpan<char> value)
    {
        if (value.Contains(':') || value.Contains(' '))
            return;

        var property = value switch
        {
            "border" => "border-width",
            "border-x" => "border-inline-width",
            "border-y" => "border-block-width",
            "border-s" => "border-inline-start-width",
            "border-e" => "border-inline-end-width",
            "border-t" => "border-top-width",
            "border-r" => "border-right-width",
            "border-b" => "border-bottom-width",
            "border-l" => "border-left-width",
            _ => null
        };

        ReadOnlySpan<char> token;

        if (property is not null)
            token = "1";
        else if (value.StartsWith("border-", StringComparison.Ordinal))
        {
            var suffix = value["border-".Length..];
            var dash = suffix.IndexOf('-');

            if (dash > 0)
            {
                var side = suffix[..dash];
                token = suffix[(dash + 1)..];
                property = side switch
                {
                    "x" => "border-inline-width",
                    "y" => "border-block-width",
                    "s" => "border-inline-start-width",
                    "e" => "border-inline-end-width",
                    "t" => "border-top-width",
                    "r" => "border-right-width",
                    "b" => "border-bottom-width",
                    "l" => "border-left-width",
                    _ => null
                };
            }
            else
            {
                token = suffix;
                property = "border-width";
            }
        }
        else
            return;

        if (property is null)
            return;

        var width = ConvertBorderWidthToken(token);
        if (width is null)
            return;
        AddDeclaration(ref buffer, selector, property, width);
        var styleProperty = property switch
        {
            "border-inline-width" => "border-inline-style",
            "border-block-width" => "border-block-style",
            "border-inline-start-width" => "border-inline-start-style",
            "border-inline-end-width" => "border-inline-end-style",
            "border-top-width" => "border-top-style",
            "border-right-width" => "border-right-style",
            "border-bottom-width" => "border-bottom-style",
            "border-left-width" => "border-left-style",
            _ => "border-style"
        };
        AddDeclaration(ref buffer, selector, styleProperty, "solid");
    }

    private static string? ConvertBorderWidthToken(ReadOnlySpan<char> token)
    {
        var arbitrary = ConvertArbitraryToken(token);
        if (arbitrary is not null)
            return arbitrary;

        return token switch
        {
            "0" => "0",
            "1" => "1px",
            "2" => "2px",
            "4" => "4px",
            "8" => "8px",
            _ => null
        };
    }

    private static string? ConvertFractionToken(ReadOnlySpan<char> token)
    {
        return token switch
        {
            "1/2" => "50%",
            "1/3" => "33.333333%",
            "2/3" => "66.666667%",
            "1/4" => "25%",
            "2/4" => "50%",
            "3/4" => "75%",
            "1/5" => "20%",
            "2/5" => "40%",
            "3/5" => "60%",
            "4/5" => "80%",
            "1/6" => "16.666667%",
            "5/6" => "83.333333%",
            "1/12" => "8.333333%",
            "2/12" => "16.666667%",
            "3/12" => "25%",
            "4/12" => "33.333333%",
            "5/12" => "41.666667%",
            "6/12" => "50%",
            "7/12" => "58.333333%",
            "8/12" => "66.666667%",
            "9/12" => "75%",
            "10/12" => "83.333333%",
            "11/12" => "91.666667%",
            _ => null
        };
    }

    private static string? ConvertArbitraryToken(ReadOnlySpan<char> token)
    {
        if (token.Length < 2)
            return null;

        if (token[0] == '[' && token[^1] == ']')
            return string.Create(token.Length - 2, token, static (destination, source) =>
            {
                source[1..^1].CopyTo(destination);
                destination.Replace('_', ' ');
            });

        if (token[0] == '(' && token[^1] == ')')
            return $"var({token[1..^1]})";

        return null;
    }

    private static bool IsPaletteColorToken(ReadOnlySpan<char> token)
    {
        var dash = token.LastIndexOf('-');
        if (dash <= 0 || dash == token.Length - 1)
            return false;

        var family = token[..dash];
        var shade = token[(dash + 1)..];

        return IsPaletteColorFamily(family) && IsPaletteColorShade(shade);
    }

    private static bool IsPaletteColorFamily(ReadOnlySpan<char> family)
    {
        return family is "slate" or "gray" or "zinc" or "neutral" or "stone" or "red" or "orange" or "amber"
            or "yellow" or "lime" or "green" or "emerald" or "teal" or "cyan" or "sky" or "blue"
            or "indigo" or "violet" or "purple" or "fuchsia" or "pink" or "rose";
    }

    private static bool IsPaletteColorShade(ReadOnlySpan<char> shade)
    {
        return shade is "50" or "100" or "200" or "300" or "400" or "500" or "600" or "700" or "800" or "900" or "950";
    }

    private static void CollectPaddingUtilities(ref ComponentCssRuleCollector buffer, string selector, string value)
    {
        ReadOnlySpan<char> tokens = value;
        // Validate the whole composite first: unsupported tokens must not leave partial CSS.
        foreach (var range in tokens.Split(' '))
        {
            var token = tokens[range].Trim();
            if (token.IsEmpty)
                continue;
            var dash = token.IndexOf('-');
            if (token.Contains(':') || dash < 0 || ConvertSpacingToken(token) is null ||
                token[..dash] is not ("p" or "px" or "py" or "pt" or "pr" or "pb" or "pl" or "ps" or "pe"))
                throw new NotSupportedException($"Unsupported padding utility '{token}'.");
        }

        foreach (var range in tokens.Split(' '))
        {
            var token = tokens[range].Trim();
            if (token.Length == 0)
                continue;

            var spacing = ConvertSpacingToken(token);

            if (token.StartsWith("px-", StringComparison.Ordinal))
            {
                AddDeclaration(ref buffer, selector, "padding-left", spacing);
                AddDeclaration(ref buffer, selector, "padding-right", spacing);
            }
            else if (token.StartsWith("py-", StringComparison.Ordinal))
            {
                AddDeclaration(ref buffer, selector, "padding-top", spacing);
                AddDeclaration(ref buffer, selector, "padding-bottom", spacing);
            }
            else if (token.StartsWith("pt-", StringComparison.Ordinal))
                AddDeclaration(ref buffer, selector, "padding-top", spacing);
            else if (token.StartsWith("pr-", StringComparison.Ordinal))
                AddDeclaration(ref buffer, selector, "padding-right", spacing);
            else if (token.StartsWith("pb-", StringComparison.Ordinal))
                AddDeclaration(ref buffer, selector, "padding-bottom", spacing);
            else if (token.StartsWith("pl-", StringComparison.Ordinal))
                AddDeclaration(ref buffer, selector, "padding-left", spacing);
            else if (token.StartsWith("ps-", StringComparison.Ordinal))
                AddDeclaration(ref buffer, selector, "padding-inline-start", spacing);
            else if (token.StartsWith("pe-", StringComparison.Ordinal))
                AddDeclaration(ref buffer, selector, "padding-inline-end", spacing);
            else if (token.StartsWith("p-", StringComparison.Ordinal))
                AddDeclaration(ref buffer, selector, "padding", spacing);
            else
                return;
        }
    }

    private static string? ConvertSpacingToken(ReadOnlySpan<char> utility)
    {
        var dash = utility.IndexOf('-');
        if (dash < 0 || dash == utility.Length - 1)
            return null;

        var token = utility[(dash + 1)..];

        return ConvertArbitraryToken(token) ?? ConvertSpacingScaleToken(token);
    }

    private static string? ConvertSpacingScaleToken(ReadOnlySpan<char> token)
    {
        return token switch
        {
            "0" => "0",
            "0.5" => "0.125rem",
            "1" => "0.25rem",
            "1.5" => "0.375rem",
            "2" => "0.5rem",
            "2.5" => "0.625rem",
            "3" => "0.75rem",
            "3.5" => "0.875rem",
            "4" => "1rem",
            "5" => "1.25rem",
            "6" => "1.5rem",
            "7" => "1.75rem",
            "8" => "2rem",
            "9" => "2.25rem",
            "10" => "2.5rem",
            "12" => "3rem",
            "14" => "3.5rem",
            "16" => "4rem",
            "20" => "5rem",
            "px" => "1px",
            _ => null
        };
    }

    private static string? ConvertRoundedUtility(string value)
    {
        return value switch
        {
            "rounded-none" => "0",
            "rounded-sm" => "0.125rem",
            "rounded" => "0.25rem",
            "rounded-md" => "0.375rem",
            "rounded-lg" => "0.5rem",
            "rounded-xl" => "0.75rem",
            "rounded-2xl" => "1rem",
            "rounded-3xl" => "1.5rem",
            "rounded-full" => "9999px",
            _ => null
        };
    }

    private static string? ConvertShadowUtility(string value)
    {
        return value switch
        {
            "shadow-none" => "none",
            "shadow-xs" => "0 1px 2px 0 rgb(0 0 0 / 0.05)",
            "shadow-sm" => "0 1px 3px 0 rgb(0 0 0 / 0.1), 0 1px 2px -1px rgb(0 0 0 / 0.1)",
            "shadow" => "0 1px 3px 0 rgb(0 0 0 / 0.1), 0 1px 2px -1px rgb(0 0 0 / 0.1)",
            "shadow-md" => "0 4px 6px -1px rgb(0 0 0 / 0.1), 0 2px 4px -2px rgb(0 0 0 / 0.1)",
            "shadow-lg" => "0 10px 15px -3px rgb(0 0 0 / 0.1), 0 4px 6px -4px rgb(0 0 0 / 0.1)",
            "shadow-xl" => "0 20px 25px -5px rgb(0 0 0 / 0.1), 0 8px 10px -6px rgb(0 0 0 / 0.1)",
            "shadow-2xl" => "0 25px 50px -12px rgb(0 0 0 / 0.25)",
            "shadow-inner" => "inset 0 2px 4px 0 rgb(0 0 0 / 0.05)",
            _ => null
        };
    }

    private static string? ConvertDurationUtility(ReadOnlySpan<char> value)
    {
        if (!value.StartsWith("duration-", StringComparison.Ordinal) || value.Contains(':'))
            return null;

        var token = value["duration-".Length..];

        if (token.Length == 0)
            return null;

        if (token.Length >= 2 && token[0] == '[' && token[^1] == ']')
            return token[1..^1].ToString();

        if (token.Length >= 2 && token[0] == '(' && token[^1] == ')')
            return $"var({token[1..^1]})";

        return token is "0" ? "0s" : string.Concat(token, "ms");
    }

    private static void CollectFlexComposite(ref ComponentCssRuleCollector buffer, string selector, string rawValue, string fallbackProperty, Func<ReadOnlySpan<char>, string?> converter)
    {
        var hasFlexDisplay = false;
        string? resolvedValue = null;
        var tokenStart = -1;

        for (var i = 0; i <= rawValue.Length; i++)
        {
            if (i < rawValue.Length && !char.IsWhiteSpace(rawValue[i]))
            {
                if (tokenStart < 0)
                    tokenStart = i;

                continue;
            }

            if (tokenStart < 0)
                continue;

            var token = rawValue.AsSpan(tokenStart, i - tokenStart);

            if (token.Contains(':'))
                return;

            if (token.Equals("flex", StringComparison.Ordinal))
            {
                hasFlexDisplay = true;
                tokenStart = -1;
                continue;
            }

            var converted = converter(token);
            if (converted is not null)
                resolvedValue = converted;

            tokenStart = -1;
        }

        if (resolvedValue is null)
            throw new NotSupportedException($"Unsupported theme utility '{rawValue}'.");

        if (hasFlexDisplay)
            AddDeclaration(ref buffer, selector, "display", "flex");
        AddDeclaration(ref buffer, selector, fallbackProperty, resolvedValue);
    }

}
