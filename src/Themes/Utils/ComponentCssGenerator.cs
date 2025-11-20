using System;
using System.Buffers;
using System.Collections.Concurrent;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using Soenneker.Extensions.String;
using Soenneker.Quark.Dtos;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark.Themes.Utils;

/// <summary>Generates CSS rules from ComponentOptions using CssSelector and CssProperty attributes — optimized.</summary>
public static class ComponentCssGenerator
{
    // If type discovery is only at startup, you can switch to Dictionary<Type,...> + lock
    private static readonly ConcurrentDictionary<Type, CachedTypeInfo> _cache = new();

    /// <summary>Generates CSS rules for a ComponentOptions object (e.g., AnchorOptions) with aggressive caching and minimal allocations.</summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string Generate(ComponentOptions options)
    {
        if (options is null)
            return string.Empty;

        var type = options.GetType();
        var cached = _cache.GetOrAdd(type, BuildCacheForType);

        if (cached.Accessors.Length == 0 || cached.Selector.IsNullOrEmpty())
            return string.Empty;

        // Heuristic capacity: selector + braces + ~40 chars per property
     //   var estimated = 16 + cached.Selector.Length + 4 + cached.Accessors.Length * 40;
        using var sb = new PooledStringBuilder();

        var any = false;

        for (var i = 0; i < cached.Accessors.Length; i++)
        {
            var acc = cached.Accessors[i];

            var cssValueObj = acc.GetOptionValue(options);

            if (cssValueObj is null)
                continue;

            var styleObj = acc.GetStyleValue(cssValueObj);

            if (styleObj is null)
                continue;

            // Style can be string or some value type; only ToString() if needed
            var s = styleObj as string ?? styleObj.ToString();
            if (s.IsNullOrEmpty())
                continue;

            if (!TrySliceAfterColon(s!, out var valueSpan) || valueSpan.IsEmpty)
                continue;

            if (!any)
            {
                any = true;
                sb.Append(cached.Selector);
                sb.Append(" {\n");
            }

            sb.Append(acc.CssPrefix);
            sb.Append(valueSpan);
            sb.Append(";\n");
        }

        if (!any)
            return string.Empty;

        sb.Append('}');
        return sb.ToString();
    }

    private static CachedTypeInfo BuildCacheForType(Type type)
    {
        var cssSelectorAttr = type.GetCustomAttribute<CssSelectorAttribute>();
        var selector = cssSelectorAttr?.GetSelector();

        if (selector.IsNullOrEmpty())
            return new CachedTypeInfo { Selector = string.Empty, Accessors = [] };

        var props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);

        var rented = ArrayPool<Accessor>.Shared.Rent(props.Length);
        var count = 0;

        for (var i = 0; i < props.Length; i++)
        {
            var p = props[i];

            // Check for CssProperty attribute (with inheritance support)
            var cssPropAttr = p.GetCustomAttribute<CssPropertyAttribute>(inherit: true);
            if (cssPropAttr is null)
                continue;

            var propType = p.PropertyType;
            var underlying = Nullable.GetUnderlyingType(propType) ?? propType;

            if (!underlying.IsGenericType || underlying.GetGenericTypeDefinition() != typeof(CssValue<>))
                continue;

            // options -> CssValue<T> (boxed)
            var getOptionVal = CreateBoxedGetter(type, p);

            // CssValue<T> has .StyleValue
            var styleProp = underlying.GetProperty("StyleValue", BindingFlags.Public | BindingFlags.Instance);
            if (styleProp is null)
                continue;

            // CssValue<T> -> StyleValue (boxed)
            var getStyleVal = CreateBoxedGetter(underlying, styleProp);

            rented[count++] = new Accessor
            {
                GetOptionValue = getOptionVal,
                GetStyleValue = getStyleVal,
                CssPrefix = "  " + cssPropAttr.PropertyName + ": "
            };
        }

        Accessor[] accessors;
        if (count == 0)
        {
            ArrayPool<Accessor>.Shared.Return(rented, clearArray: true);
            accessors = [];
        }
        else
        {
            accessors = new Accessor[count];
            Array.Copy(rented, accessors, count);
            ArrayPool<Accessor>.Shared.Return(rented, clearArray: true);
        }

        return new CachedTypeInfo { Selector = selector!, Accessors = accessors };
    }

    /// <summary>
    /// Builds a fast boxed getter using DynamicMethod/IL. Much cheaper than Expression.Compile()
    /// and faster to invoke. Visibility is skipped so we can access non-publics if needed.
    /// </summary>
    private static Func<object, object?> CreateBoxedGetter(Type declaringType, PropertyInfo prop)
    {
        var get = prop.GetMethod ?? throw new InvalidOperationException($"Property {prop.Name} has no getter.");

        // The DynamicMethod must be associated with a module, not a Type
        var module = declaringType.Module;

        var dm = new DynamicMethod(name: "get_" + prop.Name + "_boxed", returnType: typeof(object), parameterTypes: [typeof(object)], m: module,
            skipVisibility: true);

        var il = dm.GetILGenerator();
        il.Emit(OpCodes.Ldarg_0); // load object
        il.Emit(OpCodes.Castclass, declaringType); // cast to declaring type
        il.EmitCall(get.IsVirtual ? OpCodes.Callvirt : OpCodes.Call, get, null); // call getter
        if (get.ReturnType.IsValueType)
            il.Emit(OpCodes.Box, get.ReturnType); // box if value type
        il.Emit(OpCodes.Ret);

        return (Func<object, object?>)dm.CreateDelegate(typeof(Func<object, object?>));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool TrySliceAfterColon(string s, out ReadOnlySpan<char> value)
    {
        var span = s.AsSpan();
        var idx = span.IndexOf(':');

        if (idx <= 0 || (uint)(idx + 1) >= (uint)span.Length)
        {
            value = default;
            return false;
        }

        value = span[(idx + 1)..]
            .TrimStart(); // .NET 8/9 span trim, zero-alloc
        return !value.IsEmpty;
    }
}