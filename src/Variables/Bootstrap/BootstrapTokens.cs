using System;
using System.Collections.Generic;

namespace Soenneker.Quark;

/// <summary>
/// Bootstrap theme and size tokens used for validation.
/// </summary>
public static class BootstrapTokens
{
    /// <summary>
    /// Bootstrap theme color tokens (primary, secondary, success, danger, warning, info, light, dark, link, muted).
    /// </summary>
    public static readonly HashSet<string> ThemeTokens = new(StringComparer.OrdinalIgnoreCase)
        { "primary", "secondary", "success", "danger", "warning", "info", "light", "dark", "link", "muted" };

    /// <summary>
    /// Bootstrap size tokens (xs, sm, md, lg, xl, xxl).
    /// </summary>
    public static readonly HashSet<string> SizeTokens = new(StringComparer.OrdinalIgnoreCase) 
        { "xs", "sm", "md", "lg", "xl", "xxl" };
}

