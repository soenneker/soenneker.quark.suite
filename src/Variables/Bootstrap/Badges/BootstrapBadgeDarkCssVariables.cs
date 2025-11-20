namespace Soenneker.Quark;

/// <summary>
/// CSS variables for Bootstrap dark badge component.
/// </summary>
public sealed class BootstrapBadgeDarkCssVariables : BootstrapBaseBadgeCssVariables
{
	/// <summary>
	/// Gets the CSS selector for the dark badge component.
	/// </summary>
	/// <returns>The CSS selector string.</returns>
    public override string GetSelector()
    {
        return ".text-bg-dark";
    }
}