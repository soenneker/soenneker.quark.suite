namespace Soenneker.Quark;

/// <summary>
/// Interface for the Anchor component
/// </summary>
public interface IAnchor : IElement
{
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


