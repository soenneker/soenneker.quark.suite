namespace Soenneker.Quark;

/// <summary>
/// Content container for navigation menu dropdown content.
/// </summary>
public interface INavigationMenuContent : IElement
{
    /// <summary>
    /// Gets or sets the optional motion token used for animation data attributes.
    /// </summary>
    string? Motion { get; set; }

    /// <summary>
    /// Gets or sets whether the dropdown content remains mounted while closed.
    /// </summary>
    bool ForceMount { get; set; }
}
