namespace Soenneker.Quark;

/// <summary>
/// Variables for .form-control
/// </summary>
public sealed class BootstrapFormControlCssVariables : BootstrapBaseFormControlCssVariables
{
	/// <summary>
	/// Gets the CSS selector for the form control component.
	/// </summary>
	/// <returns>The CSS selector string.</returns>
    public override string GetSelector()
    {
        return ".form-control";
    }
}