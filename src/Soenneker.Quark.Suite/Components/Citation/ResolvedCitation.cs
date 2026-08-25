using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>
/// Represents the resolved citation.
/// </summary>
public sealed class ResolvedCitation
{
    /// <summary>
    /// Gets or sets url.
    /// </summary>
    public string Url { get; init; } = "";

    /// <summary>
    /// Gets or sets title.
    /// </summary>
    public RenderFragment? Title { get; init; }

    /// <summary>
    /// Gets or sets description.
    /// </summary>
    public RenderFragment? Description { get; init; }

    /// <summary>
    /// Gets or sets site name.
    /// </summary>
    public string SiteName { get; init; } = "Source";

    /// <summary>
    /// Gets or sets favicon src.
    /// </summary>
    public string FaviconSrc { get; init; } = "";
}
