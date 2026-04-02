using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Soenneker.Quark;

internal static partial class ColorUtility
{
    private static readonly HashSet<string> KeywordTokens = new(System.StringComparer.Ordinal)
    {
        "inherit",
        "current",
        "transparent",
        "black",
        "white"
    };

    public static string GetClass(string prefix, ColorRule rule, HashSet<string> semanticTokens)
    {
        if (rule.IsUtility)
            return rule.Value.StartsWith(prefix, System.StringComparison.Ordinal) ? rule.Value : string.Empty;

        return IsTokenAllowed(rule.Value, semanticTokens) ? $"{prefix}{rule.Value}" : string.Empty;
    }

    private static bool IsTokenAllowed(string token, HashSet<string> semanticTokens)
    {
        if (string.IsNullOrWhiteSpace(token))
            return false;

        if (semanticTokens.Contains(token))
            return true;

        var slashIndex = token.IndexOf('/');

        if (slashIndex > 0)
        {
            var baseToken = token[..slashIndex];
            var modifier = token[(slashIndex + 1)..];

            if ((semanticTokens.Contains(baseToken) || IsPaletteToken(baseToken) || KeywordTokens.Contains(baseToken)) && IsOpacityModifier(modifier))
                return true;
        }

        return IsPaletteToken(token) || KeywordTokens.Contains(token) || IsArbitraryToken(token);
    }

    private static bool IsPaletteToken(string token)
    {
        return PaletteTokenRegex().IsMatch(token);
    }

    private static bool IsArbitraryToken(string token)
    {
        return token.Length >= 2
               && ((token[0] == '[' && token[^1] == ']')
                   || (token[0] == '(' && token[^1] == ')'));
    }

    private static bool IsOpacityModifier(string modifier)
    {
        if (modifier.Length == 0)
            return false;

        if (modifier.Length >= 2 && modifier[0] == '[' && modifier[^1] == ']')
            return true;

        return OpacityModifierRegex().IsMatch(modifier);
    }

    [GeneratedRegex(@"^(?:slate|gray|zinc|neutral|stone|red|orange|amber|yellow|lime|green|emerald|teal|cyan|sky|blue|indigo|violet|purple|fuchsia|pink|rose)-(?:50|100|200|300|400|500|600|700|800|900|950)$", RegexOptions.CultureInvariant)]
    private static partial Regex PaletteTokenRegex();

    [GeneratedRegex(@"^(?:0|5|10|15|20|25|30|35|40|45|50|55|60|65|70|75|80|85|90|95|100)$", RegexOptions.CultureInvariant)]
    private static partial Regex OpacityModifierRegex();
}

internal readonly record struct ColorRule(string Value, BreakpointType? Breakpoint, bool IsUtility = false);
