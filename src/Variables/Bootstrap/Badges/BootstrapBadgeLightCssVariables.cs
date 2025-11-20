namespace Soenneker.Quark;

/// <summary>
/// CSS variables for Bootstrap light badge component.
/// </summary>
public sealed class BootstrapBadgeLightCssVariables : BootstrapBaseBadgeCssVariables
{
	/// <summary>
	/// Gets the CSS selector for the light badge component.
	/// </summary>
	/// <returns>The CSS selector string.</returns>
    public override string GetSelector()
    {
        return ".text-bg-light";
    }
}


