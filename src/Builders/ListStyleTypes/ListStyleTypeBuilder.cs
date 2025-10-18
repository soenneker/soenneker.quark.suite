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

    public string ToClass()
    {
        // list-style-type is a CSS property, not a Bootstrap class
        return string.Empty;
    }

    public string ToStyle()
    {
        var valueStr = _value.Value;
        
        if (valueStr == "none" || string.IsNullOrEmpty(valueStr))
            return string.Empty;

        return $"list-style-type: {valueStr}";
    }

    public override string ToString() => ToStyle();
}

