namespace Soenneker.Quark;

/// <summary>
/// CSS variables for Bootstrap success badge component.
/// </summary>
public sealed class BootstrapBadgeSuccessCssVariables : BootstrapBaseBadgeCssVariables
{
	/// <summary>
	/// Gets the CSS selector for the success badge component.
	/// </summary>
	/// <returns>The CSS selector string.</returns>
    public override string GetSelector()
    {
        return ".text-bg-success";
    }
}


