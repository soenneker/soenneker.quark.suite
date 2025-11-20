namespace Soenneker.Quark;

/// <summary>
/// Base selector for alerts.
/// </summary>
public class BootstrapAlertCssVariables : BootstrapBaseAlertCssVariables
{
    public override string GetSelector()
    {
        return ".alert";
    }
}


