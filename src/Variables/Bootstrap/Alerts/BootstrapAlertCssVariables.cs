namespace Soenneker.Quark;

/// <summary>
/// Base selector for alerts.
/// </summary>
public sealed class BootstrapAlertCssVariables : BootstrapBaseAlertCssVariables
{
	/// <summary>
	/// Gets the CSS selector for the base alert component.
	/// </summary>
	/// <returns>The CSS selector string.</returns>
    public override string GetSelector()
    {
        return ".alert";
    }
}


