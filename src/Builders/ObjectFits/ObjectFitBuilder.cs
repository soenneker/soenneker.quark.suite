using Soenneker.Quark.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// Simplified object-fit builder with fluent API for chaining object-fit rules.
/// </summary>
public sealed class ObjectFitBuilder : ICssBuilder
{
    private readonly List<ObjectFitRule> _rules = new(4);

    private const string _classContain = "object-fit-contain";
    private const string _classCover = "object-fit-cover";
    private const string _classFill = "object-fit-fill";
    private const string _classScale = "object-fit-scale";
    private const string _classNone = "object-fit-none";
    private const string _stylePrefix = "object-fit: ";

    internal ObjectFitBuilder(string fit, BreakpointType? breakpoint = null)
    {
        _rules.Add(new ObjectFitRule(fit, breakpoint));
    }

    internal ObjectFitBuilder(List<ObjectFitRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    /// <summary>
    /// Chain with contain for the next rule.
    /// </summary>
    public ObjectFitBuilder Contain => ChainWithFit(ObjectFitKeyword.ContainValue);

    /// <summary>
    /// Chain with cover for the next rule.
    /// </summary>
    public ObjectFitBuilder Cover => ChainWithFit(ObjectFitKeyword.CoverValue);

    /// <summary>
    /// Chain with fill for the next rule.
    /// </summary>
    public ObjectFitBuilder Fill => ChainWithFit(ObjectFitKeyword.FillValue);

    /// <summary>
    /// Chain with scale-down for the next rule.
    /// </summary>
    public ObjectFitBuilder ScaleDown => ChainWithFit(ObjectFitKeyword.ScaleDownValue);

    /// <summary>
    /// Chain with none for the next rule.
    /// </summary>
    public ObjectFitBuilder None => ChainWithFit(ObjectFitKeyword.NoneValue);

    /// <summary>
    /// Sets the object fit to inherit.
    /// </summary>
    public ObjectFitBuilder Inherit => ChainWithFit(GlobalKeyword.InheritValue);
    /// <summary>
    /// Sets the object fit to initial.
    /// </summary>
    public ObjectFitBuilder Initial => ChainWithFit(GlobalKeyword.InitialValue);
    /// <summary>
    /// Sets the object fit to revert.
    /// </summary>
    public ObjectFitBuilder Revert => ChainWithFit(GlobalKeyword.RevertValue);
    /// <summary>
    /// Sets the object fit to revert-layer.
    /// </summary>
    public ObjectFitBuilder RevertLayer => ChainWithFit(GlobalKeyword.RevertLayerValue);
    /// <summary>
    /// Sets the object fit to unset.
    /// </summary>
    public ObjectFitBuilder Unset => ChainWithFit(GlobalKeyword.UnsetValue);

    /// <summary>
    /// Apply on phone devices (portrait phones, less than 576px).
    /// </summary>
    public ObjectFitBuilder OnPhone => ChainWithBreakpoint(BreakpointType.Phone);

    /// <summary>
    /// Apply on mobile devices (landscape phones, 576px and up).
    /// </summary>
    public ObjectFitBuilder OnTablet => ChainWithBreakpoint(BreakpointType.Tablet);

    /// <summary>
    /// Apply on laptop devices (laptops, 992px and up).
    /// </summary>
    public ObjectFitBuilder OnLaptop => ChainWithBreakpoint(BreakpointType.Laptop);

    /// <summary>
    /// Apply on desktop devices (desktops, 1200px and up).
    /// </summary>
    public ObjectFitBuilder OnDesktop => ChainWithBreakpoint(BreakpointType.Desktop);

    /// <summary>
    /// Apply on wide screen devices (larger desktops, 1400px and up).
    /// </summary>
    public ObjectFitBuilder OnWidescreen => ChainWithBreakpoint(BreakpointType.Widescreen);
    /// <summary>
    /// Applies the object fit on ultrawide breakpoint.
    /// </summary>
    public ObjectFitBuilder OnUltrawide => ChainWithBreakpoint(BreakpointType.Ultrawide);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ObjectFitBuilder ChainWithFit(string fit)
    {
        _rules.Add(new ObjectFitRule(fit, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ObjectFitBuilder ChainWithBreakpoint(BreakpointType breakpoint)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new ObjectFitRule(ObjectFitKeyword.ContainValue, breakpoint));
            return this;
        }

        var lastIdx = _rules.Count - 1;
        var last = _rules[lastIdx];
        _rules[lastIdx] = new ObjectFitRule(last.Fit, breakpoint);
        return this;
    }

    /// <summary>
    /// Gets the CSS class string for the current configuration.
    /// </summary>
    public string ToClass()
    {
        if (_rules.Count == 0)
            return string.Empty;

        using var sb = new PooledStringBuilder();
        var first = true;

        for (var i = 0; i < _rules.Count; i++)
        {
            var rule = _rules[i];
            var cls = GetFitClass(rule.Fit);
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

    /// <summary>
    /// Gets the CSS style string for the current configuration.
    /// </summary>
    public string ToStyle()
    {
        if (_rules.Count == 0)
            return string.Empty;

        using var sb = new PooledStringBuilder();
        var first = true;

        for (var i = 0; i < _rules.Count; i++)
        {
            var rule = _rules[i];
            var val = rule.Fit;
            if (string.IsNullOrEmpty(val))
                continue;

            if (!first) sb.Append("; ");
            else first = false;

            sb.Append(_stylePrefix);
            sb.Append(val);
        }

        return sb.ToString();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string GetFitClass(string fit)
    {
        return fit switch
        {
            ObjectFitKeyword.ContainValue => _classContain,
            ObjectFitKeyword.CoverValue => _classCover,
            ObjectFitKeyword.FillValue => _classFill,
            ObjectFitKeyword.ScaleDownValue => _classScale,
            ObjectFitKeyword.NoneValue => _classNone,
            _ => string.Empty
        };
    }

}


