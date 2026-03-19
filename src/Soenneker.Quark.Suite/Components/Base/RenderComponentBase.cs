using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Soenneker.Extensions.String;
using Soenneker.Quark.Utilities;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// Minimal suite-level render base that owns render invalidation, render-key computation,
/// attribute caching, attribute merging, and helper utilities.
/// </summary>
public abstract class RenderComponentBase : CoreComponent
{
    private bool _shouldRender = true;
    private int _lastRenderKey;
    private Dictionary<string, object>? _cachedAttrs;
    private int _cachedAttrsKey;
    private int _renderVersion;

    /// <summary>
    /// Allows higher-level bases to opt into always-render behavior without coupling the core render pipeline
    /// to suite-specific services or options.
    /// </summary>
    protected virtual bool AlwaysRender => true;

    public void Refresh()
    {
        InvalidateRender();
        StateHasChanged();
    }

    public Task RefreshOffThread()
    {
        InvalidateRender();
        return InvokeAsync(StateHasChanged);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected void InvalidateRender()
    {
        unchecked
        {
            _renderVersion++;
        }

        _lastRenderKey = ComputeRenderKey();
        _cachedAttrs = null;
        _cachedAttrsKey = 0;
        _shouldRender = true;
    }

    protected override bool ShouldRender()
    {
        if (AlwaysRender)
            return true;

        return _shouldRender;
    }

    protected override void OnParametersSet()
    {
        if (AlwaysRender)
        {
            _shouldRender = true;
            _lastRenderKey = ComputeRenderKey();
            return;
        }

        var key = ComputeRenderKey();
        _shouldRender = key != _lastRenderKey;
        _lastRenderKey = key;
    }

    protected virtual IReadOnlyDictionary<string, object> BuildAttributes()
    {
        var currentKey = _lastRenderKey;

        if (!AlwaysRender && _cachedAttrs is not null && _cachedAttrsKey == currentKey)
            return _cachedAttrs;

        var attrs = new Dictionary<string, object>(8 + (Attributes?.Count ?? 0));
        var cls = new PooledStringBuilder(64);
        var sty = new PooledStringBuilder(128);

        try
        {
            BuildOwnedAttributes(attrs);
            BuildOwnedClassAndStyle(ref sty, ref cls);
            MergeAdditionalAttributes(attrs, ref sty, ref cls);

            if (cls.Length > 0)
                attrs["class"] = cls.ToString();

            if (sty.Length > 0)
                attrs["style"] = sty.ToString();

            BuildAttributesCore(attrs);

            if (attrs.TryGetValue("class", out var classObj) && classObj is string classStr && classStr.Length > 0)
                attrs["class"] = ClassNames.Combine(classStr);

            _cachedAttrs = attrs;
            _cachedAttrsKey = currentKey;

            return attrs;
        }
        finally
        {
            sty.Dispose();
            cls.Dispose();
        }
    }

    protected virtual void BuildOwnedAttributes(Dictionary<string, object> attrs)
    {
        if (Id.HasContent())
            attrs["id"] = Id!;
    }

    protected virtual void BuildOwnedClassAndStyle(ref PooledStringBuilder sty, ref PooledStringBuilder cls)
    {
    }

    /// <summary>
    /// Final attribute hook for the concrete component. Use this for component-local attributes,
    /// class tweaks, or style tweaks after shared ownership has been applied.
    /// </summary>
    protected virtual void BuildAttributesCore(Dictionary<string, object> attrs)
    {
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static void AddIf<T>(ref HashCode hc, CssValue<T>? v) where T : class, ICssBuilder
    {
        if (v is { IsEmpty: false })
            hc.Add(v.Value);
    }

    private int ComputeRenderKey()
    {
        var hc = new HashCode();

        hc.Add(_renderVersion);
        hc.Add(Id);
        AddAdditionalAttributesToRenderKey(ref hc);
        ComputeRenderKeyCore(ref hc);

        return hc.ToHashCode();
    }

    protected virtual void ComputeRenderKeyCore(ref HashCode hc)
    {
    }

    private void AddAdditionalAttributesToRenderKey(ref HashCode hc)
    {
        if (Attributes is null || Attributes.Count == 0)
            return;

        foreach (KeyValuePair<string, object> kv in Attributes)
        {
            hc.Add(kv.Key, StringComparer.OrdinalIgnoreCase);
            hc.Add(kv.Value);
        }
    }

    private void MergeAdditionalAttributes(Dictionary<string, object> attrs, ref PooledStringBuilder sty, ref PooledStringBuilder cls)
    {
        if (Attributes is null)
            return;

        foreach (KeyValuePair<string, object> kv in Attributes)
        {
            string k = kv.Key;
            object v = kv.Value;

            if (k.Equals("class", StringComparison.OrdinalIgnoreCase))
            {
                string? s = v as string ?? v?.ToString();

                if (!string.IsNullOrEmpty(s))
                    AppendClass(ref cls, s);

                continue;
            }

            if (k.Equals("style", StringComparison.OrdinalIgnoreCase))
            {
                string? s = v as string ?? v?.ToString();

                if (!string.IsNullOrEmpty(s))
                    AppendStyleDecl(ref sty, s);

                continue;
            }

            attrs[k] = v;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static void AppendClass(ref PooledStringBuilder b, string s)
    {
        if (s.IsNullOrEmpty())
            return;

        if (b.Length != 0)
            b.Append(' ');

        b.Append(s);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static void AppendStyleDecl(ref PooledStringBuilder b, string nameColonSpace, string value)
    {
        if (b.Length != 0)
        {
            b.Append(';');
            b.Append(' ');
        }

        b.Append(nameColonSpace);
        b.Append(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static void AppendStyleDecl(ref PooledStringBuilder b, string fullDecl)
    {
        if (fullDecl.IsNullOrEmpty())
            return;

        if (b.Length != 0)
        {
            b.Append(';');
            b.Append(' ');
        }

        b.Append(fullDecl);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static void AddCss<T>(ref PooledStringBuilder styB, ref PooledStringBuilder clsB, CssValue<T>? v) where T : class, ICssBuilder
    {
        if (v is not { IsEmpty: false })
            return;

        string s = v.Value.ToString();

        if (s.Length == 0)
            return;

        if (v.Value.IsCssStyle)
            AppendStyleDecl(ref styB, s);
        else
            AppendClass(ref clsB, s);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static void AddCss(ref PooledStringBuilder styB, ref PooledStringBuilder clsB, CssValue<WidthBuilder>? v, ReadOnlySpan<char> propertyName)
    {
        if (v is not { IsEmpty: false })
            return;

        string s = v.Value.ToString();

        if (s.Length == 0)
            return;

        if (!v.Value.IsCssStyle)
        {
            AppendClass(ref clsB, s);
            return;
        }

        if (s.AsSpan().IndexOf(':') >= 0)
            AppendStyleDecl(ref styB, s);
        else
            AppendStyleDecl(ref styB, propertyName, s);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static void AddCss(ref PooledStringBuilder styB, ref PooledStringBuilder clsB, CssValue<HeightBuilder>? v, ReadOnlySpan<char> propertyName)
    {
        if (v is not { IsEmpty: false })
            return;

        string s = v.Value.ToString();

        if (s.Length == 0)
            return;

        if (!v.Value.IsCssStyle)
        {
            AppendClass(ref clsB, s);
            return;
        }

        if (s.AsSpan().IndexOf(':') >= 0)
            AppendStyleDecl(ref styB, s);
        else
            AppendStyleDecl(ref styB, propertyName, s);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static void AddCss(ref PooledStringBuilder styB, ref PooledStringBuilder clsB, CssValue<MaxWidthBuilder>? v, ReadOnlySpan<char> propertyName)
    {
        if (v is not { IsEmpty: false } vv)
            return;

        string s = vv.ToString();

        if (s.Length == 0)
            return;

        if (!vv.IsCssStyle)
        {
            AppendClass(ref clsB, s);
            return;
        }

        if (s.AsSpan().IndexOf(':') >= 0)
            AppendStyleDecl(ref styB, s);
        else
            AppendStyleDecl(ref styB, propertyName, s);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static void AddCss(ref PooledStringBuilder styB, ref PooledStringBuilder clsB, CssValue<MinWidthBuilder>? v, ReadOnlySpan<char> propertyName)
    {
        if (v is not { IsEmpty: false } vv)
            return;

        string s = vv.ToString();

        if (s.Length == 0)
            return;

        if (!vv.IsCssStyle)
        {
            AppendClass(ref clsB, s);
            return;
        }

        if (s.AsSpan().IndexOf(':') >= 0)
            AppendStyleDecl(ref styB, s);
        else
            AppendStyleDecl(ref styB, propertyName, s);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static string EnsureClass(string? existing, string? toAdd)
    {
        if (toAdd.IsNullOrEmpty())
            return existing ?? string.Empty;

        if (existing.IsNullOrEmpty())
            return toAdd;

        return existing.Contains(toAdd!, StringComparison.Ordinal) ? existing : string.Concat(existing, " ", toAdd);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static string AppendToClass(string? existing, string toAdd)
    {
        if (toAdd.IsNullOrEmpty())
            return existing ?? string.Empty;

        if (existing.IsNullOrEmpty())
            return toAdd;

        return $"{existing} {toAdd}";
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static void EnsureClassAttr(Dictionary<string, object> attrs, string token)
    {
        attrs.TryGetValue("class", out object? clsObj);
        string cls = EnsureClass(clsObj?.ToString(), token);

        if (cls.Length > 0)
            attrs["class"] = cls;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static void AppendToClassAttr(Dictionary<string, object> attrs, string token)
    {
        attrs.TryGetValue("class", out object? clsObj);
        string cls = AppendToClass(clsObj?.ToString(), token);

        if (cls.Length > 0)
            attrs["class"] = cls;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static void AppendClassAttribute(Dictionary<string, object> attrs, params string?[] classes)
    {
        var cls = new PooledStringBuilder(64);

        try
        {
            if (attrs.TryGetValue("class", out object? existing))
            {
                string? existingStr = existing.ToString();

                if (!existingStr.IsNullOrWhiteSpace())
                    cls.Append(existingStr);
            }

            foreach (string? c in classes)
            {
                if (!c.IsNullOrWhiteSpace())
                    AppendClass(ref cls, c!);
            }

            if (cls.Length > 0)
                attrs["class"] = cls.ToString();
        }
        finally
        {
            cls.Dispose();
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static void BuildClassAttribute(Dictionary<string, object> attrs, BuildClassAction builder)
    {
        var cls = new PooledStringBuilder(64);

        try
        {
            if (attrs.TryGetValue("class", out object? existing))
            {
                string? existingStr = existing.ToString();

                if (existingStr.HasContent())
                    cls.Append(existingStr);
            }

            builder(ref cls);

            if (cls.Length > 0)
                attrs["class"] = cls.ToString();
        }
        finally
        {
            cls.Dispose();
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static void BuildStyleAttribute(Dictionary<string, object> attrs, BuildStyleAction builder)
    {
        var sty = new PooledStringBuilder(64);

        try
        {
            if (attrs.TryGetValue("style", out object? existing))
            {
                string? existingStr = existing.ToString();

                if (existingStr.HasContent())
                    sty.Append(existingStr);
            }

            builder(ref sty);

            if (sty.Length > 0)
                attrs["style"] = sty.ToString();
        }
        finally
        {
            sty.Dispose();
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static void BuildClassAndStyleAttributes(Dictionary<string, object> attrs, BuildClassAndStyleAction builder)
    {
        attrs.TryGetValue("class", out object? existingClassObj);
        attrs.TryGetValue("style", out object? existingStyleObj);

        string? existingClassStr = existingClassObj as string ?? existingClassObj?.ToString();
        string? existingStyleStr = existingStyleObj as string ?? existingStyleObj?.ToString();

        int existingClassLen = existingClassStr?.Length ?? 0;
        int existingStyleLen = existingStyleStr?.Length ?? 0;

        var cls = new PooledStringBuilder(Math.Max(32, existingClassLen + 32));
        var sty = new PooledStringBuilder(Math.Max(32, existingStyleLen + 32));

        try
        {
            if (existingClassLen != 0)
                cls.Append(existingClassStr!);

            if (existingStyleLen != 0)
                sty.Append(existingStyleStr!);

            builder(ref cls, ref sty);

            if (cls.Length > 0)
                attrs["class"] = existingClassObj is string && cls.Length == existingClassLen ? existingClassObj : cls.ToString();

            if (sty.Length > 0)
                attrs["style"] = existingStyleObj is string && sty.Length == existingStyleLen ? existingStyleObj : sty.ToString();
        }
        finally
        {
            cls.Dispose();
            sty.Dispose();
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static void SetOrRemove(Dictionary<string, object> attrs, string name, bool condition, object trueValue)
    {
        if (condition)
            attrs[name] = trueValue;
        else
            attrs.Remove(name);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static void AppendStyleDeclAttr(Dictionary<string, object> attrs, string fullDecl)
    {
        if (string.IsNullOrWhiteSpace(fullDecl))
            return;

        attrs.TryGetValue("style", out object? styleObj);

        if (styleObj is string existing && existing.Length != 0)
        {
            using var b = new PooledStringBuilder(existing.Length + 2 + fullDecl.Length);

            b.Append(existing);

            if (existing[^1] != ';')
                b.Append(';');

            b.Append(' ');
            b.Append(fullDecl);
            attrs["style"] = b.ToString();
            return;
        }

        attrs["style"] = fullDecl;
    }

    protected virtual void ApplyBorderColor(ref PooledStringBuilder sty, ref PooledStringBuilder cls, CssValue<BorderColorBuilder>? value)
    {
        AddCss(ref sty, ref cls, value);
    }

    protected virtual void ApplyTextColor(ref PooledStringBuilder sty, ref PooledStringBuilder cls, CssValue<TextColorBuilder>? value)
    {
        AddCss(ref sty, ref cls, value);
    }

    protected virtual void ApplyBackgroundColor(ref PooledStringBuilder sty, ref PooledStringBuilder cls, CssValue<BackgroundColorBuilder>? value)
    {
        AddColorCss(ref sty, ref cls, value, "bg", "background-color");
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void AddColorCss(ref PooledStringBuilder styB, ref PooledStringBuilder clsB, CssValue<BackgroundColorBuilder>? v, ReadOnlySpan<char> classPrefix, ReadOnlySpan<char> cssProperty)
    {
        if (v is not { IsEmpty: false })
            return;

        string result = v.Value.ToString();

        if (result.Length == 0)
            return;

        if (v.Value.IsCssStyle)
        {
            AppendStyleDecl(ref styB, cssProperty, result);
            return;
        }

        if (v.Value.TryGetThemeToken(out string? token) && token is not null)
        {
            AppendClassToken(ref clsB, classPrefix, token);
            return;
        }

        AppendClass(ref clsB, result);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void AppendClassToken(ref PooledStringBuilder b, ReadOnlySpan<char> prefix, string token)
    {
        if (token.Length == 0)
            return;

        if (b.Length != 0)
            b.Append(' ');

        b.Append(prefix);
        b.Append('-');
        b.Append(token);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static void AppendStyleDecl(ref PooledStringBuilder b, ReadOnlySpan<char> name, string value)
    {
        if (value.Length == 0)
            return;

        if (b.Length != 0)
        {
            b.Append(';');
            b.Append(' ');
        }

        b.Append(name);
        b.Append(": ");
        b.Append(value);
    }

}
