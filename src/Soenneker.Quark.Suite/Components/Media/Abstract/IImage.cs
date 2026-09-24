using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components.Web;

namespace Soenneker.Quark;

/// <summary>
/// Interface for the Image component
/// </summary>
public interface IImage : IComponent
{
    /// <summary>
    /// Gets or sets the URL of the image.
    /// </summary>
    string? Source { get; set; }

    /// <summary>
    /// Optional replacement for the final file extension in Source, with or without a leading dot.
    /// Appends an extension when the filename has none and preserves query strings and fragments.
    /// Null or blank values, data URLs, blob URLs, and URLs without a filename leave Source unchanged.
    /// Applies to previews and lightboxes; SrcSet is unchanged. This changes the URL, not the image format.
    /// </summary>
    string? Extension { get; set; }

    /// <summary>
    /// Generates width candidates by inserting -480, -960, and -1440 (or SrcSetWidths) before the
    /// resolved source extension. Assumes those files exist. Explicit SrcSet takes precedence.
    /// Set Sizes to describe the rendered width; automatic source sets default to 100vw.
    /// </summary>
    bool AutoSrcSet { get; set; }

    /// <summary>Widths used by AutoSrcSet. Defaults to 480, 960, and 1440. Each width must be positive.</summary>
    IReadOnlyList<int> SrcSetWidths { get; set; }

    /// <summary>
    /// Follows the Quark theme by inserting -dark before the resolved source extension in dark mode.
    /// With AutoSrcSet, generates names such as photo-dark-480.avif. Assumes the files exist.
    /// Requires AddQuarkThemeAsScoped or AddQuarkSuiteAsScoped. Explicit SrcSet is unchanged.
    /// </summary>
    bool AutoDark { get; set; }

    /// <summary>
    /// Gets or sets the alternative text for the image.
    /// </summary>
    string? Alt { get; set; }

    /// <summary>
    /// Gets or sets whether the image should be responsive (fluid).
    /// </summary>
    bool Fluid { get; set; }

    /// <summary>
    /// Gets or sets whether the image should be lazy-loaded.
    /// </summary>
    bool Lazy { get; set; }

    /// <summary>
    /// Gets or sets the loading strategy (lazy, eager).
    /// </summary>
    string? Loading { get; set; }

    /// <summary>
    /// Gets or sets the decoding strategy (async, sync, auto).
    /// </summary>
    string? Decoding { get; set; }

    /// <summary>
    /// Gets or sets the fetch priority (high, low, auto).
    /// </summary>
    string? FetchPriority { get; set; }

    /// <summary>
    /// Gets or sets the image sizes for responsive images.
    /// </summary>
    string? Sizes { get; set; }

    /// <summary>
    /// Gets or sets the source set for responsive images.
    /// </summary>
    string? SrcSet { get; set; }

    /// <summary>
    /// Gets or sets the CORS settings for the image.
    /// </summary>
    string? CrossOrigin { get; set; }

    /// <summary>
    /// Gets or sets the referrer policy for the image.
    /// </summary>
    string? ReferrerPolicy { get; set; }

    /// <summary>
    /// Gets or sets the image map to use.
    /// </summary>
    string? UseMap { get; set; }

    /// <summary>
    /// Gets or sets whether the image is a server-side image map.
    /// </summary>
    bool IsMap { get; set; }

    /// <summary>
    /// Gets or sets the URL of a detailed description of the image.
    /// </summary>
    string? LongDesc { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked When the image loads successfully.
    /// </summary>
    EventCallback<ProgressEventArgs> OnLoad { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked When the image fails to load.
    /// </summary>
    EventCallback<ErrorEventArgs> OnError { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked When the image starts loading.
    /// </summary>
    EventCallback<ProgressEventArgs> OnLoadStart { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked When the image loading is aborted.
    /// </summary>
    EventCallback<ProgressEventArgs> OnAbort { get; set; }
}

