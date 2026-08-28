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
    /// Gets or sets whether to automatically load framework resources
    /// </summary>
    public bool AutomaticFrameworkResourceLoading { get; set; } = true;

    /// <summary>
    /// Gets or sets whether to use CDN for framework resources. If false, local paths will be used.
    /// </summary>
    public bool FrameworkUseCdn { get; set; } = true;

    /// <summary>
    /// Gets or sets whether components render normally instead of using render suppression.
    /// </summary>
    /// <remarks>
    /// The default is <see langword="false"/>. Quark fingerprints incoming parameters, forces a render whenever
    /// that fingerprint changes, and automatically invalidates rendering for component event handlers. As with
    /// Blazor component parameters generally, replace mutable parameter objects when their contents change. Set
    /// this option to <see langword="true"/> when an application intentionally relies on in-place mutation.
    /// Attribute dictionaries reuse their backing buffers regardless of this setting.
    /// </remarks>
    public bool AlwaysRender { get; set; }

    /// <summary>
    /// Gets or sets whether to use CDN for CodeEditor CSS. Monaco modules and workers are served from bundled static assets.
    /// </summary>
    public bool CodeEditorUseCdn { get; set; }
}
