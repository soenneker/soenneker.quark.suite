using Soenneker.Extensions.String;
using Soenneker.Quark;
using System;
using System.Collections.Generic;

namespace Soenneker.Quark;

/// <summary>
/// Represents a CSS value that can be either a builder or a string.
/// Provides implicit conversions for seamless usage.
/// </summary>
/// <typeparam name="TBuilder">The builder type that can generate CSS classes/styles</typeparam>
public readonly struct CssValue<TBuilder> : IEquatable<CssValue<TBuilder>> where TBuilder : class, ICssBuilder
{
    private readonly string _value;

    private static readonly HashSet<string> _bootstrapThemeTokens = new(StringComparer.OrdinalIgnoreCase)
    {
        "primary",
        "secondary",
        "success",
        "danger",
        "warning",
        "info",
        "light",
        "dark",
        "link",
        "muted"
    };

    private CssValue(string value)
    {
        _value = value ?? string.Empty;
    }

    /// <summary>
    /// Implicit conversion from builder to CssValue.
    /// </summary>
    public static implicit operator CssValue<TBuilder>(TBuilder builder)
    {
        return new CssValue<TBuilder>(builder.ToClass());
    }

    /// <summary>
    /// Implicit conversion from string to CssValue.
    /// </summary>
    public static implicit operator CssValue<TBuilder>(string value)
    {
        return new CssValue<TBuilder>(value);
    }

    /// <summary>
    /// Implicit conversion from int to CssValue.
    /// For Height and Width builders, converts to proper CSS style.
    /// </summary>
    public static implicit operator CssValue<TBuilder>(int value)
    {
        // Check if this is a Height or Width builder by checking the type name
        string typeName = typeof(TBuilder).Name;

        return typeName switch
        {
            nameof(HeightBuilder) => new CssValue<TBuilder>($"height: {value}px"),
            nameof(WidthBuilder) => new CssValue<TBuilder>($"width: {value}px"),
            _ => new CssValue<TBuilder>(value.ToString())
        };
    }

    /// <summary>
    /// Implicit conversion from CssValue to string.
    /// </summary>
    public static implicit operator string(CssValue<TBuilder> cssValue)
    {
        return cssValue._value;
    }

    /// <summary>
    /// Returns the string representation of the CSS value.
    /// </summary>
    public override string ToString()
    {
        return _value;
    }

    /// <summary>
    /// Determines if this CSS value is empty.
    /// </summary>
    public bool IsEmpty => _value.IsNullOrEmpty();

    /// <summary>
    /// Determines if this CSS value contains CSS properties (contains ':') or if the builder falls back to CSS styles.
    /// </summary>
    public bool IsCssStyle => _value.Contains(':') || (typeof(TBuilder) == typeof(ColorBuilder) && !IsKnownThemeToken(_value));

    /// <summary>
    /// Determines if this CSS value is a CSS class.
    /// </summary>
    public bool IsCssClass => !IsCssStyle && !IsEmpty;

    /// <summary>
    /// Checks if the value is a known Bootstrap theme token that can generate a class.
    /// </summary>
    private static bool IsKnownThemeToken(string value)
    {
        return value.HasContent() && _bootstrapThemeTokens.Contains(value);
    }

    public bool Equals(CssValue<TBuilder> other)
    {
        return _value == other._value;
    }

    public override bool Equals(object? obj)
    {
        return obj is CssValue<TBuilder> other && Equals(other);
    }

    public override int GetHashCode()
    {
        return _value.GetHashCode();
    }

    public static bool operator ==(CssValue<TBuilder> start, CssValue<TBuilder> end)
    {
        return start.Equals(end);
    }

    public static bool operator !=(CssValue<TBuilder> start, CssValue<TBuilder> end)
    {
        return !start.Equals(end);
    }

    /// <summary>
    /// Attempts to match the value against a known Bootstrap theme token
    /// (e.g., "primary", "secondary", etc.).
    /// </summary>
    public bool TryGetBootstrapThemeToken(out string? token)
    {
        if (_value.HasContent())
        {
            string v = _value.Trim();

            if (_bootstrapThemeTokens.Contains(v))
            {
                token = v.ToLowerInvariantFast();
                return true;
            }
        }

        token = null;
        return false;
    }
}
