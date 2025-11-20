namespace Soenneker.Quark;

/// <summary>
/// Bootstrap primary button CSS variables
/// </summary>
public sealed class BootstrapPrimaryButtonCssVariables : BootstrapBaseButtonCssVariables
{
	/// <summary>
	/// Gets the CSS selector for the primary button component.
	/// </summary>
	/// <returns>The CSS selector string.</returns>
    public override string GetSelector()
    {
        return ".btn-primary";
    }
}
