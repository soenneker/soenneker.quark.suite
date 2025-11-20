namespace Soenneker.Quark;

/// <summary>
/// Bootstrap success button CSS variables
/// </summary>
public sealed class BootstrapSuccessButtonCssVariables : BootstrapBaseButtonCssVariables
{
	/// <summary>
	/// Gets the CSS selector for the success button component.
	/// </summary>
	/// <returns>The CSS selector string.</returns>
    public override string GetSelector()
    {
        return ".btn-success";
    }
}


