namespace Soenneker.Quark;

/// <summary>
/// Represents the figcaption options.
/// </summary>
public sealed class FigcaptionOptions : ComponentOptions
{
    public FigcaptionOptions()
    {
        Selector = "[data-slot='figcaption']";
    }
}