using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Soenneker.Quark;

/// <summary>
/// JS interop for Sonner measurement and client-side host behavior helpers.
/// </summary>
public interface ISonnerInterop : IAsyncDisposable
{
    ValueTask Initialize(CancellationToken cancellationToken = default);

    ValueTask RegisterHotkey(ElementReference section, IReadOnlyList<string>? hotkey, CancellationToken cancellationToken = default);

    ValueTask UnregisterHotkey(ElementReference section, CancellationToken cancellationToken = default);

    ValueTask RegisterSwipeHandlers(ElementReference section, DotNetObjectReference<Sonner> callbackReference, CancellationToken cancellationToken = default);

    ValueTask UnregisterSwipeHandlers(ElementReference section, CancellationToken cancellationToken = default);

    ValueTask<Dictionary<string, double>> MeasureToastHeights(ElementReference section, CancellationToken cancellationToken = default);
}
