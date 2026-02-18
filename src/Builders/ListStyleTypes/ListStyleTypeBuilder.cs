using Soenneker.Quark.Enums;

namespace Soenneker.Quark;

/// <summary>
/// Builder for CSS list-style-type property.
/// </summary>
public sealed class ListStyleTypeBuilder : ICssBuilder
{
    private readonly ListStyleTypeValue _value;

    internal ListStyleTypeBuilder(ListStyleTypeValue value)
    {
        _value = value;
    }

    /// <summary>
    /// Gets the CSS class string for the current configuration.
    /// list-style-type is a CSS property, not a class utility, so this returns an empty string.
    /// </summary>
    /// <returns>An empty string.</returns>
    public string ToClass()
    {
        // list-style-type is a CSS property, not a class utility
        return string.Empty;
    }

    /// <summary>
    /// Gets the CSS style string for the current configuration.
    /// </summary>
    /// <returns>The CSS style string.</returns>
    public string ToStyle()
    {
        string? valueStr = _value.Value;
        
        if (valueStr == "none" || string.IsNullOrEmpty(valueStr))
            return string.Empty;

        return $"list-style-type: {valueStr}";
    }

    /// <summary>
    /// Returns the CSS style string representation of this list style type builder.
    /// </summary>
    /// <returns>The CSS style string.</returns>
    public override string ToString() => ToStyle();
}

