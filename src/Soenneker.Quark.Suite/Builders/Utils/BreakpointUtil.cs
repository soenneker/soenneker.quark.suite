using System;
using System.Runtime.CompilerServices;

namespace Soenneker.Quark;

/// <summary>
/// Shared utilities for converting breakpoint values into Tailwind-responsive class prefixes.
/// </summary>
public static class BreakpointUtil
{
    /// <summary>
    /// Converts a BreakpointType to its corresponding CSS class token.
    /// Returns empty string for phone/extra-small (default) BreakpointTypes.
    /// </summary>
    /// <param name="breakpoint">The BreakpointType to convert</param>
    /// <returns>The CSS class token (e.g., "sm", "md", "lg", "xl", "2xl") or empty string.</returns>
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
    /// <returns>The CSS class token (e.g., "sm", "md", "lg", "xl", "2xl") or empty string.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string GetBreakpointClass(BreakpointType? breakpoint) => GetBreakpointToken(breakpoint);

    /// <summary>
    /// Returns the Tailwind responsive class: breakpoint prefix + class (e.g. "md" + "col-span-2" => "md:col-span-2").
    /// Use for all Tailwind utilities that use the bp:utility format.
    /// </summary>
    /// <param name="className">The base CSS class name</param>
    /// <param name="bp">The breakpoint token (e.g., "sm", "md", "lg", "xl", "2xl") or empty</param>
    /// <returns>The class with Tailwind responsive prefix, or the class unchanged if bp is empty</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ApplyTailwindBreakpoint(string className, string bp)
    {
        if (string.IsNullOrEmpty(bp))
            return className;
        return string.Create(bp.Length + 1 + className.Length, (className, bp), static (dst, s) =>
        {
            s.bp.AsSpan().CopyTo(dst);
            var idx = s.bp.Length;
            dst[idx++] = ':';
            s.className.AsSpan().CopyTo(dst[idx..]);
        });
    }
}

