using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>Hosts shared persistent live regions. Place outside inert or aria-hidden containers.</summary>
public interface IAnnounceProvider
{
    /// <summary>Application content containing Announce components.</summary>
    RenderFragment? ChildContent { get; set; }
}
