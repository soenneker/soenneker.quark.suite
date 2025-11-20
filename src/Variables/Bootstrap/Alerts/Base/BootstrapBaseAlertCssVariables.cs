using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

/// <summary>
/// Base CSS variables for Bootstrap alerts.
/// </summary>
public abstract class BootstrapBaseAlertCssVariables : IBootstrapCssVariableGroup
{
	/// <summary>
	/// Gets or sets the CSS variable value for the alert text color.
	/// </summary>
	public string? Color { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the alert background color.
	/// </summary>
	public string? Background { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the alert border color.
	/// </summary>
	public string? BorderColor { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the alert border.
	/// </summary>
	public string? Border { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the alert border radius.
	/// </summary>
	public string? BorderRadius { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the alert link color.
	/// </summary>
	public string? LinkColor { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the alert horizontal padding.
	/// </summary>
	public string? PaddingX { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the alert vertical padding.
	/// </summary>
	public string? PaddingY { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the alert bottom margin.
	/// </summary>
	public string? MarginBottom { get; set; }

	/// <summary>
	/// Gets the CSS selector for the alert component.
	/// </summary>
	/// <returns>The CSS selector string.</returns>
    public virtual string GetSelector()
    {
        return ":root";
    }

	/// <summary>
	/// Gets the collection of CSS variables for the alert component.
	/// </summary>
	/// <returns>An enumerable collection of CSS property name and value tuples.</returns>
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


