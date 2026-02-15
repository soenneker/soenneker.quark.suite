using System;
using System.Globalization;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

/// <summary>
/// Represents a CSS value that can be either a CSS class or inline style, generated from a builder.
/// </summary>
/// <typeparam name="TBuilder">The type of CSS builder used to generate the value.</typeparam>
public readonly struct CssValue<TBuilder> : IEquatable<CssValue<TBuilder>> where TBuilder : class, ICssBuilder
{
    private readonly string _value;
    private readonly string? _styleValue;
    private readonly string? _cssSelector;
    private readonly bool _selectorIsAbsolute;

    // Cache generic-type checks per closed generic
    private static readonly bool _isHeight = typeof(TBuilder) == typeof(HeightBuilder);
    private static readonly bool _isWidth = typeof(TBuilder) == typeof(WidthBuilder);
    private static readonly bool _isColor = typeof(TBuilder) == typeof(ColorBuilder);
    private static readonly bool _isSize = typeof(TBuilder) == typeof(SizeBuilder);

    private CssValue(string value, string? styleValue = null, string? cssSelector = null, bool selectorIsAbsolute = false)
    {
        _value = value ?? string.Empty;
        _styleValue = styleValue;
        _cssSelector = cssSelector;
        _selectorIsAbsolute = selectorIsAbsolute;
    }

    private CssValue(CssValue<TBuilder> source, string selector, bool selectorIsAbsolute)
    {
        _value = source._value;
        _styleValue = source._styleValue;
        _cssSelector = selector;
        _selectorIsAbsolute = selectorIsAbsolute;
    }

    /// <summary>
    /// Implicitly converts a CSS builder to a CssValue.
    /// </summary>
    public static implicit operator CssValue<TBuilder>(TBuilder builder) => new(builder.ToClass(), builder.ToStyle());

    /// <summary>
    /// Implicitly converts a string to a CssValue.
    /// </summary>
    public static implicit operator CssValue<TBuilder>(string value) => new(value);

    /// <summary>
    /// Implicitly converts an integer to a CssValue. For HeightBuilder and WidthBuilder, converts to pixel values.
    /// </summary>
    public static implicit operator CssValue<TBuilder>(int value) =>
        _isHeight ? new CssValue<TBuilder>($"height: {value}px") :
        _isWidth ? new CssValue<TBuilder>($"width: {value}px") : new CssValue<TBuilder>(value.ToString());

    /// <summary>
    /// Implicitly converts a CssValue to a string.
    /// </summary>
    public static implicit operator string(CssValue<TBuilder> v) => v._value;

    /// <summary>
    /// Returns the string representation of this CSS value.
    /// </summary>
    public override string ToString() => _value;

    /// <summary>
    /// Gets whether this CSS value is empty.
    /// </summary>
    public bool IsEmpty => _value.IsNullOrEmpty();

    /// <summary>
    /// Gets whether this CSS value represents an inline style (e.g., "color: red") rather than a CSS class.
    /// </summary>
    public bool IsCssStyle =>
        // style if it looks like "prop: val" OR (ColorBuilder with non-theme token)
        // OR (CSS unit value) OR standalone CSS values (var(), #fff, inherit, etc.)
        _value.Contains(':') || (_isColor && !IsKnownThemeOrSizeToken(_value)) ||
        LooksLikeCssUnit(_value) || LooksLikeStandaloneCssValue(_value);

    /// <summary>
    /// Gets whether this CSS value represents a CSS class (e.g., "btn-primary") rather than an inline style.
    /// </summary>
    public bool IsCssClass => !IsCssStyle && !IsEmpty;

    internal string? CssSelector => _cssSelector;

    internal bool SelectorIsAbsolute => _selectorIsAbsolute;

    /// <summary>Gets the style representation (e.g., "text-decoration: underline") if available</summary>
    public string StyleValue
    {
        get
        {
            // Check if _styleValue looks like a style (contains colon)
            if (!string.IsNullOrEmpty(_styleValue) && _styleValue.Contains(':'))
                return _styleValue;

            // Check if _value looks like a style (contains colon)
            if (!string.IsNullOrEmpty(_value) && _value.Contains(':'))
                return _value;

            // Neither looks like a style, return empty
            return string.Empty;
        }
    }

    /// <summary>
    /// Creates a new CssValue with the specified CSS selector.
    /// </summary>
    /// <param name="selector">The CSS selector to apply.</param>
    /// <param name="absolute">Whether the selector is absolute (not relative to base selector).</param>
    /// <returns>A new CssValue with the specified selector.</returns>
    public CssValue<TBuilder> WithSelector(string selector, bool absolute = false)
    {
        if (selector.IsNullOrWhiteSpace())
            return this;

        ReadOnlySpan<char> trimmed = selector.AsSpan().Trim();
        if (trimmed.Length != selector.Length)
            return new CssValue<TBuilder>(this, trimmed.ToString(), absolute);

        return new CssValue<TBuilder>(this, selector, absolute);
    }

    private static bool IsKnownThemeOrSizeToken(string value)
    {
        if (value.IsNullOrEmpty())
            return false;

        return false;
    }

    private static bool LooksLikeCssUnit(string value)
    {
        if (value.IsNullOrEmpty())
            return false;

        // Check if the value ends with common CSS units
        ReadOnlySpan<char> trimmedSpan = value.AsSpan().Trim();
        if (trimmedSpan.Length == 0)
            return false;

        string trimmed = trimmedSpan.Length == value.Length ? value : trimmedSpan.ToString();

        return trimmed.EndsWithIgnoreCase("px") || trimmed.EndsWithIgnoreCase("em") ||
               trimmed.EndsWithIgnoreCase("rem") || trimmed.EndsWithIgnoreCase("%") ||
               trimmed.EndsWithIgnoreCase("vh") || trimmed.EndsWithIgnoreCase("vw") ||
               trimmed.EndsWithIgnoreCase("vmin") || trimmed.EndsWithIgnoreCase("vmax") ||
               trimmed.Equals("auto", StringComparison.OrdinalIgnoreCase) || trimmed.Equals("inherit", StringComparison.OrdinalIgnoreCase) ||
               trimmed.Equals("initial", StringComparison.OrdinalIgnoreCase) || trimmed.Equals("unset", StringComparison.OrdinalIgnoreCase);
    }

    private static bool LooksLikeStandaloneCssValue(string value)
    {
        if (value.IsNullOrEmpty())
            return false;

        ReadOnlySpan<char> trimmedSpan = value.AsSpan().Trim();
        if (trimmedSpan.Length == 0)
            return false;

        string trimmed = trimmedSpan.Length == value.Length ? value : trimmedSpan.ToString();

        if (trimmed.StartsWith("#", StringComparison.Ordinal) || trimmed.StartsWith("rgb", StringComparison.OrdinalIgnoreCase) ||
            trimmed.StartsWith("hsl", StringComparison.OrdinalIgnoreCase) || trimmed.StartsWith("var(", StringComparison.OrdinalIgnoreCase) ||
            trimmed.StartsWith("calc(", StringComparison.OrdinalIgnoreCase) || trimmed.StartsWith("clamp(", StringComparison.OrdinalIgnoreCase) ||
            trimmed.StartsWith("min(", StringComparison.OrdinalIgnoreCase) || trimmed.StartsWith("max(", StringComparison.OrdinalIgnoreCase))
            return true;

        if (double.TryParse(trimmed, NumberStyles.Float, CultureInfo.InvariantCulture, out _))
            return true;

        return trimmed.Equals("inherit", StringComparison.OrdinalIgnoreCase) || trimmed.Equals("initial", StringComparison.OrdinalIgnoreCase) ||
               trimmed.Equals("unset", StringComparison.OrdinalIgnoreCase) || trimmed.Equals("revert", StringComparison.OrdinalIgnoreCase) ||
               trimmed.Equals("revert-layer", StringComparison.OrdinalIgnoreCase) || trimmed.Equals("auto", StringComparison.OrdinalIgnoreCase) ||
               trimmed.Equals("none", StringComparison.OrdinalIgnoreCase) || trimmed.Equals("currentColor", StringComparison.OrdinalIgnoreCase) ||
               trimmed.Equals("transparent", StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Gets whether this non-empty value affects the generated markup (class or style).
    /// </summary>
    public bool AffectsMarkup => !IsEmpty;

    /// <summary>
    /// Determines whether this CssValue is equal to another CssValue.
    /// </summary>
    public bool Equals(CssValue<TBuilder> other) => _value == other._value;

    /// <summary>
    /// Determines whether this CssValue is equal to the specified object.
    /// </summary>
    public override bool Equals(object? obj) => obj is CssValue<TBuilder> o && Equals(o);

    /// <summary>
    /// Returns the hash code for this CssValue.
    /// </summary>
    public override int GetHashCode() => StringComparer.Ordinal.GetHashCode(_value);

    /// <summary>
    /// Determines whether two CssValue instances are equal.
    /// </summary>
    public static bool operator ==(CssValue<TBuilder> a, CssValue<TBuilder> b) => a.Equals(b);

    /// <summary>
    /// Determines whether two CssValue instances are not equal.
    /// </summary>
    public static bool operator !=(CssValue<TBuilder> a, CssValue<TBuilder> b) => !a.Equals(b);

    /// <summary>
    /// Attempts to extract a Bootstrap theme token from this CSS value.
    /// </summary>
    /// <param name="token">When this method returns, contains the Bootstrap theme token if found; otherwise, null.</param>
    /// <returns>true if a Bootstrap theme token was found; otherwise, false.</returns>
    public bool TryGetBootstrapThemeToken(out string? token)
    {
        token = null;
        return false;
    }
}