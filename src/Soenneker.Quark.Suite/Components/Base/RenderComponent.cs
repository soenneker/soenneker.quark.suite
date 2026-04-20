using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Soenneker.Extensions.String;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// Minimal suite-level render base that owns render invalidation, render-key computation,
/// attribute caching, attribute merging, and helper utilities.
/// </summary>
public abstract class RenderComponent : CoreComponent
{
    private static readonly HashSet<string> _semanticColorTokens = new(StringComparer.Ordinal)
    {
        "primary",
        "primary-foreground",
        "secondary",
        "secondary-foreground",
        "destructive",
        "destructive-foreground",
        "muted",
        "muted-foreground",
        "accent",
        "accent-foreground",
        "popover",
        "popover-foreground",
        "card",
        "card-foreground",
        "background",
        "foreground",
        "border",
        "input",
        "ring",
        "white",
        "black",
        "transparent"
    };

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
        ApplyDefaultParameters();

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
        ApplyDefaultParameters();

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
            {
                var normalizedClass = NormalizeClassTokens(cls.ToString());

                if (normalizedClass.Length > 0)
                    attrs["class"] = normalizedClass;
            }

            if (sty.Length > 0)
                attrs["style"] = sty.ToString();

            BuildAttributesCore(attrs);

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
    /// Applies default parameter values before render-key computation and attribute emission.
    /// Components should set inherited builder-backed defaults here instead of hard-coding
    /// competing utility classes in their emitted class contracts.
    /// </summary>
    protected virtual void ApplyDefaultParameters()
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

