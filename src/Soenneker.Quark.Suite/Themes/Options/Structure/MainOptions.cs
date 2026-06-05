
namespace Soenneker.Quark;

/// <summary>
/// Represents the main options.
/// </summary>
public sealed class MainOptions : ComponentOptions
{
    public MainOptions()
    {
        Selector = "[data-slot='main']";
    }
}
