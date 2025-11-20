namespace Soenneker.Quark;

/// <summary>
/// Base badge selector.
/// </summary>
public sealed class BootstrapBadgeCssVariables : BootstrapBaseBadgeCssVariables
{
	/// <summary>
	/// Gets the CSS selector for the base badge component.
	/// </summary>
	/// <returns>The CSS selector string.</returns>
    public override string GetSelector()
    {
        return ".badge";
    }
}