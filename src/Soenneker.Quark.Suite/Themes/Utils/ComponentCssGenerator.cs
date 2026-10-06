using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>Generates CSS rules from ComponentOptions without reflection — optimized.</summary>
public static class ComponentCssGenerator
{
    /// <summary>
    /// Generates CSS rules for a ComponentOptions object (e.g., AnchorOptions) with declarations grouped by selector.
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
        var rules = new ComponentCssRuleCollector();
        try
        {
            options.CollectCssRules(ref rules);
            rules.AppendTo(ref builder);
        }
        finally
        {
            rules.Dispose();
        }
    }
}
