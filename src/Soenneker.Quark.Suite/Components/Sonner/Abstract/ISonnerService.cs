using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>
/// Imperative Sonner-style toast API.
/// </summary>
public interface ISonnerService
{
    event Action? StateChanged;

    SonnerPosition DefaultPosition { get; set; }

    int DefaultDuration { get; set; }

    bool DefaultCloseButton { get; set; }

    IReadOnlyList<SonnerToast> GetToasts();

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

    void RegisterToaster(string? toasterId, SonnerPosition? defaultPosition, int? defaultDuration, bool? closeButton);

    void UnregisterToaster(string? toasterId);

    void Pause(string? toasterId, SonnerPosition position);

    void Resume(string? toasterId, SonnerPosition position);

    void Dismiss(string? id = null);
}
