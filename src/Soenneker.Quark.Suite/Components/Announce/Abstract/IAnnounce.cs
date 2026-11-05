using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>Screen-reader announcement published through the nearest AnnounceProvider. Separate from the visual Announcement component.</summary>
public interface IAnnounce
{
    /// <summary>Priority: polite, assertive, or off. Defaults to polite.</summary>
    string Type { get; set; }
    /// <summary>Content to announce, removed when the component is disposed.</summary>
    RenderFragment? ChildContent { get; set; }
}
