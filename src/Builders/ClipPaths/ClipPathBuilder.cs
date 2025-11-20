using Soenneker.Quark.Enums;

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// Simplified clip path builder with fluent API for chaining clip path rules.
/// </summary>
public sealed class ClipPathBuilder : ICssBuilder
{
    private readonly List<ClipPathRule> _rules = new(4);

    private const string _classClipPathNone = "clip-path-none";
    private const string _classClipPathCircle = "clip-path-circle";
    private const string _classClipPathEllipse = "clip-path-ellipse";
    private const string _classClipPathInset = "clip-path-inset";
    private const string _classClipPathPolygon = "clip-path-polygon";

    internal ClipPathBuilder(string path, BreakpointType? breakpoint = null)
    {
        _rules.Add(new ClipPathRule(path, breakpoint));
    }

    internal ClipPathBuilder(List<ClipPathRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    public ClipPathBuilder None => ChainWithPath("none");
    public ClipPathBuilder Circle => ChainWithPath("circle");
    public ClipPathBuilder Ellipse => ChainWithPath("ellipse");
    public ClipPathBuilder Inset => ChainWithPath("inset");
    public ClipPathBuilder Polygon => ChainWithPath("polygon");

    public ClipPathBuilder OnPhone => ChainWithBreakpoint(BreakpointType.Phone);
    public ClipPathBuilder OnTablet => ChainWithBreakpoint(BreakpointType.Tablet);
    public ClipPathBuilder OnLaptop => ChainWithBreakpoint(BreakpointType.Laptop);
    public ClipPathBuilder OnDesktop => ChainWithBreakpoint(BreakpointType.Desktop);
    public ClipPathBuilder OnWidescreen => ChainWithBreakpoint(BreakpointType.Widescreen);
    public ClipPathBuilder OnUltrawide => ChainWithBreakpoint(BreakpointType.Ultrawide);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ClipPathBuilder ChainWithPath(string path)
    {
        _rules.Add(new ClipPathRule(path, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ClipPathBuilder ChainWithBreakpoint(BreakpointType breakpoint)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new ClipPathRule("none", breakpoint));
            return this;
        }

        var lastIdx = _rules.Count - 1;
        var last = _rules[lastIdx];
        _rules[lastIdx] = new ClipPathRule(last.Path, breakpoint);
        return this;
    }

    public string ToClass()
    {
        if (_rules.Count == 0)
            return string.Empty;

        using var sb = new PooledStringBuilder();
        var first = true;

        for (var i = 0; i < _rules.Count; i++)
        {
            var rule = _rules[i];
            var cls = GetClipPathClass(rule.Path);
            if (cls.Length == 0)
                continue;

            var bp = BreakpointUtil.GetBreakpointClass(rule.breakpoint);
            if (bp.Length != 0)
                cls = BreakpointUtil.InsertBreakpointType(cls, bp);

            if (!first) sb.Append(' ');
            else first = false;

            sb.Append(cls);
        }

        return sb.ToString();
    }

    public string ToStyle()
    {
        if (_rules.Count == 0)
            return string.Empty;

        using var sb = new PooledStringBuilder();
        var first = true;

        for (var i = 0; i < _rules.Count; i++)
        {
            var rule = _rules[i];
            var pathValue = GetClipPathValue(rule.Path);

            if (pathValue is null)
                continue;

            if (!first) sb.Append("; ");
            else first = false;

            sb.Append("clip-path: ");
            sb.Append(pathValue);
        }

        return sb.ToString();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string GetClipPathClass(string path)
    {
        return path switch
        {
            "none" => _classClipPathNone,
            "circle" => _classClipPathCircle,
            "ellipse" => _classClipPathEllipse,
            "inset" => _classClipPathInset,
            "polygon" => _classClipPathPolygon,
            _ => string.Empty
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string? GetClipPathValue(string path)
    {
        return path switch
        {
            "none" => "none",
            "circle" => "circle(50%)",
            "ellipse" => "ellipse(50% 50%)",
            "inset" => "inset(10%)",
            "polygon" => "polygon(0% 0%, 100% 0%, 100% 100%, 0% 100%)",
            _ => null
        };
    }


    public override string ToString()
    {
        return ToClass();
    }
}
