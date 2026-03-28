using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>
/// Consumer-facing Sonner utility for creating and dismissing toasts from components.
/// </summary>
public interface ISonnerUtil
{
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

    ValueTask Dismiss(string? id = null);
}
