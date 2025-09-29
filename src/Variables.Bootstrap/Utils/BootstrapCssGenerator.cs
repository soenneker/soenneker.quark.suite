using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;
using Soenneker.Extensions.String;

namespace Soenneker.Quark.Variables.Bootstrap.Utils;

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
        for (int i = 0; i < cssVariablesObjects.Length; i++)
        {
            object? obj = cssVariablesObjects[i];
            if (obj is null) continue;
            AddCssVariables(obj, result);
        }
        return result;
    }

    /// <summary>Returns a single <c>:root { � }</c> block.</summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string GenerateRootCss(params object?[] cssVariablesObjects)
    {
        Dictionary<string, string> map = GenerateCssVariables(cssVariablesObjects);
        if (map.Count == 0)
            return string.Empty;

        // quick capacity guess to reduce growths
        int cap = 16 + map.Count * 32;
        using var sb = new PooledStringBuilder(cap);

        sb.Append(":root {\n".AsSpan());

        foreach (KeyValuePair<string, string> kvp in map)
        {
            sb.Append("  ".AsSpan());
            sb.Append(kvp.Key);                    // "--bs-�"
            sb.Append(": ".AsSpan());
            sb.Append(kvp.Value);                  // value
            sb.Append(";\n".AsSpan());
        }
        sb.Append('}');

        return sb.ToStringAndDispose();
    }

    // -------- internals --------

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void AddCssVariables(object source, Dictionary<string, string> target)
    {
        Accessor[] accessors = GetOrBuildAccessors(source.GetType());
        for (int i = 0; i < accessors.Length; i++)
        {
            ref readonly Accessor acc = ref accessors[i];
            string? val = acc.Getter(source);
            if (!string.IsNullOrEmpty(val))
                target[acc.CssName] = val!;
        }
    }

    private static Accessor[] GetOrBuildAccessors(Type type)
    {
        if (_accessorCache.TryGetValue(type, out Accessor[]? cached))
            return cached;

        PropertyInfo[] props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var list = new List<Accessor>(props.Length);

        for (int i = 0; i < props.Length; i++)
        {
            PropertyInfo p = props[i];

            // only string props; reduces boxing/conversion and keeps the fast path simple
            if (p.PropertyType != typeof(string))
                continue;

            var attr = p.GetCustomAttribute<CssVariableAttribute>(inherit: false);
            if (attr is null)
                continue;

            string cssName = attr.GetName();

            if (cssName.IsNullOrEmpty())
                continue;

            Func<object, string?> getter = CompileStringGetter(type, p);
            list.Add(new Accessor(cssName, getter));
        }

        Accessor[] result = list.Count == 0 ? [] : list.ToArray();
        _accessorCache[type] = result;
        return result;
    }

    private static Func<object, string?> CompileStringGetter(Type declaringType, PropertyInfo prop)
    {
        ParameterExpression obj = Expression.Parameter(typeof(object), "o");
        UnaryExpression cast = Expression.Convert(obj, declaringType);
        MemberExpression access = Expression.Property(cast, prop);
        UnaryExpression body = Expression.Convert(access, typeof(string));
        return Expression.Lambda<Func<object, string?>>(body, obj).Compile();
    }
}
