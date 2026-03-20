using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>
/// Consumer-facing Sonner utility for creating and dismissing toasts from components.
/// </summary>
public interface ISonnerUtil
{
    string Toast(string title, Action<SonnerToastOptions>? configure = null);

    string Toast(RenderFragment content, Action<SonnerToastOptions>? configure = null);

    string Success(string title, Action<SonnerToastOptions>? configure = null);

    string Info(string title, Action<SonnerToastOptions>? configure = null);

    string Warning(string title, Action<SonnerToastOptions>? configure = null);

    string Error(string title, Action<SonnerToastOptions>? configure = null);

    string Loading(string title, Action<SonnerToastOptions>? configure = null);

    string Custom(RenderFragment content, Action<SonnerToastOptions>? configure = null);

    string Promise(Task task, SonnerPromiseOptions options);

    string Promise(Func<Task> taskFactory, SonnerPromiseOptions options);

    void Dismiss(string? id = null);
}
