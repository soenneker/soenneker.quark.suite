using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Quark.Enums;
using Soenneker.Utils.PooledStringBuilders;


namespace Soenneker.Quark;

public sealed class PointerEventsBuilder : ICssBuilder
{
    private readonly List<PointerEventsRule> _rules = new(4);

    // ----- Class constants -----
    private const string _classNone = "pe-none";
    private const string _classAuto = "pe-auto";

    // ----- CSS prefix & style constants (Intellenum *Value are const, so const-interp is fine) -----
    private const string _pointerEventsPrefix = "pointer-events: ";

    private const string _styleNone = $"{_pointerEventsPrefix}none";
    private const string _styleAuto = $"{_pointerEventsPrefix}auto";
    private const string _styleInherit = $"{_pointerEventsPrefix}{GlobalKeyword.InheritValue}";
    private const string _styleInitial = $"{_pointerEventsPrefix}{GlobalKeyword.InitialValue}";
    private const string _styleUnset = $"{_pointerEventsPrefix}{GlobalKeyword.UnsetValue}";
    private const string _styleRevert = $"{_pointerEventsPrefix}{GlobalKeyword.RevertValue}";
    private const string _styleRevertLayer = $"{_pointerEventsPrefix}{GlobalKeyword.RevertLayerValue}";

    internal PointerEventsBuilder(string value, BreakpointType? breakpoint = null)
    {
        _rules.Add(new PointerEventsRule(value, breakpoint));
    }

    internal PointerEventsBuilder(List<PointerEventsRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    public PointerEventsBuilder None => Chain(PointerEventsKeyword.None);
    public PointerEventsBuilder Auto => Chain(PointerEventsKeyword.Auto);
    public PointerEventsBuilder Inherit => Chain(GlobalKeyword.InheritValue);
    public PointerEventsBuilder Initial => Chain(GlobalKeyword.InitialValue);
    public PointerEventsBuilder Revert => Chain(GlobalKeyword.RevertValue);
    public PointerEventsBuilder RevertLayer => Chain(GlobalKeyword.RevertLayerValue);
    public PointerEventsBuilder Unset => Chain(GlobalKeyword.UnsetValue);

    public PointerEventsBuilder OnPhone => ChainBp(BreakpointType.Phone);
    public PointerEventsBuilder OnTablet => ChainBp(BreakpointType.Tablet);
    public PointerEventsBuilder OnLaptop => ChainBp(BreakpointType.Laptop);
    public PointerEventsBuilder OnDesktop => ChainBp(BreakpointType.Desktop);
    public PointerEventsBuilder OnWidescreen => ChainBp(BreakpointType.Widescreen);
    public PointerEventsBuilder OnUltrawide => ChainBp(BreakpointType.Ultrawide);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private PointerEventsBuilder Chain(string value)
    {
        _rules.Add(new PointerEventsRule(value, null));
        return this;
    }

    /// <summary>Apply a BreakpointType to the most recent rule (or bootstrap with "auto" if empty).</summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private PointerEventsBuilder ChainBp(BreakpointType bp)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new PointerEventsRule(PointerEventsKeyword.Auto, bp));
            return this;
        }

        var lastIdx = _rules.Count - 1;
        var last = _rules[lastIdx];
        _rules[lastIdx] = new PointerEventsRule(last.Value, bp);
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

            var baseClass = rule.Value switch
            {
                PointerEventsKeyword.NoneValue => _classNone,
                PointerEventsKeyword.AutoValue => _classAuto,
                _ => string.Empty
            };

            if (baseClass.Length == 0)
                continue;

            var bp = BreakpointUtil.GetBreakpointToken(rule.breakpoint);
            if (bp.Length != 0)
                baseClass = InsertBreakpointType(baseClass, bp);

            if (!first) sb.Append(' ');
            else first = false;

            sb.Append(baseClass);
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

            var css = rule.Value switch
            {
                PointerEventsKeyword.NoneValue => _styleNone,
                PointerEventsKeyword.AutoValue => _styleAuto,
                GlobalKeyword.InheritValue => _styleInherit,
                GlobalKeyword.InitialValue => _styleInitial,
                GlobalKeyword.UnsetValue => _styleUnset,
                GlobalKeyword.RevertValue => _styleRevert,
                GlobalKeyword.RevertLayerValue => _styleRevertLayer,
                _ => string.Empty
            };

            if (css.Length == 0)
                continue;

            if (!first) sb.Append("; ");
            else first = false;

            sb.Append(css);
        }

        return sb.ToString();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string GetBp(BreakpointType? breakpoint) => breakpoint?.Value ?? string.Empty;

    /// <summary>
    /// Insert BreakpointType token as: "pe-auto" + "md" ? "pe-md-auto".
    /// Falls back to "bp-{class}" if no dash exists.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string InsertBreakpointType(string className, string bp)
    {
        var dashIndex = className.IndexOf('-');
        if (dashIndex > 0)
        {
            // length = prefix + "-" + bp + remainder
            var len = dashIndex + 1 + bp.Length + (className.Length - dashIndex);
            return string.Create(len, (className, dashIndex, bp), static (dst, s) =>
            {
                // prefix
                s.className.AsSpan(0, s.dashIndex).CopyTo(dst);
                var idx = s.dashIndex;

                // "-" + bp
                dst[idx++] = '-';
                s.bp.AsSpan().CopyTo(dst[idx..]);
                idx += s.bp.Length;

                // remainder (starts with '-')
                s.className.AsSpan(s.dashIndex).CopyTo(dst[idx..]);
            });
        }

        // Fallback: "bp-{className}"
        return string.Create(bp.Length + 1 + className.Length, (className, bp), static (dst, s) =>
        {
            s.bp.AsSpan().CopyTo(dst);
            var idx = s.bp.Length;
            dst[idx++] = '-';
            s.className.AsSpan().CopyTo(dst[idx..]);
        });
    }
}
