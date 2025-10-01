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
}