        foreach (var kv in Attributes)
        {
            hc.Add(kv.Key, StringComparer.OrdinalIgnoreCase);
            hc.Add(kv.Value);
        }
    }

    private void MergeAdditionalAttributes(Dictionary<string, object> attrs, ref PooledStringBuilder sty, ref PooledStringBuilder cls)
    {
        if (Attributes is null)
            return;

        foreach (var kv in Attributes)
        {
            var k = kv.Key;
            var v = kv.Value;

            if (k.Equals("class", StringComparison.OrdinalIgnoreCase))
            {
                var s = v as string ?? v?.ToString();

                if (!string.IsNullOrEmpty(s))
                    AppendClass(ref cls, s);

                continue;
            }

            if (k.Equals("style", StringComparison.OrdinalIgnoreCase))
            {
                var s = v as string ?? v?.ToString();

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

        var classText = v.Value.ToString();

        if (classText.Length != 0)
        {
            AppendClass(ref clsB, classText);
            return;
        }

        if (v.Value.IsCssStyle)
        {
            var style = v.Value.StyleValue;

            if (style.Length != 0)
                AppendStyleDecl(ref styB, style);

            return;
        }

        var s = v.Value.ToString();

        if (s.Length != 0)
            AppendClass(ref clsB, s);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static void AddCss(ref PooledStringBuilder styB, ref PooledStringBuilder clsB, CssValue<WidthBuilder>? v, ReadOnlySpan<char> propertyName)
    {
        if (v is not { IsEmpty: false })
            return;

        var classText = v.Value.ToString();

        if (classText.Length != 0)
        {
            if (IsDimensionUtilityClass(classText.AsSpan()))
            {
                AppendClass(ref clsB, classText);
                return;
            }

            AppendStyleDecl(ref styB, propertyName, classText);
            return;
        }

        var styleValue = v.Value.StyleValue;

        if (styleValue.Length == 0)
            return;

        if (styleValue.AsSpan().IndexOf(':') >= 0)
            AppendStyleDecl(ref styB, styleValue);
        else
            AppendStyleDecl(ref styB, propertyName, styleValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static void AddCss(ref PooledStringBuilder styB, ref PooledStringBuilder clsB, CssValue<HeightBuilder>? v, ReadOnlySpan<char> propertyName)
    {
        if (v is not { IsEmpty: false })
            return;

        var classText = NormalizeHeightUtilityForProperty(v.Value.ToString(), propertyName);

        if (classText.Length != 0)
        {
            if (IsHeightUtilityClassForProperty(classText.AsSpan(), propertyName))
            {
                AppendClass(ref clsB, classText);
                return;
            }

            AppendStyleDecl(ref styB, propertyName, classText);
            return;
        }

        var styleValue = v.Value.StyleValue;

        if (styleValue.Length == 0)
            return;

        if (styleValue.AsSpan().IndexOf(':') >= 0)
            AppendStyleDecl(ref styB, styleValue);
        else
            AppendStyleDecl(ref styB, propertyName, styleValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static void AddCss(ref PooledStringBuilder styB, ref PooledStringBuilder clsB, CssValue<MaxWidthBuilder>? v, ReadOnlySpan<char> propertyName)
    {
        if (v is not { IsEmpty: false } vv)
            return;

        var classText = vv.ToString();

        if (classText.Length != 0)
        {
            if (IsDimensionUtilityClass(classText.AsSpan()))
            {
                AppendClass(ref clsB, classText);
                return;
            }

            AppendStyleDecl(ref styB, propertyName, classText);
            return;
        }

        var styleValue = vv.StyleValue;

        if (styleValue.Length == 0)
            return;

        if (styleValue.AsSpan().IndexOf(':') >= 0)
            AppendStyleDecl(ref styB, styleValue);
        else
            AppendStyleDecl(ref styB, propertyName, styleValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static void AddCss(ref PooledStringBuilder styB, ref PooledStringBuilder clsB, CssValue<MinWidthBuilder>? v, ReadOnlySpan<char> propertyName)
    {
        if (v is not { IsEmpty: false } vv)
            return;

        var classText = vv.ToString();

        if (classText.Length != 0)
        {
            if (IsDimensionUtilityClass(classText.AsSpan()))
            {
                AppendClass(ref clsB, classText);
                return;
            }

            AppendStyleDecl(ref styB, propertyName, classText);
            return;
        }

        var styleValue = vv.StyleValue;

        if (styleValue.Length == 0)
            return;

        if (styleValue.AsSpan().IndexOf(':') >= 0)
            AppendStyleDecl(ref styB, styleValue);
        else
            AppendStyleDecl(ref styB, propertyName, styleValue);
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

        return NormalizeClassTokens($"{existing} {toAdd}");
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static void EnsureClassAttr(Dictionary<string, object> attrs, string token)
    {
        attrs.TryGetValue("class", out var clsObj);
        var cls = EnsureClass(clsObj?.ToString(), token);

        if (cls.Length > 0)
            attrs["class"] = cls;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static void AppendToClassAttr(Dictionary<string, object> attrs, string token)
    {
        attrs.TryGetValue("class", out var clsObj);
        var cls = AppendToClass(clsObj?.ToString(), token);

        if (cls.Length > 0)
            attrs["class"] = cls;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static void AppendClassAttribute(Dictionary<string, object> attrs, params string?[] classes)
    {
        attrs.TryGetValue("class", out var existingObj);
        var existing = existingObj?.ToString();
        var appended = string.Join(' ', classes ?? Array.Empty<string?>());
        var cls = AppendToClass(existing, appended);

        if (cls.Length > 0)
            attrs["class"] = cls;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static void BuildClassAttribute(Dictionary<string, object> attrs, BuildClassAction builder)
    {
        var cls = new PooledStringBuilder(64);

        try
        {
            builder(ref cls);

            attrs.TryGetValue("class", out var existing);
            var combined = AppendToClass(cls.ToString(), existing?.ToString() ?? string.Empty);

            if (combined.Length > 0)
                attrs["class"] = combined;
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
            builder(ref sty);

            if (attrs.TryGetValue("style", out var existing))
            {
                var existingStr = existing.ToString();

                if (existingStr.HasContent())
                    AppendStyleDecl(ref sty, existingStr);
            }

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
        attrs.TryGetValue("class", out var existingClassObj);
        attrs.TryGetValue("style", out var existingStyleObj);

        var existingClassStr = existingClassObj as string ?? existingClassObj?.ToString();
        var existingStyleStr = existingStyleObj as string ?? existingStyleObj?.ToString();

        var existingClassLen = existingClassStr?.Length ?? 0;
        var existingStyleLen = existingStyleStr?.Length ?? 0;

        var cls = new PooledStringBuilder(Math.Max(32, existingClassLen + 32));
        var sty = new PooledStringBuilder(Math.Max(32, existingStyleLen + 32));

        try
        {
            builder(ref cls, ref sty);

            if (existingClassLen != 0)
                AppendClass(ref cls, existingClassStr!);

            if (existingStyleLen != 0)
                AppendStyleDecl(ref sty, existingStyleStr!);

            if (cls.Length > 0)
            {
                var normalizedClass = NormalizeClassTokens(cls.ToString());

                if (normalizedClass.Length > 0)
                    attrs["class"] = existingClassObj is string && normalizedClass.Length == existingClassLen
                        ? existingClassObj
                        : normalizedClass;
            }

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
    protected static void AppendStyleDeclAttr(Dictionary<string, object> attrs, string fullDecl)
    {
        if (string.IsNullOrWhiteSpace(fullDecl))
            return;

        attrs.TryGetValue("style", out var styleObj);

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

    private static string NormalizeClassTokens(string? value)
    {
        if (value.IsNullOrWhiteSpace())
            return string.Empty;

        var tokens = value.Split([' ', '\t', '\r', '\n'], StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        if (tokens.Length <= 1)
            return tokens.Length == 0 ? string.Empty : tokens[0];

        var seen = new HashSet<string>(StringComparer.Ordinal);
        var result = new PooledStringBuilder(value.Length);

        try
        {
            foreach (var token in tokens)
            {
                if (!seen.Add(token))
                    continue;

                if (result.Length > 0)
                    result.Append(' ');

                result.Append(token);
            }

            return result.ToString();
        }
        finally
        {
            result.Dispose();
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string NormalizeHeightUtilityForProperty(string classText, ReadOnlySpan<char> propertyName)
    {
        if (classText.Length == 0)
            return classText;

        if (propertyName.SequenceEqual("min-height".AsSpan()))
        {
            if (classText.StartsWith("h-", StringComparison.Ordinal))
                return "min-" + classText;
        }
        else if (propertyName.SequenceEqual("max-height".AsSpan()))
        {
            if (classText.StartsWith("h-", StringComparison.Ordinal))
                return "max-" + classText;
        }

        return classText;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool IsHeightUtilityClassForProperty(ReadOnlySpan<char> token, ReadOnlySpan<char> propertyName)
    {
        if (token.Length == 0)
            return false;

        if (propertyName.SequenceEqual("height".AsSpan()))
            return token.StartsWith("h-".AsSpan(), StringComparison.Ordinal)
                   || token.StartsWith("size-".AsSpan(), StringComparison.Ordinal)
                   || token.StartsWith("aspect-".AsSpan(), StringComparison.Ordinal);

        if (propertyName.SequenceEqual("min-height".AsSpan()))
            return token.StartsWith("min-h-".AsSpan(), StringComparison.Ordinal);

        if (propertyName.SequenceEqual("max-height".AsSpan()))
            return token.StartsWith("max-h-".AsSpan(), StringComparison.Ordinal);

        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool IsDimensionUtilityClass(ReadOnlySpan<char> token)
    {
        if (token.Length == 0)
            return false;

        return token.StartsWith("w-".AsSpan(), StringComparison.Ordinal)
               || token.StartsWith("min-w-".AsSpan(), StringComparison.Ordinal)
               || token.StartsWith("max-w-".AsSpan(), StringComparison.Ordinal)
               || token.StartsWith("h-".AsSpan(), StringComparison.Ordinal)
               || token.StartsWith("min-h-".AsSpan(), StringComparison.Ordinal)
               || token.StartsWith("max-h-".AsSpan(), StringComparison.Ordinal)
               || token.StartsWith("size-".AsSpan(), StringComparison.Ordinal)
               || token.StartsWith("aspect-".AsSpan(), StringComparison.Ordinal);
    }

    protected virtual void ApplyBorderColor(ref PooledStringBuilder sty, ref PooledStringBuilder cls, CssValue<BorderColorBuilder>? value)
    {
        AddColorCss(ref sty, ref cls, value, "border", "border-color");
    }

    protected virtual void ApplyTextColor(ref PooledStringBuilder sty, ref PooledStringBuilder cls, CssValue<TextColorBuilder>? value)
    {
        AddColorCss(ref sty, ref cls, value, "text", "color");
    }

    protected virtual void ApplyBackgroundColor(ref PooledStringBuilder sty, ref PooledStringBuilder cls, CssValue<BackgroundColorBuilder>? value)
    {
        AddColorCss(ref sty, ref cls, value, "bg", "background-color");
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static void AddCss(ref PooledStringBuilder styB, ref PooledStringBuilder clsB, CssValue<RingColorBuilder>? v)
    {
        AddColorCss(ref styB, ref clsB, v, "ring", "ring-color");
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static void AddCss(ref PooledStringBuilder styB, ref PooledStringBuilder clsB, CssValue<AccentColorBuilder>? v)
    {
        AddColorCss(ref styB, ref clsB, v, "accent", "accent-color");
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static void AddCss(ref PooledStringBuilder styB, ref PooledStringBuilder clsB, CssValue<CaretColorBuilder>? v)
    {
        AddColorCss(ref styB, ref clsB, v, "caret", "caret-color");
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void AddColorCss<TBuilder>(ref PooledStringBuilder styB, ref PooledStringBuilder clsB, CssValue<TBuilder>? v, ReadOnlySpan<char> classPrefix, ReadOnlySpan<char> cssProperty)
        where TBuilder : class, ICssBuilder
    {
        if (v is not { IsEmpty: false })
            return;

        var result = v.Value.ToString();

        if (result.Length == 0)
            return;

        if (v.Value.IsCssStyle)
        {
            AppendStyleDecl(ref styB, cssProperty, result);
            return;
        }

        if (v.Value.TryGetThemeToken(out var token) && token is not null)
        {
            AppendClassToken(ref clsB, classPrefix, token);
            return;
        }

        if (TryNormalizeColorUtility(result, classPrefix, out var utility))
        {
            AppendClass(ref clsB, utility);
            return;
        }

        AppendClass(ref clsB, result);
    }

    private static bool TryNormalizeColorUtility(string value, ReadOnlySpan<char> classPrefix, out string utility)
    {
        utility = string.Empty;

        if (value.IsNullOrWhiteSpace())
            return false;

        var trimmed = value.Trim();

        if (trimmed.Contains(' ') || trimmed.Contains(':'))
            return false;

        var expectedPrefix = $"{classPrefix}-";

        if (trimmed.StartsWith(expectedPrefix, StringComparison.Ordinal))
        {
            utility = trimmed;
            return true;
        }

        if (!IsColorToken(trimmed))
            return false;

        utility = expectedPrefix + trimmed;
        return true;
    }

    private static bool IsColorToken(string token)
    {
        if (token.Length == 0)
            return false;

        if (_semanticColorTokens.Contains(token))
            return true;

        var slashIndex = token.IndexOf('/');

        if (slashIndex > 0)
        {
            var baseToken = token[..slashIndex];
            var modifier = token[(slashIndex + 1)..];

            return IsColorToken(baseToken) && IsOpacityModifier(modifier);
        }

        return IsPaletteToken(token) || IsArbitraryToken(token);
    }

    private static bool IsPaletteToken(string token)
    {
        return Regex.IsMatch(token,
            "^(?:slate|gray|zinc|neutral|stone|red|orange|amber|yellow|lime|green|emerald|teal|cyan|sky|blue|indigo|violet|purple|fuchsia|pink|rose)-(?:50|100|200|300|400|500|600|700|800|900|950)$",
            RegexOptions.CultureInvariant);
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

        return Regex.IsMatch(modifier,
            "^(?:0|5|10|15|20|25|30|35|40|45|50|55|60|65|70|75|80|85|90|95|100)$",
            RegexOptions.CultureInvariant);
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
