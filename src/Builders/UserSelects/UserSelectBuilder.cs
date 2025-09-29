using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Quark.Enums;
using Soenneker.Utils.PooledStringBuilders;


namespace Soenneker.Quark;

public sealed class UserSelectBuilder : ICssBuilder
{
    private readonly List<UserSelectRule> _rules = new(4);

    private const string _classNone = "user-select-none";
    private const string _classAuto = "user-select-auto";
    private const string _classAll = "user-select-all";
    private const string _stylePrefix = "user-select: ";

    internal UserSelectBuilder(string value, BreakpointType? breakpoint = null)
    {
        _rules.Add(new UserSelectRule(value, breakpoint));
    }

    internal UserSelectBuilder(List<UserSelectRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    public UserSelectBuilder None => Chain(UserSelectKeyword.NoneValue);
    public UserSelectBuilder Auto => Chain(UserSelectKeyword.AutoValue);
    public UserSelectBuilder All => Chain(UserSelectKeyword.AllValue);
    public UserSelectBuilder Inherit => Chain(GlobalKeyword.InheritValue);
    public UserSelectBuilder Initial => Chain(GlobalKeyword.InitialValue);
    public UserSelectBuilder Revert => Chain(GlobalKeyword.RevertValue);
    public UserSelectBuilder RevertLayer => Chain(GlobalKeyword.RevertLayerValue);
    public UserSelectBuilder Unset => Chain(GlobalKeyword.UnsetValue);

    public UserSelectBuilder OnPhone => ChainBp(BreakpointType.Phone);
    public UserSelectBuilder OnTablet => ChainBp(BreakpointType.Tablet);
    public UserSelectBuilder OnLaptop => ChainBp(BreakpointType.Laptop);
    public UserSelectBuilder OnDesktop => ChainBp(BreakpointType.Desktop);
    public UserSelectBuilder OnWidescreen => ChainBp(BreakpointType.Widescreen);
    public UserSelectBuilder OnUltrawide => ChainBp(BreakpointType.Ultrawide);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private UserSelectBuilder Chain(string value)
    {
        _rules.Add(new UserSelectRule(value, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private UserSelectBuilder ChainBp(BreakpointType bp)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new UserSelectRule(UserSelectKeyword.AutoValue, bp));
            return this;
        }

        var lastIdx = _rules.Count - 1;
        var last = _rules[lastIdx];
        _rules[lastIdx] = new UserSelectRule(last.Value, bp);
        return this;
    }

    public string ToClass()
    {
        if (_rules.Count == 0) return string.Empty;

        using var sb = new PooledStringBuilder();
        var first = true;
        for (var i = 0; i < _rules.Count; i++)
        {
            var rule = _rules[i];
            var cls = rule.Value switch
            {
                UserSelectKeyword.NoneValue => _classNone,
                UserSelectKeyword.AutoValue => _classAuto,
                UserSelectKeyword.AllValue => _classAll,
                _ => string.Empty
            };
            if (cls.Length == 0)
                continue;

            var bp = BreakpointUtil.GetBreakpointToken(rule.breakpoint);
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
        if (_rules.Count == 0) return string.Empty;

        using var sb = new PooledStringBuilder();
        var first = true;
        for (var i = 0; i < _rules.Count; i++)
        {
            var rule = _rules[i];
            var val = rule.Value;
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
}

 
