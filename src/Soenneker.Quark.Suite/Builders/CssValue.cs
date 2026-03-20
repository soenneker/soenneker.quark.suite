using System;
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
        _isHeight || _isWidth ? new CssValue<TBuilder>($"{value}px", $"{value}px") : new CssValue<TBuilder>(value.ToString());

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
    /// Gets whether this CSS value represents an inline style.
    /// </summary>
    public bool IsCssStyle => !string.IsNullOrEmpty(_styleValue);

    /// <summary>
    /// Gets whether this CSS value represents a CSS class (e.g., "btn-primary") rather than an inline style.
    /// </summary>
    public bool IsCssClass => !IsCssStyle && !IsEmpty;

    internal string? CssSelector => _cssSelector;

    internal bool SelectorIsAbsolute => _selectorIsAbsolute;

    /// <summary>Gets the explicit style representation if available.</summary>
    public string StyleValue
    {
        get
        {
            return _styleValue ?? string.Empty;
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

        var trimmed = selector.AsSpan().Trim();
        if (trimmed.Length != selector.Length)
            return new CssValue<TBuilder>(this, trimmed.ToString(), absolute);

        return new CssValue<TBuilder>(this, selector, absolute);
    }

    private static bool IsKnownThemeOrSizeToken(string value)
    {
        if (value.IsNullOrEmpty())
            return false;

        return value.Equals("xs", StringComparison.OrdinalIgnoreCase) ||
               value.Equals("sm", StringComparison.OrdinalIgnoreCase) ||
               value.Equals("md", StringComparison.OrdinalIgnoreCase) ||
               value.Equals("lg", StringComparison.OrdinalIgnoreCase) ||
               value.Equals("xl", StringComparison.OrdinalIgnoreCase) ||
               value.Equals("xxl", StringComparison.OrdinalIgnoreCase) ||
               value.Equals("primary", StringComparison.OrdinalIgnoreCase) ||
               value.Equals("primary-foreground", StringComparison.OrdinalIgnoreCase) ||
               value.Equals("secondary", StringComparison.OrdinalIgnoreCase) ||
               value.Equals("secondary-foreground", StringComparison.OrdinalIgnoreCase) ||
               value.Equals("success", StringComparison.OrdinalIgnoreCase) ||
               value.Equals("danger", StringComparison.OrdinalIgnoreCase) ||
               value.Equals("destructive", StringComparison.OrdinalIgnoreCase) ||
               value.Equals("destructive-foreground", StringComparison.OrdinalIgnoreCase) ||
               value.Equals("warning", StringComparison.OrdinalIgnoreCase) ||
               value.Equals("info", StringComparison.OrdinalIgnoreCase) ||
               value.Equals("light", StringComparison.OrdinalIgnoreCase) ||
               value.Equals("dark", StringComparison.OrdinalIgnoreCase) ||
               value.Equals("background", StringComparison.OrdinalIgnoreCase) ||
               value.Equals("foreground", StringComparison.OrdinalIgnoreCase) ||
               value.Equals("card", StringComparison.OrdinalIgnoreCase) ||
               value.Equals("card-foreground", StringComparison.OrdinalIgnoreCase) ||
               value.Equals("popover", StringComparison.OrdinalIgnoreCase) ||
               value.Equals("popover-foreground", StringComparison.OrdinalIgnoreCase) ||
               value.Equals("accent", StringComparison.OrdinalIgnoreCase) ||
               value.Equals("accent-foreground", StringComparison.OrdinalIgnoreCase) ||
               value.Equals("input", StringComparison.OrdinalIgnoreCase) ||
               value.Equals("ring", StringComparison.OrdinalIgnoreCase) ||
               value.Equals("border", StringComparison.OrdinalIgnoreCase) ||
               value.Equals("body", StringComparison.OrdinalIgnoreCase) ||
               value.Equals("body-secondary", StringComparison.OrdinalIgnoreCase) ||
               value.Equals("body-tertiary", StringComparison.OrdinalIgnoreCase) ||
               value.Equals("link", StringComparison.OrdinalIgnoreCase) ||
               value.Equals("muted", StringComparison.OrdinalIgnoreCase) ||
               value.Equals("muted-foreground", StringComparison.OrdinalIgnoreCase) ||
               value.Equals("white", StringComparison.OrdinalIgnoreCase) ||
               value.Equals("black", StringComparison.OrdinalIgnoreCase);
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
    /// Attempts to extract a theme token from this CSS value (e.g. size or color token for theme mapping).
    /// </summary>
    /// <param name="token">When this method returns, contains the theme token if found; otherwise, null.</param>
    /// <returns>true if a theme token was found; otherwise, false.</returns>
    public bool TryGetThemeToken(out string? token)
    {
        token = null;
        if (_value.IsNullOrEmpty())
            return false;
        if (!IsKnownThemeOrSizeToken(_value))
            return false;
        token = _value;
        return true;
    }
}
