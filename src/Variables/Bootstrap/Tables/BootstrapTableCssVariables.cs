using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

/// <summary>
/// Bootstrap's table CSS variables
/// </summary>
public sealed class BootstrapTableCssVariables : IBootstrapCssVariableGroup
{
	/// <summary>
	/// Table color type. Default: initial
	/// </summary>
	public string? ColorType { get; set; }

	/// <summary>
	/// Table background type. Default: initial
	/// </summary>
	public string? BgType { get; set; }

	/// <summary>
	/// Table color state. Default: initial
	/// </summary>
	public string? ColorState { get; set; }

	/// <summary>
	/// Table background state. Default: initial
	/// </summary>
	public string? BgState { get; set; }

	/// <summary>
	/// Table color. Default: var(--bs-emphasis-color)
	/// </summary>
	public string? Color { get; set; }

	/// <summary>
	/// Table background. Default: var(--bs-body-bg)
	/// </summary>
	public string? Background { get; set; }

	/// <summary>
	/// Table border color. Default: var(--bs-border-color)
	/// </summary>
	public string? BorderColor { get; set; }

	/// <summary>
	/// Table accent background. Default: transparent
	/// </summary>
	public string? AccentBg { get; set; }

	/// <summary>
	/// Table striped color. Default: var(--bs-emphasis-color)
	/// </summary>
	public string? StripedColor { get; set; }

	/// <summary>
	/// Table striped background. Default: rgba(var(--bs-emphasis-color-rgb), 0.05)
	/// </summary>
	public string? StripedBg { get; set; }

	/// <summary>
	/// Table active color. Default: var(--bs-emphasis-color)
	/// </summary>
	public string? ActiveColor { get; set; }

	/// <summary>
	/// Table active background. Default: rgba(var(--bs-emphasis-color-rgb), 0.1)
	/// </summary>
	public string? ActiveBg { get; set; }

	/// <summary>
	/// Table hover color. Default: var(--bs-emphasis-color)
	/// </summary>
	public string? HoverColor { get; set; }

	/// <summary>
	/// Table hover background. Default: rgba(var(--bs-emphasis-color-rgb), 0.075)
	/// </summary>
	public string? HoverBg { get; set; }

    public string GetSelector()
    {
        return ".table";
    }

    public IEnumerable<(string CssPropertyName, string Value)> GetCssVariables()
    {
        if (ColorType.HasContent())
            yield return ("--bs-table-color-type", ColorType);
        if (BgType.HasContent())
            yield return ("--bs-table-bg-type", BgType);
        if (ColorState.HasContent())
            yield return ("--bs-table-color-state", ColorState);
        if (BgState.HasContent())
            yield return ("--bs-table-bg-state", BgState);
        if (Color.HasContent())
            yield return ("--bs-table-color", Color);
        if (Background.HasContent())
            yield return ("--bs-table-bg", Background);
        if (BorderColor.HasContent())
            yield return ("--bs-table-border-color", BorderColor);
        if (AccentBg.HasContent())
            yield return ("--bs-table-accent-bg", AccentBg);
        if (StripedColor.HasContent())
            yield return ("--bs-table-striped-color", StripedColor);
        if (StripedBg.HasContent())
            yield return ("--bs-table-striped-bg", StripedBg);
        if (ActiveColor.HasContent())
            yield return ("--bs-table-active-color", ActiveColor);
        if (ActiveBg.HasContent())
            yield return ("--bs-table-active-bg", ActiveBg);
        if (HoverColor.HasContent())
            yield return ("--bs-table-hover-color", HoverColor);
        if (HoverBg.HasContent())
            yield return ("--bs-table-hover-bg", HoverBg);
    }
}

