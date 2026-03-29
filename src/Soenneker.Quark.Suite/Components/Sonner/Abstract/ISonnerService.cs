using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Quark;

/// <summary>
/// Imperative Sonner-style toast API.
/// </summary>
public interface ISonnerService : IDisposable, IAsyncDisposable
{
    event Action? StateChanged;

    SonnerPosition DefaultPosition { get; set; }

    int DefaultDuration { get; set; }

    bool DefaultCloseButton { get; set; }

    ValueTask<IReadOnlyList<SonnerToast>> GetToasts(CancellationToken cancellationToken = default);

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

    ValueTask RegisterToaster(string? toasterId, SonnerPosition? defaultPosition, int? defaultDuration, bool? closeButton,
        CancellationToken cancellationToken = default);

    ValueTask UnregisterToaster(string? toasterId, CancellationToken cancellationToken = default);

    ValueTask Pause(string? toasterId, SonnerPosition position, CancellationToken cancellationToken = default);

    ValueTask Resume(string? toasterId, SonnerPosition position, CancellationToken cancellationToken = default);

    ValueTask Dismiss(string? id = null, CancellationToken cancellationToken = default);
}
