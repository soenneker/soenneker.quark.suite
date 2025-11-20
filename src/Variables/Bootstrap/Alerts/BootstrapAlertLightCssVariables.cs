namespace Soenneker.Quark;

/// <summary>
/// CSS variables for Bootstrap light alert component.
/// </summary>
public sealed class BootstrapAlertLightCssVariables : BootstrapBaseAlertCssVariables
{
	/// <summary>
	/// Gets the CSS selector for the light alert component.
	/// </summary>
	/// <returns>The CSS selector string.</returns>
    public override string GetSelector()
    {
        return ".alert-light";
    }
}


