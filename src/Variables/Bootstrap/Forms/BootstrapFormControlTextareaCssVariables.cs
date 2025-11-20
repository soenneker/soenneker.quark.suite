using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

/// <summary>
/// Variables for textarea.form-control
/// </summary>
public class BootstrapFormControlTextareaCssVariables : IBootstrapCssVariableGroup
{
	/// <summary>
	/// Textarea form control min height. Default: calc(1.5em + 0.75rem + calc(var(--bs-border-width) * 2))
	/// </summary>
	public string? MinHeight { get; set; }

	/// <summary>
	/// Textarea form control small min height. Default: calc(1.5em + 0.5rem + calc(var(--bs-border-width) * 2))
	/// </summary>
	public string? SmMinHeight { get; set; }

	/// <summary>
	/// Textarea form control large min height. Default: calc(1.5em + 1rem + calc(var(--bs-border-width) * 2))
	/// </summary>
	public string? LgMinHeight { get; set; }

    public string GetSelector()
    {
        return "textarea.form-control";
    }

    public IEnumerable<(string CssPropertyName, string Value)> GetCssVariables()
    {
        if (MinHeight.HasContent())
            yield return ("--bs-textarea-form-control-min-height", MinHeight);
        if (SmMinHeight.HasContent())
            yield return ("--bs-textarea-form-control-sm-min-height", SmMinHeight);
        if (LgMinHeight.HasContent())
            yield return ("--bs-textarea-form-control-lg-min-height", LgMinHeight);
    }
}
