namespace Soenneker.Quark;

/// <summary>
/// Variables for .form-control
/// </summary>
public sealed class BootstrapFormControlCssVariables : BootstrapBaseFormControlCssVariables
{
    public override string GetSelector()
    {
        return ".form-control";
    }
}