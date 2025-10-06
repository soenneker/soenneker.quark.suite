namespace Soenneker.Quark;

/// <summary>
/// Variables for .form-control-plaintext
/// </summary>
[CssSelector(".form-control-plaintext")]
public class BootstrapFormControlPlaintextCssVariables
{
	/// <summary>
	/// Form control plaintext padding. Default: 0.375rem 0
	/// </summary>
	[CssVariable("bs-form-control-plaintext-padding")]
	public string? Padding { get; set; }

	/// <summary>
	/// Form control plaintext line height. Default: 1.5
	/// </summary>
	[CssVariable("bs-form-control-plaintext-line-height")]
	public string? LineHeight { get; set; }

	/// <summary>
	/// Form control plaintext color. Default: var(--bs-body-color)
	/// </summary>
	[CssVariable("bs-form-control-plaintext-color")]
	public string? Color { get; set; }

	/// <summary>
	/// Form control plaintext background color. Default: transparent
	/// </summary>
	[CssVariable("bs-form-control-plaintext-bg")]
	public string? Background { get; set; }

	/// <summary>
	/// Form control plaintext border. Default: solid transparent
	/// </summary>
	[CssVariable("bs-form-control-plaintext-border")]
	public string? Border { get; set; }

	/// <summary>
	/// Form control plaintext border width. Default: var(--bs-border-width) 0
	/// </summary>
	[CssVariable("bs-form-control-plaintext-border-width")]
	public string? BorderWidth { get; set; }
}
