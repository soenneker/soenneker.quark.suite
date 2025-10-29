using Soenneker.Quark;
using System;
using System.Collections.Generic;
using Soenneker.Extensions.String;

public readonly struct CssValue<TBuilder> : IEquatable<CssValue<TBuilder>> where TBuilder : class, ICssBuilder
{
    private readonly string? _styleValue;
    private readonly string _value;

    // Cache generic-type checks per closed generic
    private static readonly bool _isHeight = typeof(TBuilder) == typeof(HeightBuilder);
    private static readonly bool _isWidth = typeof(TBuilder) == typeof(WidthBuilder);
    private static readonly bool _isColor = typeof(TBuilder) == typeof(ColorBuilder);
    private static readonly bool _isSize = typeof(TBuilder) == typeof(SizeBuilder);

    private static readonly HashSet<string> _bootstrapThemeTokens = new(StringComparer.OrdinalIgnoreCase)
        { "primary", "secondary", "success", "danger", "warning", "info", "light", "dark", "link", "muted" };

    private static readonly HashSet<string> _bootstrapSizeTokens = new(StringComparer.OrdinalIgnoreCase) { "xs", "sm", "md", "lg", "xl", "xxl" };

    private CssValue(string value, string? styleValue = null)
    {
        _value = value ?? string.Empty;
        _styleValue = styleValue;
    }

    public static implicit operator CssValue<TBuilder>(TBuilder builder) => new(builder.ToClass(), builder.ToStyle());

    public static implicit operator CssValue<TBuilder>(string value) => new(value);

    public static implicit operator CssValue<TBuilder>(int value) =>
        _isHeight ? new CssValue<TBuilder>($"height: {value}px") : _isWidth ? new CssValue<TBuilder>($"width: {value}px") : new CssValue<TBuilder>(value.ToString());

    public static implicit operator string(CssValue<TBuilder> v) => v._value;

    public override string ToString() => _value;

    public bool IsEmpty => _value.IsNullOrEmpty();

    public bool IsCssStyle =>
        // style if it looks like "prop: val" OR (ColorBuilder with non-theme token)
        _value.IndexOf(':') >= 0 || (_isColor && !IsKnownThemeOrSizeToken(_value));

    public bool IsCssClass => !IsCssStyle && !IsEmpty;

    /// <summary>Gets the style representation (e.g., "text-decoration: underline") if available</summary>
    public string StyleValue
    {
        get
        {
            // Check if _styleValue looks like a style (contains colon)
            if (!string.IsNullOrEmpty(_styleValue) && _styleValue.IndexOf(':') >= 0)
                return _styleValue;
            
            // Check if _value looks like a style (contains colon)
            if (!string.IsNullOrEmpty(_value) && _value.IndexOf(':') >= 0)
                return _value;
            
            // Neither looks like a style, return empty
            return string.Empty;
        }
    }

    private static bool IsKnownThemeOrSizeToken(string value)
    {
        if (value.IsNullOrEmpty()) 
            return false;

        // For SizeBuilder, we accept size tokens; otherwise theme tokens (colors)
        if (_isSize) return _bootstrapSizeTokens.Contains(value);
        return _bootstrapThemeTokens.Contains(value);
    }

    /// <summary>Does this non-empty value change generated markup (class or style)?</summary>
    public bool AffectsMarkup => !IsEmpty; // keep simple: empty is no-op, anything else impacts attrs

    public bool Equals(CssValue<TBuilder> other) => _value == other._value;

    public override bool Equals(object? obj) => obj is CssValue<TBuilder> o && Equals(o);

    public override int GetHashCode() => StringComparer.Ordinal.GetHashCode(_value);

    public static bool operator ==(CssValue<TBuilder> a, CssValue<TBuilder> b) => a.Equals(b);
    public static bool operator !=(CssValue<TBuilder> a, CssValue<TBuilder> b) => !a.Equals(b);

    public bool TryGetBootstrapThemeToken(out string? token)
    {
        if (_value.HasContent())
        {
            var v = _value.Trim();
            if (_isSize)
            {
                if (_bootstrapSizeTokens.Contains(v))
                {
                    token = v;
                    return true;
                }
            }
            else
            {
                if (_bootstrapThemeTokens.Contains(v))
                {
                    token = v;
                    return true;
                }
            }
        }

        token = null;
        return false;
    }
}