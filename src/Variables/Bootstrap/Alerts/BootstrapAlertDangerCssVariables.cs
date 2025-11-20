namespace Soenneker.Quark;

/// <summary>
/// CSS variables for Bootstrap danger alert component.
/// </summary>
public sealed class BootstrapAlertDangerCssVariables : BootstrapBaseAlertCssVariables
{
	/// <summary>
	/// Gets the CSS selector for the danger alert component.
	/// </summary>
	/// <returns>The CSS selector string.</returns>
    public override string GetSelector()
    {
        return ".alert-danger";
    }
}