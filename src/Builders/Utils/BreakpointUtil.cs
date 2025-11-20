using System;
using Soenneker.Quark.Enums;

using System.Runtime.CompilerServices;

namespace Soenneker.Quark;

/// <summary>
/// Shared utilities for converting BreakpointTypes to CSS class tokens.
/// </summary>
public static class BreakpointUtil
{
    /// <summary>
    /// Converts a BreakpointType to its corresponding CSS class token.
    /// Returns empty string for phone/extra-small (default) BreakpointTypes.
    /// </summary>
    /// <param name="breakpoint">The BreakpointType to convert</param>
    /// <returns>The CSS class token (e.g., "sm", "md", "lg", "xl", "xxl") or empty string</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string GetBreakpointToken(BreakpointType? breakpoint)
    {
        return breakpoint?.Value ?? string.Empty;
    }

    /// <summary>
    /// Converts a BreakpointType to its corresponding CSS class token.
    /// Alias for GetBreakpointToken for backward compatibility.
    /// </summary>
    /// <param name="breakpoint">The BreakpointType to convert</param>
    /// <returns>The CSS class token (e.g., "sm", "md", "lg", "xl", "xxl") or empty string</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string GetBreakpointClass(BreakpointType? breakpoint) => GetBreakpointToken(breakpoint);

    /// <summary>
    /// Insert BreakpointType token into a CSS class name.
    /// For example: "text-break" + "md" => "text-md-break".
    /// Falls back to "bp-{className}" if no dash exists in the class name.
    /// </summary>
    /// <param name="className">The base CSS class name</param>
    /// <param name="bp">The breakpoint token (e.g., "sm", "md", "lg", "xl", "xxl")</param>
    /// <returns>The CSS class name with the breakpoint inserted</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string InsertBreakpointType(string className, string bp)
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

