namespace Soenneker.Quark;

[CssSelector(".breadcrumb-item + .breadcrumb-item::before")]
public class BootstrapBreadcrumbItemCssVariables
{
	[CssVariable("bs-breadcrumb-divider")]
	public string? Divider { get; set; }
}