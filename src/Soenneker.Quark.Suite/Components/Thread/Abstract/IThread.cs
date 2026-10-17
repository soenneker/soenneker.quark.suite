using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
namespace Soenneker.Quark;
/// <summary>A conversation surface with optional measured message virtualization.</summary>
public interface IThread : IElement
{
    /// <summary>Renders only a measured window of messages. Defaults to false. Constrain the thread height.</summary>
    bool Virtualize { get; set; }
    /// <summary>Stable message keys in chronological order.</summary>
    IReadOnlyList<string> ItemKeys { get; set; }
    /// <summary>Renders a message at its full collection index. Keep message state in the caller.</summary>
    RenderFragment<int>? ItemTemplate { get; set; }
    /// <summary>Whether older messages are available.</summary>
    bool HasOlderItems { get; set; }
    /// <summary>Loads and prepends an older page without replacing the current messages.</summary>
    EventCallback LoadOlder { get; set; }
}
