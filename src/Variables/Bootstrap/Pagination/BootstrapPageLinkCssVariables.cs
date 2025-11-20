using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

/// <summary>
/// CSS variables for Bootstrap page link component.
/// </summary>
public sealed class BootstrapPageLinkCssVariables : IBootstrapCssVariableGroup
{
	/// <summary>
	/// Gets or sets the CSS variable value for the page link hover text color.
	/// </summary>
	public string? HoverColor { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the page link hover background color.
	/// </summary>
	public string? HoverBackground { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the page link hover border color.
	/// </summary>
	public string? HoverBorderColor { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the page link active text color.
	/// </summary>
	public string? ActiveColor { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the page link active background color.
	/// </summary>
	public string? ActiveBackground { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the page link active border color.
	/// </summary>
	public string? ActiveBorderColor { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the page link disabled text color.
	/// </summary>
	public string? DisabledColor { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the page link disabled background color.
	/// </summary>
	public string? DisabledBackground { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the page link disabled border color.
	/// </summary>
	public string? DisabledBorderColor { get; set; }

	/// <summary>
	/// Gets the CSS selector for the page link component.
	/// </summary>
	/// <returns>The CSS selector string.</returns>
    public string GetSelector()
    {
        return ".page-link";
    }

	/// <summary>
	/// Gets the collection of CSS variables for the page link component.
	/// </summary>
	/// <returns>An enumerable collection of CSS property name and value tuples.</returns>
    public IEnumerable<(string CssPropertyName, string Value)> GetCssVariables()
    {
        if (HoverColor.HasContent())
            yield return ("--bs-pagination-hover-color", HoverColor);
        if (HoverBackground.HasContent())
            yield return ("--bs-pagination-hover-bg", HoverBackground);
        if (HoverBorderColor.HasContent())
            yield return ("--bs-pagination-hover-border-color", HoverBorderColor);
        if (ActiveColor.HasContent())
            yield return ("--bs-pagination-active-color", ActiveColor);
        if (ActiveBackground.HasContent())
            yield return ("--bs-pagination-active-bg", ActiveBackground);
        if (ActiveBorderColor.HasContent())
            yield return ("--bs-pagination-active-border-color", ActiveBorderColor);
        if (DisabledColor.HasContent())
            yield return ("--bs-pagination-disabled-color", DisabledColor);
        if (DisabledBackground.HasContent())
            yield return ("--bs-pagination-disabled-bg", DisabledBackground);
        if (DisabledBorderColor.HasContent())
            yield return ("--bs-pagination-disabled-border-color", DisabledBorderColor);
    }
}

