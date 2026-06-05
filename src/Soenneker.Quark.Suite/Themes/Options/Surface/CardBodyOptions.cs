
namespace Soenneker.Quark;

/// <summary>
/// Represents the card body options.
/// </summary>
public sealed class CardBodyOptions : ComponentOptions
{
    public CardBodyOptions()
    {
        Selector = "[data-slot='card-content']";
    }
}
