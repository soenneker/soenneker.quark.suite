using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

/// <summary>Generates Bootstrap CSS custom properties with low allocations.</summary>
public static class BootstrapCssGenerator
{
    private static readonly ConcurrentDictionary<Type, Accessor[]> _accessorCache = new();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Dictionary<string, string> GenerateCssVariables(object? cssVariables)
    {
        if (cssVariables is null)
            return new Dictionary<string, string>(0);

        var result = new Dictionary<string, string>(16);
        AddCssVariables(cssVariables, result);
        return result;
    }

    /// <summary>Later objects overwrite earlier keys.</summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Dictionary<string, string> GenerateCssVariables(params object?[] cssVariablesObjects)
    {
        if (cssVariablesObjects is null || cssVariablesObjects.Length == 0)
            return new Dictionary<string, string>(0);

        var result = new Dictionary<string, string>(64);
        for (var i = 0; i < cssVariablesObjects.Length; i++)
        {
            var obj = cssVariablesObjects[i];
            if (obj is null) continue;
            AddCssVariables(obj, result);
        }
        return result;
    }

    /// <summary>Returns a single <c>:root { � }</c> block.</summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string GenerateRootCss(params object?[] cssVariablesObjects)
    {
        var map = GenerateCssVariables(cssVariablesObjects);

        if (map.Count == 0)
            return string.Empty;

        // quick capacity guess to reduce growths
        var cap = 16 + map.Count * 32;
        using var sb = new PooledStringBuilder(cap);

        sb.Append(":root {\n".AsSpan());

        foreach (var kvp in map)
        {
            sb.Append("  ".AsSpan());
            sb.Append(kvp.Key);                    // "--bs-�"
            sb.Append(": ".AsSpan());
            sb.Append(kvp.Value);                  // value
            sb.Append(";\n".AsSpan());
        }
        sb.Append("}\n\n".AsSpan());

        // Generate component-specific overrides to match Bootstrap's component classes
        // Group variables by component type
        var componentGroups = map.GroupBy(kvp => GetComponentFromVariable(kvp.Key));
        
        foreach (var group in componentGroups)
        {
            var component = group.Key;
            if (string.IsNullOrEmpty(component)) continue;

            // Target the same component class that Bootstrap uses
            sb.Append($".{component} {{\n".AsSpan());
            foreach (var kvp in group)
            {
                sb.Append("  ".AsSpan());
                sb.Append(kvp.Key);                    // "--bs-"
                sb.Append(": ".AsSpan());
                sb.Append(kvp.Value);                  // value
                sb.Append(";\n".AsSpan());
            }
            sb.Append("}\n\n".AsSpan());
        }

        return sb.ToStringAndDispose();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string GetComponentFromVariable(string variableName)
    {
        // Extract component name from CSS variable
        // e.g., "--bs-card-border-color" -> "card"
        if (variableName.StartsWith("--bs-"))
        {
            var withoutPrefix = variableName.Substring(5); // Remove "--bs-"
            var firstDash = withoutPrefix.IndexOf('-');
            if (firstDash > 0)
            {
                return withoutPrefix.Substring(0, firstDash);
            }
        }
        return string.Empty;
    }

    // -------- internals --------

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void AddCssVariables(object source, Dictionary<string, string> target)
    {
        var accessors = GetOrBuildAccessors(source.GetType());
        for (var i = 0; i < accessors.Length; i++)
        {
            ref readonly var acc = ref accessors[i];
            var val = acc.Getter(source);
            if (!string.IsNullOrEmpty(val))
                target[acc.CssName] = val!;
        }
    }

    private static Accessor[] GetOrBuildAccessors(Type type)
    {
        if (_accessorCache.TryGetValue(type, out var cached))
            return cached;

        var props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var list = new List<Accessor>(props.Length);

        for (var i = 0; i < props.Length; i++)
        {
            var p = props[i];

            // only string props; reduces boxing/conversion and keeps the fast path simple
            if (p.PropertyType != typeof(string))
                continue;

            var attr = p.GetCustomAttribute<CssVariableAttribute>(inherit: false);
            if (attr is null)
                continue;

            var cssName = attr.GetName();

            if (cssName.IsNullOrEmpty())
                continue;

            var getter = CompileStringGetter(type, p);
            list.Add(new Accessor(cssName, getter));
        }

        var result = list.Count == 0 ? [] : list.ToArray();
        _accessorCache[type] = result;
        return result;
    }

    private static Func<object, string?> CompileStringGetter(Type declaringType, PropertyInfo prop)
    {
        var obj = Expression.Parameter(typeof(object), "o");
        var cast = Expression.Convert(obj, declaringType);
        var access = Expression.Property(cast, prop);
        var body = Expression.Convert(access, typeof(string));
        return Expression.Lambda<Func<object, string?>>(body, obj).Compile();
    }
}
