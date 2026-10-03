using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

/// <summary>Generates CSS rules from ComponentOptions without reflection — optimized.</summary>
public static class ComponentCssGenerator
{
    /// <summary>
    /// Generates CSS rules for a ComponentOptions object (e.g., AnchorOptions) with aggressive caching and minimal allocations.
    /// </summary>
    /// <param name="options">Options to configure for the Component CSS Generator.</param>
    /// <returns>The text produced by generate.</returns>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static string Generate(ComponentOptions options)
    {
        var builder = new PooledStringBuilder(stackalloc char[128]);
        try
        {
            Append(ref builder, options);
            return builder.ToString();
        }
        finally
        {
            builder.Dispose();
        }
    }

    internal static void Append(ref PooledStringBuilder builder, ComponentOptions options)
    {
        if (options is null) return;
        var rules = options.GetCssRules();
        if (rules is null) return;

        string? firstSelector = null;
        var firstDeclarations = new SmallBatch<string>();
        OrderedDictionary<string, SmallBatch<string>>? blocks = null;
        foreach (var rule in rules)
        {
            if (rule.Selector.IsNullOrWhiteSpace() || rule.Declaration.IsNullOrWhiteSpace())
                continue;
            firstSelector ??= rule.Selector;
            if (string.Equals(firstSelector, rule.Selector, StringComparison.Ordinal))
                firstDeclarations.Add(rule.Declaration);
            else
            {
                blocks ??= new OrderedDictionary<string, SmallBatch<string>>(4, StringComparer.Ordinal);
                if (blocks.TryGetValue(rule.Selector, out var declarations, out var index))
                {
                    declarations.Add(rule.Declaration);
                    blocks.SetAt(index, declarations);
                }
                else blocks.Add(rule.Selector, new SmallBatch<string>(rule.Declaration));
            }
        }
        if (firstSelector is null) return;
        if (builder.Length > 0) builder.Append('\n');
        AppendBlock(ref builder, firstSelector, firstDeclarations);
        if (blocks is not null)
        {
            foreach (var block in blocks)
            {
                builder.Append('\n');
                AppendBlock(ref builder, block.Key, block.Value);
            }
        }
    }

    private static void AppendBlock(ref PooledStringBuilder builder, string selector, SmallBatch<string> declarations)
    {
        builder.Append(selector);
        builder.Append(" {\n");
        for (var i = 0; i < declarations.Count; i++)
        {
            builder.Append("  ");
            builder.Append(declarations[i].AsSpan().TrimEnd("; "));
            builder.Append(";\n");
        }
        builder.Append("}");
    }
}
