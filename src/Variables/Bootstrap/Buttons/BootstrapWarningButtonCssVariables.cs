namespace Soenneker.Quark;

/// <summary>
/// Bootstrap warning button CSS variables
/// </summary>
public sealed class BootstrapWarningButtonCssVariables : BootstrapBaseButtonCssVariables
{
	/// <summary>
	/// Gets the CSS selector for the warning button component.
	/// </summary>
	/// <returns>The CSS selector string.</returns>
    public override string GetSelector()
    {
        return ".btn-warning";
    }
}


