namespace Soenneker.Quark;

/// <summary>
/// CSS variables for Bootstrap success alert component.
/// </summary>
public sealed class BootstrapAlertSuccessCssVariables : BootstrapBaseAlertCssVariables
{
	/// <summary>
	/// Gets the CSS selector for the success alert component.
	/// </summary>
	/// <returns>The CSS selector string.</returns>
    public override string GetSelector()
    {
        return ".alert-success";
    }
}


