namespace Soenneker.Quark;

/// <summary>
/// CSS variables for Bootstrap warning badge component.
/// </summary>
public sealed class BootstrapBadgeWarningCssVariables : BootstrapBaseBadgeCssVariables
{
	/// <summary>
	/// Gets the CSS selector for the warning badge component.
	/// </summary>
	/// <returns>The CSS selector string.</returns>
    public override string GetSelector()
    {
        return ".text-bg-warning";
    }
}


