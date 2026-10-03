using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using Soenneker.Extensions.String;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

internal static class SvgAttributeWriter
{
    internal static string InjectAttributesIntoSvg(string svg, IReadOnlyDictionary<string, object> attributes)
    {
        if (attributes.Count == 0)
            return svg;

        var svgStart = svg.IndexOf("<svg", StringComparison.OrdinalIgnoreCase);
        if (svgStart < 0)
            return svg;

        var tagEnd = svg.IndexOf('>', svgStart);
        if (tagEnd < 0)
            return svg;

        var attrSb = new PooledStringBuilder(stackalloc char[128]);
        try
        {
            attrSb.Append(svg.AsSpan(0, tagEnd));
            if (attributes is Dictionary<string, object> dictionary)
            {
                foreach (var pair in dictionary)
                    AppendAttribute(ref attrSb, pair.Key, pair.Value);
            }
            else if (attributes is OrderedDictionary<string, object> ordered)
            {
                foreach (var pair in ordered)
                    AppendAttribute(ref attrSb, pair.Key, pair.Value);
            }
            else
            {
                foreach (var pair in attributes)
                    AppendAttribute(ref attrSb, pair.Key, pair.Value);
            }


            if (attrSb.Length == tagEnd)
                return svg;

            attrSb.Append(svg.AsSpan(tagEnd));
            return attrSb.ToString();
        }
        finally
        {
            attrSb.Dispose();
        }
    }

    private static void AppendAttribute(ref PooledStringBuilder attrSb, string key, object? v)
    {
        if (key.Length == 0)
            return;

        if (IsClassKey(key))
        {
            if (v is string cls && cls.Length > 0)
            {
                attrSb.Append(" class=\"");
                AppendEscapedHtmlAttribute(ref attrSb, cls);
                attrSb.Append('"');
            }

            return;
        }

        if (IsStyleKey(key))
        {
            if (v is string sty && sty.Length > 0)
            {
                attrSb.Append(" style=\"");
                AppendEscapedHtmlAttribute(ref attrSb, sty);
                attrSb.Append('"');
            }

            return;
        }

        if (IsHiddenKey(key))
        {
            if (v is true || (v is string hs && (hs == "true" || hs == "hidden")))
                attrSb.Append(" hidden");
            return;
        }

        if (!IsSvgSafeAttribute(key) || v is null)
            return;

        if (v is bool b)
        {
            if (b)
            {
                attrSb.Append(' ');
                attrSb.Append(key);
            }

            return;
        }

        if (v is string s)
        {
            if (s.Length == 0)
                return;

            attrSb.Append(' ');
            attrSb.Append(key);
            attrSb.Append("=\"");
            AppendEscapedHtmlAttribute(ref attrSb, s);
            attrSb.Append('"');
            return;
        }

        if (v is int or long or uint or ulong or float or double or decimal)
        {
            AppendNumericAttribute(ref attrSb, key, v);
            return;
        }

        var val = v switch
        {
            IFormattable fmt => fmt.ToString(null, CultureInfo.InvariantCulture),
            _ => v.ToString()
        };

        if (val.HasContent())
        {
            attrSb.Append(' ');
            attrSb.Append(key);
            attrSb.Append("=\"");
            AppendEscapedHtmlAttribute(ref attrSb, val);
            attrSb.Append('"');
        }    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool IsClassKey(string key) => key.Length == 5 && (key[0] == 'c' || key[0] == 'C') && key.Equals("class", StringComparison.OrdinalIgnoreCase);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool IsStyleKey(string key) => key.Length == 5 && (key[0] == 's' || key[0] == 'S') && key.Equals("style", StringComparison.OrdinalIgnoreCase);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool IsHiddenKey(string key) => key.Length == 6 && (key[0] == 'h' || key[0] == 'H') && key.Equals("hidden", StringComparison.OrdinalIgnoreCase);

    private static bool IsSvgSafeAttribute(string key)
    {
        return key.Length > 0 && (key.StartsWith("aria-", StringComparison.OrdinalIgnoreCase) || key.StartsWith("data-", StringComparison.OrdinalIgnoreCase) || key.Equals("id", StringComparison.OrdinalIgnoreCase) || key.Equals("title", StringComparison.OrdinalIgnoreCase) || key.Equals("role", StringComparison.OrdinalIgnoreCase) || key.Equals("tabindex", StringComparison.OrdinalIgnoreCase) || key.Equals("focusable", StringComparison.OrdinalIgnoreCase));
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static void AppendNumericAttribute(ref PooledStringBuilder builder, string key, object value)
    {
        Span<char> text = stackalloc char[64];
        var format = value is float or double ? "R" : null;
        var number = (ISpanFormattable)value;
        if (number.TryFormat(text, out var length, format, CultureInfo.InvariantCulture))
        {
            builder.Append(' ');
            builder.Append(key);
            builder.Append("=\"");
            AppendEscapedHtmlAttribute(ref builder, text[..length]);
            builder.Append('"');
        }
        else
        {
            var formatted = number.ToString(format, CultureInfo.InvariantCulture);
            builder.Append(' ');
            builder.Append(key);
            builder.Append("=\"");
            AppendEscapedHtmlAttribute(ref builder, formatted);
            builder.Append('"');
        }
    }

    private static void AppendEscapedHtmlAttribute(ref PooledStringBuilder sb, scoped ReadOnlySpan<char> value)
    {
        while (!value.IsEmpty)
        {
            var index = value.IndexOfAny("&\"<>");
            if (index < 0)
            {
                sb.Append(value);
                return;
            }
            sb.Append(value[..index]);
            sb.Append(value[index] switch
            {
                '&' => "&amp;",
                '"' => "&quot;",
                '<' => "&lt;",
                _ => "&gt;"
            });
            value = value[(index + 1)..];
        }
    }
}
