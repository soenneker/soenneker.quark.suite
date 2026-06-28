namespace Soenneker.Quark;

/// <summary>
/// Represents the dialog description options.
/// </summary>
public sealed class DialogDescriptionOptions : ComponentOptions
{
    public DialogDescriptionOptions()
    {
        Selector = "[data-slot='dialog-description']";
    }
}
