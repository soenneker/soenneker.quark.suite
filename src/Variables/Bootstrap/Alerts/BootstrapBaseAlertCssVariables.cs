using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

/// <summary>
/// Base CSS variables for Bootstrap alerts.
/// </summary>
public abstract class BootstrapBaseAlertCssVariables : IBootstrapCssVariableGroup
{
	public string? Color { get; set; }

	public string? Background { get; set; }

	public string? BorderColor { get; set; }

	public string? Border { get; set; }

	public string? BorderRadius { get; set; }

	public string? LinkColor { get; set; }

	public string? PaddingX { get; set; }

	public string? PaddingY { get; set; }

	public string? MarginBottom { get; set; }

    public virtual string GetSelector()
    {
        return ":root";
    }

    public virtual IEnumerable<(string CssPropertyName, string Value)> GetCssVariables()
    {
        if (Color.HasContent())
            yield return ("--bs-alert-color", Color);
        if (Background.HasContent())
            yield return ("--bs-alert-bg", Background);
        if (BorderColor.HasContent())
            yield return ("--bs-alert-border-color", BorderColor);
        if (Border.HasContent())
            yield return ("--bs-alert-border", Border);
        if (BorderRadius.HasContent())
            yield return ("--bs-alert-border-radius", BorderRadius);
        if (LinkColor.HasContent())
            yield return ("--bs-alert-link-color", LinkColor);
        if (PaddingX.HasContent())
            yield return ("--bs-alert-padding-x", PaddingX);
        if (PaddingY.HasContent())
            yield return ("--bs-alert-padding-y", PaddingY);
        if (MarginBottom.HasContent())
            yield return ("--bs-alert-margin-bottom", MarginBottom);
    }
}


