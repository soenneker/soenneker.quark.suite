namespace Soenneker.Quark;

/// <summary>
/// CSS variables for Bootstrap primary alert component.
/// </summary>
public sealed class BootstrapAlertPrimaryCssVariables : BootstrapBaseAlertCssVariables
{
	/// <summary>
	/// Gets the CSS selector for the primary alert component.
	/// </summary>
	/// <returns>The CSS selector string.</returns>
    public override string GetSelector()
    {
        return ".alert-primary";
    }
}


