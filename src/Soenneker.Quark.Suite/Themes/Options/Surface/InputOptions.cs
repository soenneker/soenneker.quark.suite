
namespace Soenneker.Quark;

/// <summary>
/// Represents the input options.
/// </summary>
public sealed class InputOptions : ComponentOptions
{
    public InputOptions()
    {
        Selector = "[data-slot='input']";
    }
}
