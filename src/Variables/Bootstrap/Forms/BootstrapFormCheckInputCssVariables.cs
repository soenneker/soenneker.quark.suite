using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

/// <summary>
/// Variables for .form-check inputs and switches.
/// </summary>
public class BootstrapFormCheckInputCssVariables : IBootstrapCssVariableGroup
{
	public string? Background { get; set; }

	public string? BorderColor { get; set; }

	public string? BackgroundImage { get; set; }

	public string? CheckedBackgroundImage { get; set; }

	public string? CheckedColor { get; set; }

    public string GetSelector()
    {
        return ".form-check-input";
    }

    public IEnumerable<(string CssPropertyName, string Value)> GetCssVariables()
    {
        if (Background.HasContent())
            yield return ("--bs-form-check-bg", Background);
        if (BorderColor.HasContent())
            yield return ("--bs-form-check-border-color", BorderColor);
        if (BackgroundImage.HasContent())
            yield return ("--bs-form-check-bg-image", BackgroundImage);
        if (CheckedBackgroundImage.HasContent())
            yield return ("--bs-form-check-checked-bg-image", CheckedBackgroundImage);
        if (CheckedColor.HasContent())
            yield return ("--bs-form-check-checked-color", CheckedColor);
    }
}

