using Soenneker.Quark.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;


namespace Soenneker.Quark;

/// <summary>
/// Simplified user select builder with fluent API for chaining user select rules.
/// </summary>
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

    /// <summary>
    /// Sets the user select to none.
    /// </summary>
    public UserSelectBuilder None => Chain(UserSelectKeyword.NoneValue);

    /// <summary>
    /// Sets the user select to auto.
    /// </summary>
    public UserSelectBuilder Auto => Chain(UserSelectKeyword.AutoValue);

    /// <summary>
    /// Sets the user select to all.
    /// </summary>
    public UserSelectBuilder All => Chain(UserSelectKeyword.AllValue);

    /// <summary>
    /// Sets the user select to inherit.
    /// </summary>
    public UserSelectBuilder Inherit => Chain(GlobalKeyword.InheritValue);

    /// <summary>
    /// Sets the user select to initial.
    /// </summary>
    public UserSelectBuilder Initial => Chain(GlobalKeyword.InitialValue);

    /// <summary>
    /// Sets the user select to revert.
    /// </summary>
    public UserSelectBuilder Revert => Chain(GlobalKeyword.RevertValue);

    /// <summary>
    /// Sets the user select to revert-layer.
    /// </summary>
    public UserSelectBuilder RevertLayer => Chain(GlobalKeyword.RevertLayerValue);

    /// <summary>
    /// Sets the user select to unset.
    /// </summary>
    public UserSelectBuilder Unset => Chain(GlobalKeyword.UnsetValue);

    /// <summary>
    /// Applies the user select on phone breakpoint.
    /// </summary>
    public UserSelectBuilder OnPhone => ChainBp(BreakpointType.Phone);

    /// <summary>
    /// Applies the user select on tablet breakpoint.
    /// </summary>
    public UserSelectBuilder OnTablet => ChainBp(BreakpointType.Tablet);

    /// <summary>
    /// Applies the user select on laptop breakpoint.
    /// </summary>
    public UserSelectBuilder OnLaptop => ChainBp(BreakpointType.Laptop);

    /// <summary>
    /// Applies the user select on desktop breakpoint.
    /// </summary>
    public UserSelectBuilder OnDesktop => ChainBp(BreakpointType.Desktop);

    /// <summary>
    /// Applies the user select on widescreen breakpoint.
    /// </summary>
    public UserSelectBuilder OnWidescreen => ChainBp(BreakpointType.Widescreen);

    /// <summary>
    /// Applies the user select on ultrawide breakpoint.
    /// </summary>
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

    /// <summary>
    /// Gets the CSS class string for the current configuration.
    /// </summary>
    /// <returns>The CSS class string.</returns>
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


}

 
