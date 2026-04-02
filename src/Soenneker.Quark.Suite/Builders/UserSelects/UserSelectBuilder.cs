using Soenneker.Quark.Attributes;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;


namespace Soenneker.Quark;

/// <summary>
/// Simplified user select builder with fluent API for chaining user select rules.
/// </summary>
[TailwindPrefix("select-", Responsive = true)]
public sealed class UserSelectBuilder : ICssBuilder
{
    private readonly List<UserSelectRule> _rules = new(4);
    private BreakpointType? _pendingBreakpoint;

    private const string _classNone = "select-none";
    private const string _classAuto = "select-auto";
    private const string _classAll = "select-all";

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
    /// Applies the user select on phone breakpoint.
    /// </summary>
    public UserSelectBuilder OnBase => SetPendingBreakpoint(BreakpointType.Base);

    /// <summary>
    /// Applies the user select on small breakpoint (≥640px).
    /// </summary>
    public UserSelectBuilder OnSm => SetPendingBreakpoint(BreakpointType.Sm);

    /// <summary>
    /// Applies the user select on tablet breakpoint.
    /// </summary>
    public UserSelectBuilder OnMd => SetPendingBreakpoint(BreakpointType.Md);

    /// <summary>
    /// Applies the user select on laptop breakpoint.
    /// </summary>
    public UserSelectBuilder OnLg => SetPendingBreakpoint(BreakpointType.Lg);

    /// <summary>
    /// Applies the user select on desktop breakpoint.
    /// </summary>
    public UserSelectBuilder OnXl => SetPendingBreakpoint(BreakpointType.Xl);

    /// <summary>
    /// Applies the user select on the 2xl breakpoint.
    /// </summary>
    public UserSelectBuilder On2xl => SetPendingBreakpoint(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private UserSelectBuilder Chain(string value)
    {
        _rules.Add(new UserSelectRule(value, ConsumePendingBreakpoint()));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private UserSelectBuilder SetPendingBreakpoint(BreakpointType bp)
    {
        _pendingBreakpoint = bp;
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private BreakpointType? ConsumePendingBreakpoint()
    {
        var breakpoint = _pendingBreakpoint;
        _pendingBreakpoint = null;
        return breakpoint;
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

            var bp = BreakpointUtil.GetBreakpointToken(rule.Breakpoint);
            if (bp.Length != 0)
                cls = BreakpointUtil.ApplyTailwindBreakpoint(cls, bp);

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
        return string.Empty;
    }
    public override string ToString() => ToClass();
}