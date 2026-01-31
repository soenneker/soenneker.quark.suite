using Soenneker.Quark.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// Simplified cursor builder with fluent API for chaining cursor rules.
/// </summary>
public sealed class CursorBuilder : ICssBuilder
{
    private readonly List<CursorRule> _rules = new(4);

    internal CursorBuilder(string cursor, BreakpointType? breakpoint = null)
    {
        _rules.Add(new CursorRule(cursor, breakpoint));
    }

    internal CursorBuilder(List<CursorRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    /// <summary>
    /// Sets the cursor to auto.
    /// </summary>
    public CursorBuilder Auto => ChainWithCursor("auto");
    /// <summary>
    /// Sets the cursor to pointer.
    /// </summary>
    public CursorBuilder Pointer => ChainWithCursor("pointer");
    /// <summary>
    /// Sets the cursor to grab.
    /// </summary>
    public CursorBuilder Grab => ChainWithCursor("grab");
    /// <summary>
    /// Sets the cursor to grabbing.
    /// </summary>
    public CursorBuilder Grabbing => ChainWithCursor("grabbing");
    /// <summary>
    /// Sets the cursor to text.
    /// </summary>
    public CursorBuilder Text => ChainWithCursor("text");
    /// <summary>
    /// Sets the cursor to move.
    /// </summary>
    public CursorBuilder Move => ChainWithCursor("move");
    /// <summary>
    /// Sets the cursor to resize.
    /// </summary>
    public CursorBuilder Resize => ChainWithCursor("resize");
    /// <summary>
    /// Sets the cursor to not-allowed.
    /// </summary>
    public CursorBuilder NotAllowed => ChainWithCursor("not-allowed");
    /// <summary>
    /// Sets the cursor to help.
    /// </summary>
    public CursorBuilder Help => ChainWithCursor("help");
    /// <summary>
    /// Sets the cursor to wait.
    /// </summary>
    public CursorBuilder Wait => ChainWithCursor("wait");
    /// <summary>
    /// Sets the cursor to crosshair.
    /// </summary>
    public CursorBuilder Crosshair => ChainWithCursor("crosshair");
    /// <summary>
    /// Sets the cursor to zoom-in.
    /// </summary>
    public CursorBuilder ZoomIn => ChainWithCursor("zoom-in");
    /// <summary>
    /// Sets the cursor to zoom-out.
    /// </summary>
    public CursorBuilder ZoomOut => ChainWithCursor("zoom-out");

    /// <summary>
    /// Applies the cursor on phone breakpoint.
    /// </summary>
    public CursorBuilder OnPhone => ChainWithBreakpoint(BreakpointType.Phone);
    /// <summary>
    /// Applies the cursor on tablet breakpoint.
    /// </summary>
    public CursorBuilder OnTablet => ChainWithBreakpoint(BreakpointType.Tablet);
    /// <summary>
    /// Applies the cursor on laptop breakpoint.
    /// </summary>
    public CursorBuilder OnLaptop => ChainWithBreakpoint(BreakpointType.Laptop);
    /// <summary>
    /// Applies the cursor on desktop breakpoint.
    /// </summary>
    public CursorBuilder OnDesktop => ChainWithBreakpoint(BreakpointType.Desktop);
    /// <summary>
    /// Applies the cursor on widescreen breakpoint.
    /// </summary>
    public CursorBuilder OnWidescreen => ChainWithBreakpoint(BreakpointType.Widescreen);
    /// <summary>
    /// Applies the cursor on ultrawide breakpoint.
    /// </summary>
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

        var lastIdx = _rules.Count - 1;
        var last = _rules[lastIdx];
        _rules[lastIdx] = new CursorRule(last.Cursor, breakpoint);
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
            var cls = GetCursorClass(rule.Cursor);
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
            var cursorValue = GetCursorValue(rule.Cursor);

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
        // Bootstrap doesn't have cursor utility classes, so always return empty
        // Cursor styles should be applied via inline styles using ToStyle()
        return string.Empty;
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

    /// <summary>
    /// Returns the CSS style string representation of this cursor builder.
    /// </summary>
    /// <returns>The CSS style string.</returns>
    public override string ToString()
    {
        return ToStyle();
    }
}
