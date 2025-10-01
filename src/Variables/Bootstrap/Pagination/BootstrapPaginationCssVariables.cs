namespace Soenneker.Quark;

/// <summary>
/// Variables for pagination.
/// </summary>
[CssSelector(".pagination")]
public class BootstrapPaginationCssVariables
{
	[CssVariable("bs-pagination-color")]
	public string? Color { get; set; }

	[CssVariable("bs-pagination-bg")]
	public string? Background { get; set; }

	[CssVariable("bs-pagination-border-color")]
	public string? BorderColor { get; set; }
}

