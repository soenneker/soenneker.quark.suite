namespace Soenneker.Quark;

/// <summary>
/// CSS variables for Bootstrap danger badge component.
/// </summary>
public sealed class BootstrapBadgeDangerCssVariables : BootstrapBaseBadgeCssVariables
{
	/// <summary>
	/// Gets the CSS selector for the danger badge component.
	/// </summary>
	/// <returns>The CSS selector string.</returns>
    public override string GetSelector()
    {
        return ".text-bg-danger";
    }
}


