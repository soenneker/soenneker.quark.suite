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
    /// Gets or sets whether to automatically load Font Awesome
    /// </summary>
    public bool AutomaticFontAwesomeLoading { get; set; } = true;
}
