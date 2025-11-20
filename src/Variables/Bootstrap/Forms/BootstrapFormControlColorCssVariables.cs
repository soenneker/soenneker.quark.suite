using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

/// <summary>
/// Variables for .form-control-color
/// </summary>
public class BootstrapFormControlColorCssVariables : IBootstrapCssVariableGroup
{
	/// <summary>
	/// Form control color width. Default: 3rem
	/// </summary>
	public string? Width { get; set; }

	/// <summary>
	/// Form control color height. Default: calc(1.5em + 0.75rem + calc(var(--bs-border-width) * 2))
	/// </summary>
	public string? Height { get; set; }

	/// <summary>
	/// Form control color padding. Default: 0.375rem
	/// </summary>
	public string? Padding { get; set; }

	/// <summary>
	/// Form control color border radius. Default: var(--bs-border-radius)
	/// </summary>
	public string? BorderRadius { get; set; }

	/// <summary>
	/// Form control color small height. Default: calc(1.5em + 0.5rem + calc(var(--bs-border-width) * 2))
	/// </summary>
	public string? SmHeight { get; set; }

	/// <summary>
	/// Form control color large height. Default: calc(1.5em + 1rem + calc(var(--bs-border-width) * 2))
	/// </summary>
	public string? LgHeight { get; set; }

    public string GetSelector()
    {
        return ".form-control-color";
    }

    public IEnumerable<(string CssPropertyName, string Value)> GetCssVariables()
    {
        if (Width.HasContent())
            yield return ("--bs-form-control-color-width", Width);
        if (Height.HasContent())
            yield return ("--bs-form-control-color-height", Height);
        if (Padding.HasContent())
            yield return ("--bs-form-control-color-padding", Padding);
        if (BorderRadius.HasContent())
            yield return ("--bs-form-control-color-border-radius", BorderRadius);
        if (SmHeight.HasContent())
            yield return ("--bs-form-control-color-sm-height", SmHeight);
        if (LgHeight.HasContent())
            yield return ("--bs-form-control-color-lg-height", LgHeight);
    }
}
