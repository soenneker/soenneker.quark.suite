using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

/// <summary>
/// Bootstrap's breakpoint CSS variables
/// </summary>
public sealed class BootstrapBreakpointsCssVariables : IBootstrapCssVariableGroup
{
    /// <summary>
    /// Breakpoint XS. Default: 0
    /// </summary>
    public string? BreakpointXs { get; set; }

    /// <summary>
    /// Breakpoint SM. Default: 576px
    /// </summary>
    public string? BreakpointSm { get; set; }

    /// <summary>
    /// Breakpoint MD. Default: 768px
    /// </summary>
    public string? BreakpointMd { get; set; }

    /// <summary>
    /// Breakpoint LG. Default: 992px
    /// </summary>
    public string? BreakpointLg { get; set; }

    /// <summary>
    /// Breakpoint XL. Default: 1200px
    /// </summary>
    public string? BreakpointXl { get; set; }

    /// <summary>
    /// Breakpoint XXL. Default: 1400px
    /// </summary>
    public string? BreakpointXxl { get; set; }

	/// <summary>
	/// Gets the CSS selector for the breakpoints component.
	/// </summary>
	/// <returns>The CSS selector string.</returns>
    public string GetSelector()
    {
        return ":root";
    }

	/// <summary>
	/// Gets the collection of CSS variables for the breakpoints component.
	/// </summary>
	/// <returns>An enumerable collection of CSS property name and value tuples.</returns>
    public IEnumerable<(string CssPropertyName, string Value)> GetCssVariables()
    {
        if (BreakpointXs.HasContent())
            yield return ("--bs-breakpoint-xs", BreakpointXs);

        if (BreakpointSm.HasContent())
            yield return ("--bs-breakpoint-sm", BreakpointSm);

        if (BreakpointMd.HasContent())
            yield return ("--bs-breakpoint-md", BreakpointMd);

        if (BreakpointLg.HasContent())
            yield return ("--bs-breakpoint-lg", BreakpointLg);

        if (BreakpointXl.HasContent())
            yield return ("--bs-breakpoint-xl", BreakpointXl);

        if (BreakpointXxl.HasContent())
            yield return ("--bs-breakpoint-xxl", BreakpointXxl);
    }
}
