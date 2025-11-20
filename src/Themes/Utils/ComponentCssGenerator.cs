using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

/// <summary>Generates CSS rules from ComponentOptions without reflection — optimized.</summary>
public static class ComponentCssGenerator
{
    /// <summary>Generates CSS rules for a ComponentOptions object (e.g., AnchorOptions) with aggressive caching and minimal allocations.</summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string Generate(ComponentOptions options)
    {
        if (options is null)
            return string.Empty;

        var rules = options.GetCssRules();

        if (rules is null)
            return string.Empty;

        Dictionary<string, List<string>>? blocks = null;
        List<string>? order = null;

        foreach (var rule in rules)
        {
            if (rule.Selector.IsNullOrWhiteSpace() || rule.Declaration.IsNullOrWhiteSpace())
                continue;

            blocks ??= new Dictionary<string, List<string>>(8, StringComparer.Ordinal);
            order ??= new List<string>(8);

            if (!blocks.TryGetValue(rule.Selector, out var declarations))
            {
                declarations = [];
                blocks[rule.Selector] = declarations;
                order.Add(rule.Selector);
            }

            declarations.Add(rule.Declaration);
        }

        if (blocks is null || blocks.Count == 0 || order is null || order.Count == 0)
            return string.Empty;

        using var sb = new PooledStringBuilder();

        foreach (var selector in order)
        {
            if (!blocks.TryGetValue(selector, out var declarations) || declarations.Count == 0)
                continue;

            sb.Append(selector);
            sb.Append(" {\n");

            foreach (var declaration in declarations)
            {
                if (declaration.IsNullOrWhiteSpace())
                    continue;

                sb.Append("  ");
                sb.Append(declaration.TrimEnd(';', ' '));
                sb.Append(";\n");
            }

            sb.Append("}\n");
        }

        return sb.ToString()
                 .TrimEnd();
    }
}