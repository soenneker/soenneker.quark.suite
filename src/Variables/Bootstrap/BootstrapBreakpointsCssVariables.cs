namespace Soenneker.Quark;

/// <summary>
/// Bootstrap's breakpoint CSS variables
/// </summary>
[CssSelector]
public sealed class BootstrapBreakpointsCssVariables
{
    /// <summary>
    /// Breakpoint XS. Default: 0
    /// </summary>
    [CssVariable("bs-breakpoint-xs")]
    public string? BreakpointXs { get; set; }

    /// <summary>
    /// Breakpoint SM. Default: 576px
    /// </summary>
    [CssVariable("bs-breakpoint-sm")]
    public string? BreakpointSm { get; set; }

    /// <summary>
    /// Breakpoint MD. Default: 768px
    /// </summary>
    [CssVariable("bs-breakpoint-md")]
    public string? BreakpointMd { get; set; }

    /// <summary>
    /// Breakpoint LG. Default: 992px
    /// </summary>
    [CssVariable("bs-breakpoint-lg")]
    public string? BreakpointLg { get; set; }

    /// <summary>
    /// Breakpoint XL. Default: 1200px
    /// </summary>
    [CssVariable("bs-breakpoint-xl")]
    public string? BreakpointXl { get; set; }

    /// <summary>
    /// Breakpoint XXL. Default: 1400px
    /// </summary>
    [CssVariable("bs-breakpoint-xxl")]
    public string? BreakpointXxl { get; set; }
}
