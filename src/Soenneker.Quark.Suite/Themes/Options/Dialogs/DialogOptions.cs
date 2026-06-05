
namespace Soenneker.Quark;

/// <summary>
/// Represents the dialog options.
/// </summary>
public sealed class DialogOptions : ComponentOptions
{
    public DialogOptions()
    {
        Selector = "[data-slot='dialog']";
    }
}
