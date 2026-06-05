namespace Soenneker.Quark;

/// <summary>
/// Represents the alert action options.
/// </summary>
public sealed class AlertActionOptions : ComponentOptions
{
    public AlertActionOptions()
    {
        Selector = "[data-slot='alert-action']";
    }
}
