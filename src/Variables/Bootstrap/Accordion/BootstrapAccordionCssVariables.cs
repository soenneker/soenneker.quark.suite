using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

public class BootstrapAccordionCssVariables : IBootstrapCssVariableGroup
{
	public string? Color { get; set; }

	public string? Background { get; set; }

	public string? BorderColor { get; set; }

	public string GetSelector()
	{
		return ".accordion";
	}

	public IEnumerable<(string CssPropertyName, string Value)> GetCssVariables()
	{
		if (Color.HasContent())
			yield return ("--bs-accordion-color", Color);

		if (Background.HasContent())
			yield return ("--bs-accordion-bg", Background);

		if (BorderColor.HasContent())
			yield return ("--bs-accordion-border-color", BorderColor);
	}
}

