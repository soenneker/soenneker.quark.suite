using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

/// <summary>
/// CSS variables for Bootstrap breadcrumb item component.
/// </summary>
public sealed class BootstrapBreadcrumbItemCssVariables : IBootstrapCssVariableGroup
{
	/// <summary>
	/// Gets or sets the CSS variable value for the breadcrumb item divider.
	/// </summary>
	public string? Divider { get; set; }

	/// <summary>
	/// Gets the CSS selector for the breadcrumb item component.
	/// </summary>
	/// <returns>The CSS selector string.</returns>
    public string GetSelector()
    {
        return ".breadcrumb-item + .breadcrumb-item::before";
    }

	/// <summary>
	/// Gets the collection of CSS variables for the breadcrumb item component.
	/// </summary>
	/// <returns>An enumerable collection of CSS property name and value tuples.</returns>
    public IEnumerable<(string CssPropertyName, string Value)> GetCssVariables()
    {
        if (Divider.HasContent())
            yield return ("--bs-breadcrumb-divider", Divider);
    }
}