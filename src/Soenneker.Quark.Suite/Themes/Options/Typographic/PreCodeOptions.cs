
namespace Soenneker.Quark;

/// <summary>
/// Configuration options for preformatted code component styling.
/// </summary>
public sealed class PreCodeOptions : ComponentOptions
{
    public PreCodeOptions()
    {
        Selector = "[data-slot='pre-code']";
    }
}
