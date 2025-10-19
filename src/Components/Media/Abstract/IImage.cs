using Microsoft.AspNetCore.Components;
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
    /// Gets or sets the callback invoked when the image loads successfully.
    /// </summary>
    EventCallback<ProgressEventArgs> OnLoad { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when the image fails to load.
    /// </summary>
    EventCallback<ErrorEventArgs> OnError { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when the image starts loading.
    /// </summary>
    EventCallback<ProgressEventArgs> OnLoadStart { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when the image loading is aborted.
    /// </summary>
    EventCallback<ProgressEventArgs> OnAbort { get; set; }
}

