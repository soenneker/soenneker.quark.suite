using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Soenneker.Quark.Utils;

/// <summary>
/// Reduces Tailwind-like class strings by removing earlier conflicting utilities
/// and keeping the last matching utility within the same variant scope.
/// </summary>
/// <remarks>
/// This is intentionally a limited reducer, not a full tailwind-merge implementation.
/// It is suitable for common component composition scenarios in Quark where:
/// - classes are appended in precedence order
/// - later values should win
/// - only a practical subset of Tailwind conflicts need handling
/// </remarks>
public static class TailwindMerge
{
    private static readonly Dictionary<string, string> _exactGroups = new(StringComparer.Ordinal)
    {
        ["block"] = "display",
        ["inline-block"] = "display",
        ["inline"] = "display",
        ["flex"] = "display",
        ["inline-flex"] = "display",
        ["grid"] = "display",
        ["inline-grid"] = "display",
        ["hidden"] = "display",

        ["static"] = "position",
        ["fixed"] = "position",
        ["absolute"] = "position",
        ["relative"] = "position",
        ["sticky"] = "position",

        ["flex-row"] = "flex-direction",
        ["flex-row-reverse"] = "flex-direction",
        ["flex-col"] = "flex-direction",
        ["flex-col-reverse"] = "flex-direction",

        ["justify-start"] = "justify-content",
        ["justify-end"] = "justify-content",
        ["justify-center"] = "justify-content",
        ["justify-between"] = "justify-content",
        ["justify-around"] = "justify-content",
        ["justify-evenly"] = "justify-content",

        ["items-start"] = "align-items",
        ["items-end"] = "align-items",
        ["items-center"] = "align-items",
        ["items-baseline"] = "align-items",
        ["items-stretch"] = "align-items",

        ["font-thin"] = "font-weight",
        ["font-extralight"] = "font-weight",
        ["font-light"] = "font-weight",
        ["font-normal"] = "font-weight",
        ["font-medium"] = "font-weight",
        ["font-semibold"] = "font-weight",
        ["font-bold"] = "font-weight",
        ["font-extrabold"] = "font-weight",
        ["font-black"] = "font-weight",

        ["text-xs"] = "font-size",
        ["text-sm"] = "font-size",
        ["text-base"] = "font-size",
        ["text-lg"] = "font-size",
        ["text-xl"] = "font-size",
        ["text-2xl"] = "font-size",
        ["text-3xl"] = "font-size",
        ["text-4xl"] = "font-size",
        ["text-5xl"] = "font-size",
        ["text-6xl"] = "font-size",
        ["text-7xl"] = "font-size",
        ["text-8xl"] = "font-size",
        ["text-9xl"] = "font-size",

        ["rounded-none"] = "border-radius",
        ["rounded-sm"] = "border-radius",
        ["rounded"] = "border-radius",
        ["rounded-md"] = "border-radius",
        ["rounded-lg"] = "border-radius",
        ["rounded-xl"] = "border-radius",
        ["rounded-2xl"] = "border-radius",
        ["rounded-3xl"] = "border-radius",
        ["rounded-full"] = "border-radius",

        ["border-solid"] = "border-style",
        ["border-dashed"] = "border-style",
        ["border-dotted"] = "border-style",
        ["border-double"] = "border-style",
        ["border-none"] = "border-style",

        ["pointer-events-none"] = "pointer-events",
        ["pointer-events-auto"] = "pointer-events",

        ["cursor-default"] = "cursor",
        ["cursor-pointer"] = "cursor",
        ["cursor-not-allowed"] = "cursor",
        ["cursor-crosshair"] = "cursor",
        ["cursor-grab"] = "cursor",
        ["cursor-grabbing"] = "cursor",
        ["cursor-col-resize"] = "cursor",
        ["cursor-row-resize"] = "cursor",
        ["cursor-e-resize"] = "cursor",
        ["cursor-w-resize"] = "cursor",
        ["cursor-wait"] = "cursor",
        ["cursor-text"] = "cursor",
        ["cursor-move"] = "cursor",
        ["cursor-help"] = "cursor",
        ["cursor-none"] = "cursor",
        ["cursor-auto"] = "cursor",
    };

