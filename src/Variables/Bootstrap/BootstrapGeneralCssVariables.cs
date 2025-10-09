
namespace Soenneker.Quark;

/// <summary>
/// Bootstrap's general CSS variables
/// </summary>
[CssSelector]
public sealed class BootstrapGeneralCssVariables
{
    /// <summary>
    /// Gradient. Default: linear-gradient(180deg, rgba(255, 255, 255, 0.15), rgba(255, 255, 255, 0))
    /// </summary>
    [CssVariable("bs-gradient")]
    public string? Gradient { get; set; }
}
