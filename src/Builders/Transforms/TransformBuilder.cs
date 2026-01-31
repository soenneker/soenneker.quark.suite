using Soenneker.Quark.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

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

    /// <summary>
    /// Sets the transform to none.
    /// </summary>
    public TransformBuilder None => ChainWithTransform("none");
    /// <summary>
    /// Sets the transform to scale.
    /// </summary>
    public TransformBuilder Scale => ChainWithTransform("scale");
    /// <summary>
    /// Sets the transform to rotate.
    /// </summary>
    public TransformBuilder Rotate => ChainWithTransform("rotate");
    /// <summary>
    /// Sets the transform to translate.
    /// </summary>
    public TransformBuilder Translate => ChainWithTransform("translate");
    /// <summary>
    /// Sets the transform to skew.
    /// </summary>
    public TransformBuilder Skew => ChainWithTransform("skew");

    /// <summary>
    /// Applies the transform on phone breakpoint.
    /// </summary>
    public TransformBuilder OnPhone => ChainWithBreakpoint(BreakpointType.Phone);
    /// <summary>
    /// Applies the transform on tablet breakpoint.
    /// </summary>
    public TransformBuilder OnTablet => ChainWithBreakpoint(BreakpointType.Tablet);
    /// <summary>
    /// Applies the transform on laptop breakpoint.
    /// </summary>
    public TransformBuilder OnLaptop => ChainWithBreakpoint(BreakpointType.Laptop);
    /// <summary>
    /// Applies the transform on desktop breakpoint.
    /// </summary>
    public TransformBuilder OnDesktop => ChainWithBreakpoint(BreakpointType.Desktop);
    /// <summary>
    /// Applies the transform on widescreen breakpoint.
    /// </summary>
    public TransformBuilder OnWidescreen => ChainWithBreakpoint(BreakpointType.Widescreen);
    /// <summary>
    /// Applies the transform on ultrawide breakpoint.
    /// </summary>
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

    /// <summary>
    /// Gets the CSS class string for the current configuration.
    /// </summary>
    /// <returns>The CSS class string.</returns>
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

            var bp = BreakpointUtil.GetBreakpointClass(rule.Breakpoint);
            if (bp.Length != 0)
                cls = BreakpointUtil.InsertBreakpointType(cls, bp);

            if (!first) sb.Append(' ');
            else first = false;

            sb.Append(cls);
        }

        return sb.ToString();
    }

    /// <summary>
    /// Gets the CSS style string for the current configuration.
    /// </summary>
    /// <returns>The CSS style string.</returns>
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

    /// <summary>
    /// Returns the CSS class string representation of this transform builder.
    /// </summary>
    /// <returns>The CSS class string.</returns>
    public override string ToString()
    {
        return ToClass();
    }
}

