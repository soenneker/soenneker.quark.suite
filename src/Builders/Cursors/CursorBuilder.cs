using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Quark;
using Soenneker.Quark.Enums;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// Simplified cursor builder with fluent API for chaining cursor rules.
/// </summary>
public sealed class CursorBuilder : ICssBuilder
{
    private readonly List<CursorRule> _rules = new(4);

    private const string _classCursorAuto = "cursor-auto";
    private const string _classCursorPointer = "cursor-pointer";
    private const string _classCursorGrab = "cursor-grab";
    private const string _classCursorGrabbing = "cursor-grabbing";
    private const string _classCursorText = "cursor-text";
    private const string _classCursorMove = "cursor-move";
    private const string _classCursorResize = "cursor-resize";
    private const string _classCursorNotAllowed = "cursor-not-allowed";
    private const string _classCursorHelp = "cursor-help";
    private const string _classCursorWait = "cursor-wait";
    private const string _classCursorCrosshair = "cursor-crosshair";
    private const string _classCursorZoomIn = "cursor-zoom-in";
    private const string _classCursorZoomOut = "cursor-zoom-out";

    internal CursorBuilder(string cursor, BreakpointType? breakpoint = null)
    {
        _rules.Add(new CursorRule(cursor, breakpoint));
    }

    internal CursorBuilder(List<CursorRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    public CursorBuilder Auto => ChainWithCursor("auto");
    public CursorBuilder Pointer => ChainWithCursor("pointer");
    public CursorBuilder Grab => ChainWithCursor("grab");
    public CursorBuilder Grabbing => ChainWithCursor("grabbing");
    public CursorBuilder Text => ChainWithCursor("text");
    public CursorBuilder Move => ChainWithCursor("move");
    public CursorBuilder Resize => ChainWithCursor("resize");
    public CursorBuilder NotAllowed => ChainWithCursor("not-allowed");
    public CursorBuilder Help => ChainWithCursor("help");
    public CursorBuilder Wait => ChainWithCursor("wait");
    public CursorBuilder Crosshair => ChainWithCursor("crosshair");
    public CursorBuilder ZoomIn => ChainWithCursor("zoom-in");
    public CursorBuilder ZoomOut => ChainWithCursor("zoom-out");

    public CursorBuilder OnPhone => ChainWithBreakpoint(BreakpointType.Phone);
    public CursorBuilder OnTablet => ChainWithBreakpoint(BreakpointType.Tablet);
    public CursorBuilder OnLaptop => ChainWithBreakpoint(BreakpointType.Laptop);
    public CursorBuilder OnDesktop => ChainWithBreakpoint(BreakpointType.Desktop);
    public CursorBuilder OnWidescreen => ChainWithBreakpoint(BreakpointType.Widescreen);
    public CursorBuilder OnUltrawide => ChainWithBreakpoint(BreakpointType.Ultrawide);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private CursorBuilder ChainWithCursor(string cursor)
    {
        _rules.Add(new CursorRule(cursor, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private CursorBuilder ChainWithBreakpoint(BreakpointType breakpoint)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new CursorRule("auto", breakpoint));
            return this;
        }

        int lastIdx = _rules.Count - 1;
        CursorRule last = _rules[lastIdx];
        _rules[lastIdx] = new CursorRule(last.Cursor, breakpoint);
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
            CursorRule rule = _rules[i];
            string cls = GetCursorClass(rule.Cursor);
            if (cls.Length == 0)
                continue;

            string bp = BreakpointUtil.GetBreakpointClass(rule.breakpoint);
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
            CursorRule rule = _rules[i];
            string? cursorValue = GetCursorValue(rule.Cursor);

            if (cursorValue is null)
                continue;

            if (!first) sb.Append("; ");
            else first = false;

            sb.Append("cursor: ");
            sb.Append(cursorValue);
        }

        return sb.ToString();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string GetCursorClass(string cursor)
    {
        return cursor switch
        {
            "auto" => _classCursorAuto,
            "pointer" => _classCursorPointer,
            "grab" => _classCursorGrab,
            "grabbing" => _classCursorGrabbing,
            "text" => _classCursorText,
            "move" => _classCursorMove,
            "resize" => _classCursorResize,
            "not-allowed" => _classCursorNotAllowed,
            "help" => _classCursorHelp,
            "wait" => _classCursorWait,
            "crosshair" => _classCursorCrosshair,
            "zoom-in" => _classCursorZoomIn,
            "zoom-out" => _classCursorZoomOut,
            _ => string.Empty
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string? GetCursorValue(string cursor)
    {
        return cursor switch
        {
            "auto" => "auto",
            "pointer" => "pointer",
            "grab" => "grab",
            "grabbing" => "grabbing",
            "text" => "text",
            "move" => "move",
            "resize" => "resize",
            "not-allowed" => "not-allowed",
            "help" => "help",
            "wait" => "wait",
            "crosshair" => "crosshair",
            "zoom-in" => "zoom-in",
            "zoom-out" => "zoom-out",
            _ => null
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string InsertBreakpointType(string className, string bp)
    {
        int dashIndex = className.IndexOf('-');
        if (dashIndex > 0)
        {
            int len = dashIndex + 1 + bp.Length + (className.Length - dashIndex);
            return string.Create(len, (className, dashIndex, bp), static (dst, s) =>
            {
                s.className.AsSpan(0, s.dashIndex).CopyTo(dst);
                int idx = s.dashIndex;
                dst[idx++] = '-';
                s.bp.AsSpan().CopyTo(dst[idx..]);
                idx += s.bp.Length;
                s.className.AsSpan(s.dashIndex).CopyTo(dst[idx..]);
            });
        }

        return string.Create(bp.Length + 1 + className.Length, (className, bp), static (dst, s) =>
        {
            s.bp.AsSpan().CopyTo(dst);
            int idx = s.bp.Length;
            dst[idx++] = '-';
            s.className.AsSpan().CopyTo(dst[idx..]);
        });
    }

    public override string ToString()
    {
        return ToClass();
    }
}
