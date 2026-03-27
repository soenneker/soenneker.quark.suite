namespace Soenneker.Quark;

public sealed class SonnerToasterOptions : ComponentOptions
{
    public SonnerToasterOptions()
    {
        Selector = "[data-sonner-toaster]";
    }

    public SonnerPosition? DefaultPosition { get; set; }

    public int? DefaultDuration { get; set; }

    public bool? CloseButton { get; set; }
}
