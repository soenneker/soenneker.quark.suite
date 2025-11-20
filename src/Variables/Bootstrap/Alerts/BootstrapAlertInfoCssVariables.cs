namespace Soenneker.Quark;

/// <summary>
/// CSS variables for Bootstrap info alert component.
/// </summary>
public sealed class BootstrapAlertInfoCssVariables : BootstrapBaseAlertCssVariables
{
	/// <summary>
	/// Gets the CSS selector for the info alert component.
	/// </summary>
	/// <returns>The CSS selector string.</returns>
    public override string GetSelector()
    {
        return ".alert-info";
    }
}