    /// <summary>
    /// Merges one or more class-string fragments into a reduced class string.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string Merge(params string?[]? values)
    {
        if (values is null || values.Length == 0)
            return string.Empty;

        var tokens = new List<Token>(32);
        var winningIndexes = new Dictionary<string, int>(StringComparer.Ordinal);

        for (int i = 0; i < values.Length; i++)
        {
            string? value = values[i];
            if (string.IsNullOrWhiteSpace(value))
                continue;

            ReadOnlySpan<char> span = value.AsSpan();
            int pos = 0;

            while (TryReadNextToken(span, ref pos, out ReadOnlySpan<char> tokenSpan))
            {
                if (!IsProbablyValidClassToken(tokenSpan))
                    continue;

                string token = tokenSpan.ToString();

                string? groupKey = GetGroupKey(tokenSpan);
                if (groupKey is not null)
                {
                    if (winningIndexes.TryGetValue(groupKey, out int priorIndex))
                        tokens[priorIndex] = default;

                    winningIndexes[groupKey] = tokens.Count;
                }

                tokens.Add(new Token(token));
            }
        }

        return JoinTokens(tokens);
    }

    private static string JoinTokens(List<Token> tokens)
    {
        int count = 0;
        int totalLength = 0;

        for (int i = 0; i < tokens.Count; i++)
        {
            string? value = tokens[i].Value;
            if (value is null)
                continue;

            count++;
            totalLength += value.Length;
        }

        if (count == 0)
            return string.Empty;

        return string.Create(totalLength + (count - 1), tokens, static (dst, src) =>
        {
            int pos = 0;

            for (int i = 0; i < src.Count; i++)
            {
                string? value = src[i].Value;
                if (value is null)
                    continue;

                if (pos != 0)
                    dst[pos++] = ' ';

                value.AsSpan().CopyTo(dst[pos..]);
                pos += value.Length;
            }
        });
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool TryReadNextToken(ReadOnlySpan<char> span, ref int pos, out ReadOnlySpan<char> token)
    {
        while ((uint)pos < (uint)span.Length && char.IsWhiteSpace(span[pos]))
            pos++;

        if (pos >= span.Length)
        {
            token = default;
            return false;
        }

        int start = pos;

        while ((uint)pos < (uint)span.Length && !char.IsWhiteSpace(span[pos]))
            pos++;

        token = span[start..pos];
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool IsProbablyValidClassToken(ReadOnlySpan<char> token)
    {
        if (token.Length == 0 || token.Length > 256)
            return false;

        for (int i = 0; i < token.Length; i++)
        {
            char c = token[i];

            if (char.IsLetterOrDigit(c))
                continue;

            switch (c)
            {
                case '-':
                case '_':
                case ':':
                case '/':
                case '[':
                case ']':
                case '(':
                case ')':
                case '.':
                case '%':
                case '!':
                case '@':
                case '#':
                case '&':
                case '+':
                case '=':
                case ',':
                case '*':
                    continue;
            }

            return false;
        }

        return true;
    }

    private static string? GetGroupKey(ReadOnlySpan<char> token)
    {
        SplitVariants(token, out ReadOnlySpan<char> variants, out ReadOnlySpan<char> utility);

        if (utility.Length == 0)
            return null;

        string? utilityGroup = GetUtilityGroup(utility);
        if (utilityGroup is null)
            return null;

        if (variants.Length == 0)
            return utilityGroup;

        return string.Concat(variants, "|", utilityGroup);
    }

    private static void SplitVariants(ReadOnlySpan<char> token, out ReadOnlySpan<char> variants, out ReadOnlySpan<char> utility)
    {
        int bracketDepth = 0;
        int parenDepth = 0;
        int lastColon = -1;

        for (int i = 0; i < token.Length; i++)
        {
            char c = token[i];

            switch (c)
            {
                case '[':
                    bracketDepth++;
                    break;
                case ']':
                    if (bracketDepth > 0)
                        bracketDepth--;
                    break;
                case '(':
                    parenDepth++;
                    break;
                case ')':
                    if (parenDepth > 0)
                        parenDepth--;
                    break;
                case ':':
                    if (bracketDepth == 0 && parenDepth == 0)
                        lastColon = i;
                    break;
            }
        }

        if (lastColon < 0)
        {
            variants = default;
            utility = token;
            return;
        }

        variants = token[..lastColon];
        utility = token[(lastColon + 1)..];
    }

    private static string? GetUtilityGroup(ReadOnlySpan<char> utility)
    {
        if (utility.Length == 0)
            return null;

        if (utility[0] == '!')
            utility = utility[1..];

        if (_exactGroups.TryGetValue(utility.ToString(), out string? exact))
            return exact;

        if (TryGetSpacingGroup(utility, out string? spacing))
            return spacing;

        if (TryGetSimplePrefixGroup(utility, "w-", "width"))
            return "width";

        if (TryGetSimplePrefixGroup(utility, "min-w-", "min-width"))
            return "min-width";

        if (TryGetSimplePrefixGroup(utility, "max-w-", "max-width"))
            return "max-width";

        if (TryGetSimplePrefixGroup(utility, "h-", "height"))
            return "height";

        if (TryGetSimplePrefixGroup(utility, "min-h-", "min-height"))
            return "min-height";

        if (TryGetSimplePrefixGroup(utility, "max-h-", "max-height"))
            return "max-height";

        if (TryGetSimplePrefixGroup(utility, "gap-", "gap"))
            return "gap";

        if (TryGetSimplePrefixGroup(utility, "gap-x-", "gap-x"))
            return "gap-x";

        if (TryGetSimplePrefixGroup(utility, "gap-y-", "gap-y"))
            return "gap-y";

        if (TryGetSimplePrefixGroup(utility, "opacity-", "opacity"))
            return "opacity";

        if (TryGetSimplePrefixGroup(utility, "z-", "z-index"))
            return "z-index";

        if (TryGetSimplePrefixGroup(utility, "grid-cols-", "grid-cols"))
            return "grid-cols";

        if (TryGetSimplePrefixGroup(utility, "grid-rows-", "grid-rows"))
            return "grid-rows";

        if (TryGetBorderWidthGroup(utility, out string? borderWidth))
            return borderWidth;

        if (TryGetColorLikeGroup(utility, "text-", out string? textGroup))
            return textGroup;

        if (TryGetColorLikeGroup(utility, "bg-", out string? bgGroup))
            return bgGroup;

        if (TryGetBorderColorGroup(utility, out string? borderColorGroup))
            return borderColorGroup;

        return null;
    }

    private static bool TryGetSpacingGroup(ReadOnlySpan<char> utility, out string? group)
    {
        if (TryMatchPrefixValue(utility, "p-")) { group = "padding"; return true; }
        if (TryMatchPrefixValue(utility, "px-")) { group = "padding-x"; return true; }
        if (TryMatchPrefixValue(utility, "py-")) { group = "padding-y"; return true; }
        if (TryMatchPrefixValue(utility, "pt-")) { group = "padding-top"; return true; }
        if (TryMatchPrefixValue(utility, "pr-")) { group = "padding-right"; return true; }
        if (TryMatchPrefixValue(utility, "pb-")) { group = "padding-bottom"; return true; }
        if (TryMatchPrefixValue(utility, "pl-")) { group = "padding-left"; return true; }

        if (TryMatchPrefixValue(utility, "m-")) { group = "margin"; return true; }
        if (TryMatchPrefixValue(utility, "mx-")) { group = "margin-x"; return true; }
        if (TryMatchPrefixValue(utility, "my-")) { group = "margin-y"; return true; }
        if (TryMatchPrefixValue(utility, "mt-")) { group = "margin-top"; return true; }
        if (TryMatchPrefixValue(utility, "mr-")) { group = "margin-right"; return true; }
        if (TryMatchPrefixValue(utility, "mb-")) { group = "margin-bottom"; return true; }
        if (TryMatchPrefixValue(utility, "ml-")) { group = "margin-left"; return true; }

        if (TryMatchPrefixValue(utility, "-m-")) { group = "margin"; return true; }
        if (TryMatchPrefixValue(utility, "-mx-")) { group = "margin-x"; return true; }
        if (TryMatchPrefixValue(utility, "-my-")) { group = "margin-y"; return true; }
        if (TryMatchPrefixValue(utility, "-mt-")) { group = "margin-top"; return true; }
        if (TryMatchPrefixValue(utility, "-mr-")) { group = "margin-right"; return true; }
        if (TryMatchPrefixValue(utility, "-mb-")) { group = "margin-bottom"; return true; }
        if (TryMatchPrefixValue(utility, "-ml-")) { group = "margin-left"; return true; }

        group = null;
        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool TryMatchPrefixValue(ReadOnlySpan<char> value, string prefix)
    {
        if (!value.StartsWith(prefix, StringComparison.Ordinal))
            return false;

        ReadOnlySpan<char> tail = value[prefix.Length..];
        return tail.Length > 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool TryGetSimplePrefixGroup(ReadOnlySpan<char> utility, string prefix, string group) =>
        utility.StartsWith(prefix, StringComparison.Ordinal) && utility.Length > prefix.Length;

    private static bool TryGetBorderWidthGroup(ReadOnlySpan<char> utility, out string? group)
    {
        if (utility.SequenceEqual("border".AsSpan()))
        {
            group = "border-width";
            return true;
        }

        if (utility.StartsWith("border-", StringComparison.Ordinal))
        {
            ReadOnlySpan<char> tail = utility["border-".Length..];

            if (tail.SequenceEqual("0".AsSpan()) || tail.SequenceEqual("2".AsSpan()) ||
                tail.SequenceEqual("4".AsSpan()) || tail.SequenceEqual("8".AsSpan()))
            {
                group = "border-width";
                return true;
            }

            if (tail.StartsWith("t-", StringComparison.Ordinal)) { group = "border-t-width"; return true; }
            if (tail.StartsWith("r-", StringComparison.Ordinal)) { group = "border-r-width"; return true; }
            if (tail.StartsWith("b-", StringComparison.Ordinal)) { group = "border-b-width"; return true; }
            if (tail.StartsWith("l-", StringComparison.Ordinal)) { group = "border-l-width"; return true; }
            if (tail.StartsWith("x-", StringComparison.Ordinal)) { group = "border-x-width"; return true; }
            if (tail.StartsWith("y-", StringComparison.Ordinal)) { group = "border-y-width"; return true; }
        }

        group = null;
        return false;
    }

    private static bool TryGetColorLikeGroup(ReadOnlySpan<char> utility, string prefix, out string? group)
    {
        if (!utility.StartsWith(prefix, StringComparison.Ordinal) || utility.Length <= prefix.Length)
        {
            group = null;
            return false;
        }

        if (prefix == "text-")
        {
            if (IsKnownTextSize(utility))
            {
                group = null;
                return false;
            }

            group = "text-color";
            return true;
        }

        group = prefix == "bg-" ? "background-color" : null;
        return group is not null;
    }

    private static bool TryGetBorderColorGroup(ReadOnlySpan<char> utility, out string? group)
    {
        if (!utility.StartsWith("border-", StringComparison.Ordinal) || utility.Length <= "border-".Length)
        {
            group = null;
            return false;
        }

        ReadOnlySpan<char> tail = utility["border-".Length..];

        if (tail.StartsWith("t-", StringComparison.Ordinal) ||
            tail.StartsWith("r-", StringComparison.Ordinal) ||
            tail.StartsWith("b-", StringComparison.Ordinal) ||
            tail.StartsWith("l-", StringComparison.Ordinal) ||
            tail.StartsWith("x-", StringComparison.Ordinal) ||
            tail.StartsWith("y-", StringComparison.Ordinal))
        {
            group = null;
            return false;
        }

        if (tail.SequenceEqual("solid".AsSpan()) ||
            tail.SequenceEqual("dashed".AsSpan()) ||
            tail.SequenceEqual("dotted".AsSpan()) ||
            tail.SequenceEqual("double".AsSpan()) ||
            tail.SequenceEqual("none".AsSpan()) ||
            tail.SequenceEqual("0".AsSpan()) ||
            tail.SequenceEqual("2".AsSpan()) ||
            tail.SequenceEqual("4".AsSpan()) ||
            tail.SequenceEqual("8".AsSpan()))
        {
            group = null;
            return false;
        }

        group = "border-color";
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool IsKnownTextSize(ReadOnlySpan<char> utility) =>
        utility.SequenceEqual("text-xs".AsSpan()) ||
        utility.SequenceEqual("text-sm".AsSpan()) ||
        utility.SequenceEqual("text-base".AsSpan()) ||
        utility.SequenceEqual("text-lg".AsSpan()) ||
        utility.SequenceEqual("text-xl".AsSpan()) ||
        utility.SequenceEqual("text-2xl".AsSpan()) ||
        utility.SequenceEqual("text-3xl".AsSpan()) ||
        utility.SequenceEqual("text-4xl".AsSpan()) ||
        utility.SequenceEqual("text-5xl".AsSpan()) ||
        utility.SequenceEqual("text-6xl".AsSpan()) ||
        utility.SequenceEqual("text-7xl".AsSpan()) ||
        utility.SequenceEqual("text-8xl".AsSpan()) ||
        utility.SequenceEqual("text-9xl".AsSpan());

    private readonly record struct Token(string? Value);
}