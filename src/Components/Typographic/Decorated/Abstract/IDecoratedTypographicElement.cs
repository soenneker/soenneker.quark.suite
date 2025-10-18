namespace Soenneker.Quark;

/// <summary>
/// Interface for decorated typographic elements that add visual styling to text.
/// Extends ITypographicElement with background, border, radius, shadow, padding, and opacity.
/// Used for badges, pills, labels, and similar decorated text elements.
/// </summary>
public interface IDecoratedTypographicElement : ITypographicElement
{
    // Inherits all typography properties from ITypographicElement
    // Plus the standard properties from IElement (BackgroundColor, Border, BorderColor, 
    // BorderRadius, BoxShadow, Padding, Opacity) are already available through IElement
}

