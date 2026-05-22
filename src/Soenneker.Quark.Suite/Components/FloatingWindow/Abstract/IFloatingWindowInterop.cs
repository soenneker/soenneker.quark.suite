using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace Soenneker.Quark;

public interface IFloatingWindowInterop : IAsyncDisposable
{
    ValueTask Initialize(bool useCdn = true, CancellationToken cancellationToken = default);

    ValueTask Create(string id, FloatingWindowOptions options, CancellationToken cancellationToken = default);

    ValueTask SetCallbacks(string id, DotNetObjectReference<FloatingWindow> dotNetRef, CancellationToken cancellationToken = default);

    ValueTask Destroy(string id, CancellationToken cancellationToken = default);

    ValueTask Show(string id, CancellationToken cancellationToken = default);

    ValueTask Hide(string id, CancellationToken cancellationToken = default);

    ValueTask Toggle(string id, CancellationToken cancellationToken = default);

    ValueTask Close(string id, CancellationToken cancellationToken = default);

    ValueTask<(int x, int y)> GetPosition(string id, CancellationToken cancellationToken = default);

    ValueTask SetPosition(string id, int x, int y, CancellationToken cancellationToken = default);

    ValueTask<FloatingWindowSize> GetSize(string id, CancellationToken cancellationToken = default);

    ValueTask SetSize(string id, int width, int height, CancellationToken cancellationToken = default);

    ValueTask BringToFront(string id, CancellationToken cancellationToken = default);

    ValueTask<FloatingWindowSize> GetViewportSize(CancellationToken cancellationToken = default);

    ValueTask CenterInViewport(string id, CancellationToken cancellationToken = default);
}
