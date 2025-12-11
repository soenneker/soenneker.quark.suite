namespace Soenneker.Quark;

/// <summary>
/// Implementation of Quark configuration options
/// </summary>
public sealed class QuarkOptions
{
    /// <summary>
    /// Gets or sets whether to enable debug mode
    /// </summary>
    public bool Debug { get; set; }

    /// <summary>
    /// Gets or sets whether to automatically load Bootstrap
    /// </summary>
    public bool AutomaticBootstrapLoading { get; set; } = true;

    /// <summary>
    /// Gets or sets whether to use CDN for Bootstrap resources. If false, local paths will be used.
    /// </summary>
    public bool BootstrapUseCdn { get; set; } = true;

    /// <summary>
    /// Gets or sets whether to automatically load Font Awesome
    /// </summary>
    public bool AutomaticFontAwesomeLoading { get; set; } = true;

    /// <summary>
    /// Gets or sets whether to always render components and disable render optimizations
    /// </summary>
    public bool AlwaysRender { get; set; }

    /// <summary>
    /// Gets or sets whether to use CDN for CodeEditor resources. If false, local paths will be used.
    /// </summary>
    public bool CodeEditorUseCdn { get; set; } = true;
}
