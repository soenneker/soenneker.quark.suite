using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

/// <summary>
/// Variables for .form-control-plaintext
/// </summary>
public sealed class BootstrapFormControlPlaintextCssVariables : IBootstrapCssVariableGroup
{
	/// <summary>
	/// Form control plaintext padding. Default: 0.375rem 0
	/// </summary>
	public string? Padding { get; set; }

	/// <summary>
	/// Form control plaintext line height. Default: 1.5
	/// </summary>
	public string? LineHeight { get; set; }

	/// <summary>
	/// Form control plaintext color. Default: var(--bs-body-color)
	/// </summary>
	public string? Color { get; set; }

	/// <summary>
	/// Form control plaintext background color. Default: transparent
	/// </summary>
	public string? Background { get; set; }

	/// <summary>
	/// Form control plaintext border. Default: solid transparent
	/// </summary>
	public string? Border { get; set; }

	/// <summary>
	/// Form control plaintext border width. Default: var(--bs-border-width) 0
	/// </summary>
	public string? BorderWidth { get; set; }

	/// <summary>
	/// Gets the CSS selector for the plaintext form control component.
	/// </summary>
	/// <returns>The CSS selector string.</returns>
    public string GetSelector()
    {
        return ".form-control-plaintext";
    }

	/// <summary>
	/// Gets the collection of CSS variables for the plaintext form control component.
	/// </summary>
	/// <returns>An enumerable collection of CSS property name and value tuples.</returns>
    public IEnumerable<(string CssPropertyName, string Value)> GetCssVariables()
    {
        if (Padding.HasContent())
            yield return ("--bs-form-control-plaintext-padding", Padding);
        if (LineHeight.HasContent())
            yield return ("--bs-form-control-plaintext-line-height", LineHeight);
        if (Color.HasContent())
            yield return ("--bs-form-control-plaintext-color", Color);
        if (Background.HasContent())
            yield return ("--bs-form-control-plaintext-bg", Background);
        if (Border.HasContent())
            yield return ("--bs-form-control-plaintext-border", Border);
        if (BorderWidth.HasContent())
            yield return ("--bs-form-control-plaintext-border-width", BorderWidth);
    }
}
