using Soenneker.Quark.Enums;

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// Simplified interaction builder with fluent API for chaining interaction rules.
/// </summary>
public sealed class InteractionBuilder : ICssBuilder
{
    private readonly List<InteractionRule> _rules = new(4);

    private const string _classUserSelectNone = "user-select-none";
    private const string _classUserSelectAuto = "user-select-auto";
    private const string _classUserSelectText = "user-select-text";
    private const string _classUserSelectAll = "user-select-all";
    private const string _classPointerEventsNone = "pointer-events-none";
    private const string _classPointerEventsAuto = "pointer-events-auto";

    internal InteractionBuilder(string userSelect, string pointerEvents, BreakpointType? breakpoint = null)
    {
        _rules.Add(new InteractionRule(userSelect, pointerEvents, breakpoint));
    }

    internal InteractionBuilder(List<InteractionRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    public InteractionBuilder None => ChainWithInteraction("none", "none");
    public InteractionBuilder All => ChainWithInteraction("auto", "auto");
    public InteractionBuilder NoSelect => ChainWithInteraction("none", "auto");
    public InteractionBuilder NoPointer => ChainWithInteraction("auto", "none");
    public InteractionBuilder Text => ChainWithInteraction("text", "auto");
    public InteractionBuilder AllText => ChainWithInteraction("all", "auto");

    public InteractionBuilder OnPhone => ChainWithBreakpoint(BreakpointType.Phone);
    public InteractionBuilder OnTablet => ChainWithBreakpoint(BreakpointType.Tablet);
    public InteractionBuilder OnLaptop => ChainWithBreakpoint(BreakpointType.Laptop);
    public InteractionBuilder OnDesktop => ChainWithBreakpoint(BreakpointType.Desktop);
    public InteractionBuilder OnWidescreen => ChainWithBreakpoint(BreakpointType.Widescreen);
    public InteractionBuilder OnUltrawide => ChainWithBreakpoint(BreakpointType.Ultrawide);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private InteractionBuilder ChainWithInteraction(string userSelect, string pointerEvents)
    {
        _rules.Add(new InteractionRule(userSelect, pointerEvents, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private InteractionBuilder ChainWithBreakpoint(BreakpointType breakpoint)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new InteractionRule("auto", "auto", breakpoint));
            return this;
        }

        var lastIdx = _rules.Count - 1;
        var last = _rules[lastIdx];
        _rules[lastIdx] = new InteractionRule(last.UserSelect, last.PointerEvents, breakpoint);
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
            var userSelectClass = GetUserSelectClass(rule.UserSelect);
            var pointerEventsClass = GetPointerEventsClass(rule.PointerEvents);

            if (userSelectClass.Length > 0)
            {
                var bp = BreakpointUtil.GetBreakpointClass(rule.breakpoint);
                if (bp.Length != 0)
                    userSelectClass = BreakpointUtil.InsertBreakpointType(userSelectClass, bp);

                if (!first) sb.Append(' ');
                else first = false;

                sb.Append(userSelectClass);
            }

            if (pointerEventsClass.Length > 0)
            {
                var bp = BreakpointUtil.GetBreakpointClass(rule.breakpoint);
                if (bp.Length != 0)
                    pointerEventsClass = BreakpointUtil.InsertBreakpointType(pointerEventsClass, bp);

                if (!first) sb.Append(' ');
                else first = false;

                sb.Append(pointerEventsClass);
            }
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
            var userSelectValue = GetUserSelectValue(rule.UserSelect);
            var pointerEventsValue = GetPointerEventsValue(rule.PointerEvents);

            if (userSelectValue is not null)
            {
                if (!first) sb.Append("; ");
                else first = false;

                sb.Append("user-select: ");
                sb.Append(userSelectValue);
            }

            if (pointerEventsValue is not null)
            {
                if (!first) sb.Append("; ");
                else first = false;

                sb.Append("pointer-events: ");
                sb.Append(pointerEventsValue);
            }
        }

        return sb.ToString();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string GetUserSelectClass(string userSelect)
    {
        return userSelect switch
        {
            "none" => _classUserSelectNone,
            "auto" => _classUserSelectAuto,
            "text" => _classUserSelectText,
            "all" => _classUserSelectAll,
            _ => string.Empty
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string GetPointerEventsClass(string pointerEvents)
    {
        return pointerEvents switch
        {
            "none" => _classPointerEventsNone,
            "auto" => _classPointerEventsAuto,
            _ => string.Empty
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string? GetUserSelectValue(string userSelect)
    {
        return userSelect switch
        {
            "none" => "none",
            "auto" => "auto",
            "text" => "text",
            "all" => "all",
            _ => null
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string? GetPointerEventsValue(string pointerEvents)
    {
        return pointerEvents switch
        {
            "none" => "none",
            "auto" => "auto",
            _ => null
        };
    }


    public override string ToString()
    {
        return ToClass();
    }
}
