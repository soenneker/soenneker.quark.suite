namespace Soenneker.Quark;

/// <summary>
/// CSS variables for Bootstrap secondary alert component.
/// </summary>
public sealed class BootstrapAlertSecondaryCssVariables : BootstrapBaseAlertCssVariables
{
	/// <summary>
	/// Gets the CSS selector for the secondary alert component.
	/// </summary>
	/// <returns>The CSS selector string.</returns>
    public override string GetSelector()
    {
        return ".alert-secondary";
    }
}


