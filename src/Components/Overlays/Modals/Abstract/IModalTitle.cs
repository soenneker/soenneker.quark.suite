namespace Soenneker.Quark;

/// <summary>
/// Represents the title heading within a modal header.
/// </summary>
public interface IModalTitle : IElement
{
    /// <summary>
    /// Gets or sets the scale/level of the heading.
    /// </summary>
    CssValue<ScaleBuilder>? Scale { get; set; }
}

