using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>
/// Consumer-facing Sonner utility for creating and dismissing toasts from components.
/// </summary>
public interface ISonnerUtil
{
    ValueTask<string> Toast(string title, Action<SonnerToastOptions>? configure = null, CancellationToken cancellationToken = default);

    ValueTask<string> Toast(RenderFragment content, Action<SonnerToastOptions>? configure = null, CancellationToken cancellationToken = default);

    ValueTask<string> Success(string title, Action<SonnerToastOptions>? configure = null, CancellationToken cancellationToken = default);

    ValueTask<string> Info(string title, Action<SonnerToastOptions>? configure = null, CancellationToken cancellationToken = default);

    ValueTask<string> Warning(string title, Action<SonnerToastOptions>? configure = null, CancellationToken cancellationToken = default);

    ValueTask<string> Error(string title, Action<SonnerToastOptions>? configure = null, CancellationToken cancellationToken = default);

    ValueTask<string> Loading(string title, Action<SonnerToastOptions>? configure = null, CancellationToken cancellationToken = default);

    ValueTask<string> Custom(RenderFragment content, Action<SonnerToastOptions>? configure = null, CancellationToken cancellationToken = default);

    ValueTask<string> Promise(ValueTask task, SonnerPromiseOptions options, CancellationToken cancellationToken = default);

    ValueTask<string> Promise(Func<ValueTask> taskFactory, SonnerPromiseOptions options, CancellationToken cancellationToken = default);

    ValueTask Dismiss(string? id = null, CancellationToken cancellationToken = default);
}
