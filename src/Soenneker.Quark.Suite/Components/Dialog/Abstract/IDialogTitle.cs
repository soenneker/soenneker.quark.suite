namespace Soenneker.Quark;

/// <summary>
/// Represents the title heading within a dialog header.
/// </summary>
public interface IDialogTitle : IElement
{
    /// <summary>
    /// Gets or sets the scale/level of the heading.
    /// </summary>
    CssValue<ScaleBuilder>? Scale { get; set; }
}

