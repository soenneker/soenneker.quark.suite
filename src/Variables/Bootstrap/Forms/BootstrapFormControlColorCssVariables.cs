namespace Soenneker.Quark;

/// <summary>
/// Variables for .form-control-color
/// </summary>
[CssSelector(".form-control-color")]
public class BootstrapFormControlColorCssVariables
{
	/// <summary>
	/// Form control color width. Default: 3rem
	/// </summary>
	[CssVariable("bs-form-control-color-width")]
	public string? Width { get; set; }

	/// <summary>
	/// Form control color height. Default: calc(1.5em + 0.75rem + calc(var(--bs-border-width) * 2))
	/// </summary>
	[CssVariable("bs-form-control-color-height")]
	public string? Height { get; set; }

	/// <summary>
	/// Form control color padding. Default: 0.375rem
	/// </summary>
	[CssVariable("bs-form-control-color-padding")]
	public string? Padding { get; set; }

	/// <summary>
	/// Form control color border radius. Default: var(--bs-border-radius)
	/// </summary>
	[CssVariable("bs-form-control-color-border-radius")]
	public string? BorderRadius { get; set; }

	/// <summary>
	/// Form control color small height. Default: calc(1.5em + 0.5rem + calc(var(--bs-border-width) * 2))
	/// </summary>
	[CssVariable("bs-form-control-color-sm-height")]
	public string? SmHeight { get; set; }

	/// <summary>
	/// Form control color large height. Default: calc(1.5em + 1rem + calc(var(--bs-border-width) * 2))
	/// </summary>
	[CssVariable("bs-form-control-color-lg-height")]
	public string? LgHeight { get; set; }
}
