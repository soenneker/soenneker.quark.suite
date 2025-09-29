using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Quark.Enums;

using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// Simplified transform builder with fluent API for chaining transform rules.
/// </summary>
public sealed class TransformBuilder : ICssBuilder
{
    private readonly List<TransformRule> _rules = new(4);

    private const string _classTransformNone = "transform-none";
    private const string _classTransformScale = "scale";
    private const string _classTransformRotate = "rotate";
    private const string _classTransformTranslate = "translate";
    private const string _classTransformSkew = "skew";

    internal TransformBuilder(string transform, BreakpointType? breakpoint = null)
    {
        _rules.Add(new TransformRule(transform, breakpoint));
    }

    internal TransformBuilder(List<TransformRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    public TransformBuilder None => ChainWithTransform("none");
    public TransformBuilder Scale => ChainWithTransform("scale");
    public TransformBuilder Rotate => ChainWithTransform("rotate");
    public TransformBuilder Translate => ChainWithTransform("translate");
    public TransformBuilder Skew => ChainWithTransform("skew");

    public TransformBuilder OnPhone => ChainWithBreakpoint(BreakpointType.Phone);
    public TransformBuilder OnTablet => ChainWithBreakpoint(BreakpointType.Tablet);
    public TransformBuilder OnLaptop => ChainWithBreakpoint(BreakpointType.Laptop);
    public TransformBuilder OnDesktop => ChainWithBreakpoint(BreakpointType.Desktop);
    public TransformBuilder OnWidescreen => ChainWithBreakpoint(BreakpointType.Widescreen);
    public TransformBuilder OnUltrawide => ChainWithBreakpoint(BreakpointType.Ultrawide);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private TransformBuilder ChainWithTransform(string transform)
    {
        _rules.Add(new TransformRule(transform, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private TransformBuilder ChainWithBreakpoint(BreakpointType breakpoint)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new TransformRule("none", breakpoint));
            return this;
        }

        var lastIdx = _rules.Count - 1;
        var last = _rules[lastIdx];
        _rules[lastIdx] = new TransformRule(last.Transform, breakpoint);
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
            var cls = GetTransformClass(rule.Transform);
            if (cls.Length == 0)
                continue;

            var bp = BreakpointUtil.GetBreakpointClass(rule.breakpoint);
            if (bp.Length != 0)
                cls = InsertBreakpointType(cls, bp);

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
            var transformValue = GetTransformValue(rule.Transform);

            if (transformValue is null)
                continue;

            if (!first) sb.Append("; ");
            else first = false;

            sb.Append("transform: ");
            sb.Append(transformValue);
        }

        return sb.ToString();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string GetTransformClass(string transform)
    {
        return transform switch
        {
            "none" => _classTransformNone,
            "scale" => _classTransformScale,
            "rotate" => _classTransformRotate,
            "translate" => _classTransformTranslate,
            "skew" => _classTransformSkew,
            _ => string.Empty
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string? GetTransformValue(string transform)
    {
        return transform switch
        {
            "none" => "none",
            "scale" => "scale(1)",
            "rotate" => "rotate(0deg)",
            "translate" => "translate(0, 0)",
            "skew" => "skew(0deg, 0deg)",
            _ => null
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string InsertBreakpointType(string className, string bp)
    {
        var dashIndex = className.IndexOf('-');
        if (dashIndex > 0)
        {
            var len = dashIndex + 1 + bp.Length + (className.Length - dashIndex);
            return string.Create(len, (className, dashIndex, bp), static (dst, s) =>
            {
                s.className.AsSpan(0, s.dashIndex).CopyTo(dst);
                var idx = s.dashIndex;
                dst[idx++] = '-';
                s.bp.AsSpan().CopyTo(dst[idx..]);
                idx += s.bp.Length;
                s.className.AsSpan(s.dashIndex).CopyTo(dst[idx..]);
            });
        }

        return string.Create(bp.Length + 1 + className.Length, (className, bp), static (dst, s) =>
        {
            s.bp.AsSpan().CopyTo(dst);
            var idx = s.bp.Length;
            dst[idx++] = '-';
            s.className.AsSpan().CopyTo(dst[idx..]);
        });
    }

    public override string ToString()
    {
        return ToClass();
    }
}

