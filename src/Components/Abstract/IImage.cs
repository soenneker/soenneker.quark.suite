using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Soenneker.Quark;

/// <summary>
/// Represents an image component with support for responsive images and lazy loading.
/// </summary>
public interface IImage : IElement
{
    /// <summary>
    /// Gets or sets the absolute or relative URL of the image.
    /// </summary>
    string? Source { get; set; }

    /// <summary>
    /// Gets or sets the alternate text for the image.
    /// </summary>
    string? Alt { get; set; }

    /// <summary>
    /// Gets or sets whether the image should take up the whole width (fluid).
    /// </summary>
    bool Fluid { get; set; }

    /// <summary>
    /// Gets or sets whether to defer loading the image until it reaches a calculated distance from the viewport.
    /// </summary>
    bool Lazy { get; set; }

    /// <summary>
    /// Gets or sets the loading attribute for the image.
    /// </summary>
    string? Loading { get; set; }

    /// <summary>
    /// Gets or sets the decoding attribute for the image.
    /// </summary>
    string? Decoding { get; set; }

    /// <summary>
    /// Gets or sets the fetchpriority attribute for the image.
    /// </summary>
    string? FetchPriority { get; set; }

    /// <summary>
    /// Gets or sets the sizes attribute for responsive images.
    /// </summary>
    string? Sizes { get; set; }

    /// <summary>
    /// Gets or sets the srcset attribute for responsive images.
    /// </summary>
    string? SrcSet { get; set; }

    /// <summary>
    /// Gets or sets the crossorigin attribute for the image.
    /// </summary>
    string? CrossOrigin { get; set; }

    /// <summary>
    /// Gets or sets the referrerpolicy attribute for the image.
    /// </summary>
    string? ReferrerPolicy { get; set; }

    /// <summary>
    /// Gets or sets the usemap attribute for image maps.
    /// </summary>
    string? UseMap { get; set; }

    /// <summary>
    /// Gets or sets whether the image is a server-side image map.
    /// </summary>
    bool IsMap { get; set; }

    /// <summary>
    /// Gets or sets the longdesc attribute for the image.
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