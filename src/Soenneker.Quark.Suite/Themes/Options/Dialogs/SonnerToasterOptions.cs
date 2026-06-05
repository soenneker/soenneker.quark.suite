namespace Soenneker.Quark;

/// <summary>
/// Represents the sonner toaster options.
/// </summary>
public sealed class SonnerToasterOptions : ComponentOptions
{
    public SonnerToasterOptions()
    {
        Selector = "[data-sonner-toaster]";
    }

    /// <summary>
    /// Gets or sets default position.
    /// </summary>
    public SonnerPosition? DefaultPosition { get; set; }

    /// <summary>
    /// Gets or sets default duration.
    /// </summary>
    public int? DefaultDuration { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether close button.
    /// </summary>
    public bool? CloseButton { get; set; }
}
