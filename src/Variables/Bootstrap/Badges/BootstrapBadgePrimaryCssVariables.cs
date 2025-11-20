namespace Soenneker.Quark;

/// <summary>
/// CSS variables for Bootstrap primary badge component.
/// </summary>
public sealed class BootstrapBadgePrimaryCssVariables : BootstrapBaseBadgeCssVariables
{
	/// <summary>
	/// Gets the CSS selector for the primary badge component.
	/// </summary>
	/// <returns>The CSS selector string.</returns>
    public override string GetSelector()
    {
        return ".text-bg-primary";
    }
}


