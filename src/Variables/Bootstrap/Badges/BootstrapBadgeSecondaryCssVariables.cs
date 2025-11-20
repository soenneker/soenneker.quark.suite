namespace Soenneker.Quark;

/// <summary>
/// CSS variables for Bootstrap secondary badge component.
/// </summary>
public sealed class BootstrapBadgeSecondaryCssVariables : BootstrapBaseBadgeCssVariables
{
	/// <summary>
	/// Gets the CSS selector for the secondary badge component.
	/// </summary>
	/// <returns>The CSS selector string.</returns>
    public override string GetSelector()
    {
        return ".text-bg-secondary";
    }
}