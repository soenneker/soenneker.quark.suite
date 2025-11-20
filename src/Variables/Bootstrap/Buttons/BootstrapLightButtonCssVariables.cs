namespace Soenneker.Quark;

/// <summary>
/// Bootstrap light button CSS variables
/// </summary>
public sealed class BootstrapLightButtonCssVariables : BootstrapBaseButtonCssVariables
{
	/// <summary>
	/// Gets the CSS selector for the light button component.
	/// </summary>
	/// <returns>The CSS selector string.</returns>
    public override string GetSelector()
    {
        return ".btn-light";
    }
}


