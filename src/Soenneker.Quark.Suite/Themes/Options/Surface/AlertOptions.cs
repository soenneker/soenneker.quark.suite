
namespace Soenneker.Quark;

/// <summary>
/// Represents the alert options.
/// </summary>
public sealed class AlertOptions : ComponentOptions
{
    public AlertOptions()
    {
        Selector = "[data-slot='alert']";
    }
}
