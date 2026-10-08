namespace Soenneker.Quark;

/// <summary>
/// Interface for the Anchor component
/// </summary>
public interface IAnchor : IElement
{
    /// <summary>
    /// Gets or sets the download filename. An empty string lets the browser choose the filename.
    /// </summary>
    string? Download { get; set; }

    /// <summary>
    /// Gets or sets the HTML rel attribute.
    /// </summary>
    string? Rel { get; set; }

    /// <summary>
    /// Gets or sets the HTML hreflang attribute.
    /// </summary>
    string? HrefLang { get; set; }

    /// <summary>
    /// Gets or sets the HTML ping attribute.
    /// </summary>
    string? Ping { get; set; }

    /// <summary>
    /// Gets or sets the HTML referrerpolicy attribute.
    /// </summary>
    string? ReferrerPolicy { get; set; }

    /// <summary>
    /// Gets or sets the HTML type attribute.
    /// </summary>
    string? MediaType { get; set; }


    /// <summary>
    /// The URL that the hyperlink points to
    /// </summary>
    string? Href { get; set; }

    /// <summary>
    /// Where to display the linked URL
    /// </summary>
    Target? Target { get; set; }

    /// <summary>
    /// Controls Blazor enhanced navigation for this link.
    /// </summary>
    bool? EnhanceNav { get; set; }
}


