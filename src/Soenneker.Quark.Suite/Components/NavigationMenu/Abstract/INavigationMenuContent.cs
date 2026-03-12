namespace Soenneker.Quark;

/// <summary>
/// Content container for navigation menu dropdown content.
/// </summary>
public interface INavigationMenuContent : IElement
{
    /// <summary>
    /// Gets or sets whether the content is currently open.
    /// </summary>
    bool Open { get; set; }

    /// <summary>
    /// Gets or sets whether automatic open behavior from hover/focus is disabled.
    /// </summary>
    bool DisableAutoOpen { get; set; }

    /// <summary>
    /// Gets or sets the optional motion token used for animation data attributes.
    /// </summary>
    string? Motion { get; set; }
}
