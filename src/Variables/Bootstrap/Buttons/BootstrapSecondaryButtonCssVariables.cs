namespace Soenneker.Quark;

/// <summary>
/// Bootstrap secondary button CSS variables
/// </summary>
public sealed class BootstrapSecondaryButtonCssVariables : BootstrapBaseButtonCssVariables
{
	/// <summary>
	/// Gets the CSS selector for the secondary button component.
	/// </summary>
	/// <returns>The CSS selector string.</returns>
    public override string GetSelector()
    {
        return ".btn-secondary";
    }
}
