
namespace Soenneker.Quark;

/// <summary>
/// Represents the text input options.
/// </summary>
public sealed class TextInputOptions : ComponentOptions
{
    public TextInputOptions()
    {
        Selector = "[data-slot='input']";
    }
}
