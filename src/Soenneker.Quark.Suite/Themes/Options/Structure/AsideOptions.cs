
namespace Soenneker.Quark;

/// <summary>
/// Represents the aside options.
/// </summary>
public sealed class AsideOptions : ComponentOptions
{
    public AsideOptions()
    {
        Selector = "[data-slot='aside']";
    }
}
