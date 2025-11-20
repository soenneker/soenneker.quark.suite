namespace Soenneker.Quark;

/// <summary>
/// Bootstrap dark button CSS variables
/// </summary>
public sealed class BootstrapDarkButtonCssVariables : BootstrapBaseButtonCssVariables
{
	/// <summary>
	/// Gets the CSS selector for the dark button component.
	/// </summary>
	/// <returns>The CSS selector string.</returns>
    public override string GetSelector()
    {
        return ".btn-dark";
    }
}


