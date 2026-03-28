using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

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

    ValueTask<IReadOnlyList<SonnerToast>> GetToasts();

    ValueTask<string> Toast(string title, Action<SonnerToastOptions>? configure = null);

    ValueTask<string> Toast(RenderFragment content, Action<SonnerToastOptions>? configure = null);

    ValueTask<string> Success(string title, Action<SonnerToastOptions>? configure = null);

    ValueTask<string> Info(string title, Action<SonnerToastOptions>? configure = null);

    ValueTask<string> Warning(string title, Action<SonnerToastOptions>? configure = null);

    ValueTask<string> Error(string title, Action<SonnerToastOptions>? configure = null);

    ValueTask<string> Loading(string title, Action<SonnerToastOptions>? configure = null);

    ValueTask<string> Custom(RenderFragment content, Action<SonnerToastOptions>? configure = null);

    ValueTask<string> Promise(ValueTask task, SonnerPromiseOptions options);

    ValueTask<string> Promise(Func<ValueTask> taskFactory, SonnerPromiseOptions options);

    ValueTask RegisterToaster(string? toasterId, SonnerPosition? defaultPosition, int? defaultDuration, bool? closeButton);

    ValueTask UnregisterToaster(string? toasterId);

    ValueTask Pause(string? toasterId, SonnerPosition position);

    ValueTask Resume(string? toasterId, SonnerPosition position);

    ValueTask Dismiss(string? id = null);
}
