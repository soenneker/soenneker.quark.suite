namespace Soenneker.Quark;

/// <summary>
/// Variables for offcanvas.
/// </summary>
[CssSelector(".offcanvas")]
public class BootstrapOffcanvasCssVariables
{
	[CssVariable("bs-offcanvas-bg")]
	public string? Background { get; set; }

	[CssVariable("bs-offcanvas-color")]
	public string? Color { get; set; }
}
