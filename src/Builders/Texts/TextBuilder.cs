using Soenneker.Extensions.String;
using Soenneker.Quark.Enums;

namespace Soenneker.Quark;

/// <summary>
/// Builder for Bootstrap text utilities.
/// </summary>
public class TextStyleBuilder : ICssBuilder
{
    private TextStyle? _textStyle;

    public bool IsEmpty => !_textStyle.HasValue;
    public bool IsCssClass => true;
    public bool IsCssStyle => false;

    public TextStyleBuilder Set(TextStyle text)
    {
        _textStyle = text;
        return this;
    }

    // Alignment
    public TextStyleBuilder Start()
    {
        _textStyle = TextStyle.Start;
        return this;
    }

    public TextStyleBuilder Center()
    {
        _textStyle = TextStyle.Center;
        return this;
    }

    public TextStyleBuilder End()
    {
        _textStyle = TextStyle.End;
        return this;
    }

    public TextStyleBuilder Justify()
    {
        _textStyle = TextStyle.Justify;
        return this;
    }

    // Wrapping
    public TextStyleBuilder Wrap()
    {
        _textStyle = TextStyle.Wrap;
        return this;
    }

    public TextStyleBuilder Nowrap()
    {
        _textStyle = TextStyle.Nowrap;
        return this;
    }

    public TextStyleBuilder Truncate()
    {
        _textStyle = TextStyle.Truncate;
        return this;
    }

    // Transformation
    public TextStyleBuilder Lowercase()
    {
        _textStyle = TextStyle.Lowercase;
        return this;
    }

    public TextStyleBuilder Uppercase()
    {
        _textStyle = TextStyle.Uppercase;
        return this;
    }

    public TextStyleBuilder Capitalize()
    {
        _textStyle = TextStyle.Capitalize;
        return this;
    }

    // Styling
    public TextStyleBuilder Reset()
    {
        _textStyle = TextStyle.Reset;
        return this;
    }

    public TextStyleBuilder Muted()
    {
        _textStyle = TextStyle.Muted;
        return this;
    }

    public TextStyleBuilder Emphasis()
    {
        _textStyle = TextStyle.Emphasis;
        return this;
    }

    public override string ToString()
    {
        if (!_textStyle.HasValue) return string.Empty;

        return GetTextClass(_textStyle.Value);
    }

    private static string GetTextClass(TextStyle text)
    {
        var baseClass = text.Type.Value switch
        {
            TextType.StartValue => "text-start",
            TextType.CenterValue => "text-center",
            TextType.EndValue => "text-end",
            TextType.JustifyValue => "text-justify",
            TextType.WrapValue => "text-wrap",
            TextType.NowrapValue => "text-nowrap",
            TextType.TruncateValue => "text-truncate",
            TextType.LowercaseValue => "text-lowercase",
            TextType.UppercaseValue => "text-uppercase",
            TextType.CapitalizeValue => "text-capitalize",
            TextType.ResetValue => "text-reset",
            TextType.MutedValue => "text-muted",
            TextType.EmphasisValue => "text-emphasis",
            _ => string.Empty
        };

        if (baseClass.IsNullOrEmpty()) return string.Empty;

        if (text.Breakpoint != null)
        {
            var breakpoint = text.Breakpoint.Value switch
            {
                TextBreakpoint.SmValue => "sm",
                TextBreakpoint.MdValue => "md",
                TextBreakpoint.LgValue => "lg",
                TextBreakpoint.XlValue => "xl",
                TextBreakpoint.XxlValue => "xxl",
                _ => string.Empty
            };

            return $"{baseClass}-{breakpoint}";
        }

        return baseClass;
    }

    public string ToClass() => ToString();
    public string ToStyle() => string.Empty;
}