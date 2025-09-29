namespace Soenneker.Quark.Variables.Bootstrap;

/// <summary>
/// Bootstrap's shadow CSS variables
/// </summary>
public sealed class BootstrapShadowsCssVariables
{
    /// <summary>
    /// Box shadow. Default: 0 0.5rem 1rem rgba(0, 0, 0, 0.15)
    /// </summary>
    [CssVariable("bs-box-shadow")]
    public string? BoxShadow { get; set; }

    /// <summary>
    /// Box shadow small. Default: 0 0.125rem 0.25rem rgba(0, 0, 0, 0.075)
    /// </summary>
    [CssVariable("bs-box-shadow-sm")]
    public string? BoxShadowSm { get; set; }

    /// <summary>
    /// Box shadow large. Default: 0 1rem 3rem rgba(0, 0, 0, 0.175)
    /// </summary>
    [CssVariable("bs-box-shadow-lg")]
    public string? BoxShadowLg { get; set; }

    /// <summary>
    /// Box shadow inset. Default: inset 0 1px 2px rgba(0, 0, 0, 0.075)
    /// </summary>
    [CssVariable("bs-box-shadow-inset")]
    public string? BoxShadowInset { get; set; }
}
