namespace Soenneker.Quark;

/// <summary>
/// Bootstrap outline warning button CSS variables
/// </summary>
public sealed class BootstrapOutlineWarningButtonCssVariables : BootstrapBaseButtonCssVariables
{
	/// <summary>
	/// Gets the CSS selector for the outline warning button component.
	/// </summary>
	/// <returns>The CSS selector string.</returns>
    public override string GetSelector()
    {
        return ".btn-outline-warning";
    }
}