using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Soenneker.Quark;

/// <summary>
/// Represents an iframe component for embedding external content.
/// </summary>
public interface IIFrame : IComponent
{
    /// <summary>
    /// Gets or sets the URL of the page to embed.
    /// </summary>
    string? Source { get; set; }

    /// <summary>
    /// Gets or sets the name of the iframe.
    /// </summary>
    string? Name { get; set; }

    /// <summary>
    /// Gets or sets the sandbox attribute for security restrictions.
    /// </summary>
    string? Sandbox { get; set; }

    /// <summary>
    /// Gets or sets the allow attribute for feature policy.
    /// </summary>
    string? Allow { get; set; }

    /// <summary>
    /// Gets or sets the loading attribute (lazy or eager).
    /// </summary>
    string? Loading { get; set; }

    /// <summary>
    /// Gets or sets whether to defer loading the iframe until it reaches a calculated distance from the viewport.
    /// </summary>
    bool Lazy { get; set; }

    /// <summary>
    /// Gets or sets the referrerpolicy attribute for the iframe.
    /// </summary>
    string? ReferrerPolicy { get; set; }

    /// <summary>
    /// Gets or sets whether to allow fullscreen.
    /// </summary>
    bool AllowFullscreen { get; set; }

    /// <summary>
    /// Gets or sets whether to allow payment request.
    /// </summary>
    bool AllowPaymentRequest { get; set; }

    /// <summary>
    /// Gets or sets the srcdoc attribute for inline HTML content.
    /// </summary>
    string? SrcDoc { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when the iframe loads successfully.
    /// </summary>
    EventCallback<ProgressEventArgs> OnLoad { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when the iframe fails to load.
    /// </summary>
    EventCallback<ErrorEventArgs> OnError { get; set; }
}

