using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Soenneker.Quark;

/// <summary>Owns browser measurement, scrolling, focus, and observer lifecycle for virtual collections.</summary>
public interface IVirtualViewportInterop : IAsyncDisposable
{
    /// <summary>Updates the keys and loading behavior of a viewport.</summary>
    ValueTask Update(ElementReference element, DotNetObjectReference<VirtualItems> receiver, IReadOnlyList<string> keys, bool follow, bool before, bool after, bool parentScroll, IReadOnlyList<string> disabledKeys);
    /// <summary>Reveals an item and optionally focuses a descendant after rendering.</summary>
    ValueTask ScrollToIndex(ElementReference element, int index, string? focusSelector);
    /// <summary>Resumes following new items.</summary>
    ValueTask JumpToEnd(ElementReference element);
    /// <summary>Releases all observers for a viewport.</summary>
    ValueTask Destroy(ElementReference element);
}
