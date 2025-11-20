namespace Soenneker.Quark;

/// <summary>
/// Bootstrap danger button CSS variables
/// </summary>
public sealed class BootstrapDangerButtonCssVariables : BootstrapBaseButtonCssVariables
{
	/// <summary>
	/// Gets the CSS selector for the danger button component.
	/// </summary>
	/// <returns>The CSS selector string.</returns>
    public override string GetSelector()
    {
        return ".btn-danger";
    }
}


