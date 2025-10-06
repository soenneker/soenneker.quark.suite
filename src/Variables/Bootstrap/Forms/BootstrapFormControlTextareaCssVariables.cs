namespace Soenneker.Quark;

/// <summary>
/// Variables for textarea.form-control
/// </summary>
[CssSelector("textarea.form-control")]
public class BootstrapFormControlTextareaCssVariables
{
	/// <summary>
	/// Textarea form control min height. Default: calc(1.5em + 0.75rem + calc(var(--bs-border-width) * 2))
	/// </summary>
	[CssVariable("bs-textarea-form-control-min-height")]
	public string? MinHeight { get; set; }

	/// <summary>
	/// Textarea form control small min height. Default: calc(1.5em + 0.5rem + calc(var(--bs-border-width) * 2))
	/// </summary>
	[CssVariable("bs-textarea-form-control-sm-min-height")]
	public string? SmMinHeight { get; set; }

	/// <summary>
	/// Textarea form control large min height. Default: calc(1.5em + 1rem + calc(var(--bs-border-width) * 2))
	/// </summary>
	[CssVariable("bs-textarea-form-control-lg-min-height")]
	public string? LgMinHeight { get; set; }
}
