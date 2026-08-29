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
    /// <summary>
    /// Occurs when state changed.
    /// </summary>
    event Action? StateChanged;

    /// <summary>
    /// Gets or sets default position.
    /// </summary>
    SonnerPosition DefaultPosition { get; set; }

    /// <summary>
    /// Gets or sets default duration.
    /// </summary>
    int DefaultDuration { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether default close button.
    /// </summary>
    bool DefaultCloseButton { get; set; }

    /// <summary>
    /// Gets toasts.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task whose result is the collection returned by get Toasts.</returns>
    ValueTask<IReadOnlyList<SonnerToast>> GetToasts(CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates sonner Service.
    /// </summary>
    /// <param name="title">Page title, when available.</param>
    /// <param name="configure">Callback used to configure the registered service or operation.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task whose result is the text returned by toast.</returns>
    ValueTask<string> Toast(string title, Action<SonnerToastOptions>? configure = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates sonner Service.
    /// </summary>
    /// <param name="content">Content to render, store, or send.</param>
    /// <param name="configure">Callback used to configure the registered service or operation.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task whose result is the text returned by toast.</returns>
    ValueTask<string> Toast(RenderFragment content, Action<SonnerToastOptions>? configure = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a successful result containing the supplied payload.
    /// </summary>
    /// <param name="title">Page title, when available.</param>
    /// <param name="configure">Callback used to configure the registered service or operation.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task whose result is the text returned by success.</returns>
    ValueTask<string> Success(string title, Action<SonnerToastOptions>? configure = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns the value produced by info.
    /// </summary>
    /// <param name="title">Page title, when available.</param>
    /// <param name="configure">Callback used to configure the registered service or operation.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task whose result is the text returned by info.</returns>
    ValueTask<string> Info(string title, Action<SonnerToastOptions>? configure = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns the value produced by warning.
    /// </summary>
    /// <param name="title">Page title, when available.</param>
    /// <param name="configure">Callback used to configure the registered service or operation.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task whose result is the text returned by warning.</returns>
    ValueTask<string> Warning(string title, Action<SonnerToastOptions>? configure = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns the value produced by error.
    /// </summary>
    /// <param name="title">Page title, when available.</param>
    /// <param name="configure">Callback used to configure the registered service or operation.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task whose result is the text returned by error.</returns>
    ValueTask<string> Error(string title, Action<SonnerToastOptions>? configure = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Loads ing.
    /// </summary>
    /// <param name="title">Page title, when available.</param>
    /// <param name="configure">Callback used to configure the registered service or operation.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task whose result is the text returned by loading.</returns>
    ValueTask<string> Loading(string title, Action<SonnerToastOptions>? configure = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds the supplied custom utility token without interpreting its value.
    /// </summary>
    /// <param name="content">Content to render, store, or send.</param>
    /// <param name="configure">Callback used to configure the registered service or operation.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task whose result is the text returned by custom.</returns>
    ValueTask<string> Custom(RenderFragment content, Action<SonnerToastOptions>? configure = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates sonner Service.
    /// </summary>
    /// <param name="task">Asynchronous operation to run.</param>
    /// <param name="options">Options to configure for the Sonner Service.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task whose result is the text returned by promise.</returns>
    ValueTask<string> Promise(ValueTask task, SonnerPromiseOptions options, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates sonner Service.
    /// </summary>
    /// <param name="taskFactory">Callback used by promise.</param>
    /// <param name="options">Options to configure for the Sonner Service.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task whose result is the text returned by promise.</returns>
    ValueTask<string> Promise(Func<ValueTask> taskFactory, SonnerPromiseOptions options, CancellationToken cancellationToken = default);

    /// <summary>
    /// Registers toaster.
    /// </summary>
    /// <param name="toasterId">Identifier of the toaster to target.</param>
    /// <param name="defaultPosition">Default Position for the register toaster operation.</param>
    /// <param name="defaultDuration">Default Duration for the register toaster operation.</param>
    /// <param name="closeButton">Close Button for the register toaster operation.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the toaster registration is complete.</returns>
    ValueTask RegisterToaster(string? toasterId, SonnerPosition? defaultPosition, int? defaultDuration, bool? closeButton,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Unregisters toaster for the Sonner Service.
    /// </summary>
    /// <param name="toasterId">Identifier of the toaster to target.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the toaster registration has been removed.</returns>
    ValueTask UnregisterToaster(string? toasterId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Pauses sonner Service.
    /// </summary>
    /// <param name="toasterId">Identifier of the toaster to target.</param>
    /// <param name="position">Position for the pause operation.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the pause operation is complete.</returns>
    ValueTask Pause(string? toasterId, SonnerPosition position, CancellationToken cancellationToken = default);

    /// <summary>
    /// Resumes sonner Service.
    /// </summary>
    /// <param name="toasterId">Identifier of the toaster to target.</param>
    /// <param name="position">Position for the resume operation.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the resume operation is complete.</returns>
    ValueTask Resume(string? toasterId, SonnerPosition position, CancellationToken cancellationToken = default);

    /// <summary>
    /// Dismisses sonner Service.
    /// </summary>
    /// <param name="id">Identifier of the Sonner Service instance or registration to target.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the dismiss operation is complete.</returns>
    ValueTask Dismiss(string? id = null, CancellationToken cancellationToken = default);
}
