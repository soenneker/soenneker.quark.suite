namespace Soenneker.Quark;

/// <summary>
/// Bootstrap base button selector variables
/// </summary>
public sealed class BootstrapButtonCssVariables : BootstrapBaseButtonCssVariables
{
	/// <summary>
	/// Gets the CSS selector for the base button component.
	/// </summary>
	/// <returns>The CSS selector string.</returns>
    public override string GetSelector()
    {
        return ".btn";
    }
}
