namespace Soenneker.Quark;

/// <summary>
/// CSS variables for Bootstrap dark alert component.
/// </summary>
public sealed class BootstrapAlertDarkCssVariables : BootstrapBaseAlertCssVariables
{
	/// <summary>
	/// Gets the CSS selector for the dark alert component.
	/// </summary>
	/// <returns>The CSS selector string.</returns>
    public override string GetSelector()
    {
        return ".alert-dark";
    }
}


