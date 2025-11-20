namespace Soenneker.Quark;

/// <summary>
/// CSS variables for Bootstrap warning alert component.
/// </summary>
public sealed class BootstrapAlertWarningCssVariables : BootstrapBaseAlertCssVariables
{
	/// <summary>
	/// Gets the CSS selector for the warning alert component.
	/// </summary>
	/// <returns>The CSS selector string.</returns>
    public override string GetSelector()
    {
        return ".alert-warning";
    }
}


