namespace Soenneker.Quark;

/// <summary>
/// CSS variables for Bootstrap info badge component.
/// </summary>
public sealed class BootstrapBadgeInfoCssVariables : BootstrapBaseBadgeCssVariables
{
	/// <summary>
	/// Gets the CSS selector for the info badge component.
	/// </summary>
	/// <returns>The CSS selector string.</returns>
    public override string GetSelector()
    {
        return ".text-bg-info";
    }
}


