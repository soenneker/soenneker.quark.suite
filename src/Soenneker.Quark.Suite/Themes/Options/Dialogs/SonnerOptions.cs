namespace Soenneker.Quark;

/// <summary>
/// Represents the sonner options.
/// </summary>
public sealed class SonnerOptions : ComponentOptions
{
    public SonnerOptions()
    {
        Selector = "[data-sonner-toast]";
    }
}
