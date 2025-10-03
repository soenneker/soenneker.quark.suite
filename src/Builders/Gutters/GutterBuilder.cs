using System.Collections.Generic;
using Soenneker.Extensions.String;
using Soenneker.Quark.Enums;

namespace Soenneker.Quark;

/// <summary>
/// Builder for Bootstrap gutter utilities.
/// </summary>
public class GutterBuilder : ICssBuilder
{
    private readonly List<Gutter> _gutters = new();

    public bool IsEmpty => _gutters.Count == 0;
    public bool IsCssClass => true;
    public bool IsCssStyle => false;

    public GutterBuilder Add(Gutter gutter)
    {
        _gutters.Add(gutter);
        return this;
    }

    public GutterBuilder None()
    {
        _gutters.Add(Gutter.None);
        return this;
    }

    public GutterBuilder Xs()
    {
        _gutters.Add(Gutter.Xs);
        return this;
    }

    public GutterBuilder Sm()
    {
        _gutters.Add(Gutter.Sm);
        return this;
    }

    public GutterBuilder Md()
    {
        _gutters.Add(Gutter.Md);
        return this;
    }

    public GutterBuilder Lg()
    {
        _gutters.Add(Gutter.Lg);
        return this;
    }

    public GutterBuilder Xl()
    {
        _gutters.Add(Gutter.Xl);
        return this;
    }

    public GutterBuilder X(int value)
    {
        _gutters.Add(Gutter.X(value));
        return this;
    }

    public GutterBuilder Y(int value)
    {
        _gutters.Add(Gutter.Y(value));
        return this;
    }

    public GutterBuilder All(int value)
    {
        _gutters.Add(Gutter.All(value));
        return this;
    }

    public GutterBuilder XSm(int value)
    {
        _gutters.Add(Gutter.XSm(value));
        return this;
    }

    public GutterBuilder YSm(int value)
    {
        _gutters.Add(Gutter.YSm(value));
        return this;
    }

    public GutterBuilder AllSm(int value)
    {
        _gutters.Add(Gutter.AllSm(value));
        return this;
    }

    public GutterBuilder XMd(int value)
    {
        _gutters.Add(Gutter.XMd(value));
        return this;
    }

    public GutterBuilder YMd(int value)
    {
        _gutters.Add(Gutter.YMd(value));
        return this;
    }

    public GutterBuilder AllMd(int value)
    {
        _gutters.Add(Gutter.AllMd(value));
        return this;
    }

    public GutterBuilder XLg(int value)
    {
        _gutters.Add(Gutter.XLg(value));
        return this;
    }

    public GutterBuilder YLg(int value)
    {
        _gutters.Add(Gutter.YLg(value));
        return this;
    }

    public GutterBuilder AllLg(int value)
    {
        _gutters.Add(Gutter.AllLg(value));
        return this;
    }

    public GutterBuilder XXl(int value)
    {
        _gutters.Add(Gutter.XXl(value));
        return this;
    }

    public GutterBuilder YXl(int value)
    {
        _gutters.Add(Gutter.YXl(value));
        return this;
    }

    public GutterBuilder AllXl(int value)
    {
        _gutters.Add(Gutter.AllXl(value));
        return this;
    }

    public GutterBuilder XXxl(int value)
    {
        _gutters.Add(Gutter.XXxl(value));
        return this;
    }

    public GutterBuilder YXxl(int value)
    {
        _gutters.Add(Gutter.YXxl(value));
        return this;
    }

    public GutterBuilder AllXxl(int value)
    {
        _gutters.Add(Gutter.AllXxl(value));
        return this;
    }

    public override string ToString()
    {
        if (_gutters.Count == 0) return string.Empty;

        var classes = new List<string>();

        foreach (var gutter in _gutters)
        {
            var className = GetGutterClass(gutter);
            if (!className.IsNullOrEmpty())
                classes.Add(className);
        }

        return string.Join(" ", classes);
    }

    private static string GetGutterClass(Gutter gutter)
    {
        if (gutter.Value == 0)
        {
            return gutter.Type.Value switch
            {
                GutterType.AllValue => "g-0",
                GutterType.XValue => "gx-0",
                GutterType.YValue => "gy-0",
                _ => string.Empty
            };
        }

        var prefix = gutter.Type.Value switch
        {
            GutterType.AllValue => "g",
            GutterType.XValue => "gx",
            GutterType.YValue => "gy",
            _ => string.Empty
        };

        if (prefix.IsNullOrEmpty()) return string.Empty;

        var breakpoint = gutter.Breakpoint.Value switch
        {
            GutterBreakpoint.SmValue => "-sm",
            GutterBreakpoint.MdValue => "-md",
            GutterBreakpoint.LgValue => "-lg",
            GutterBreakpoint.XlValue => "-xl",
            GutterBreakpoint.XxlValue => "-xxl",
            _ => string.Empty
        };

        return $"{prefix}{breakpoint}-{gutter.Value}";
    }

    public string ToClass() => ToString();
    public string ToStyle() => string.Empty;
}
