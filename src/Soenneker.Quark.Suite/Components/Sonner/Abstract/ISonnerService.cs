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
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task containing the result of the operation.</returns>
    ValueTask<IReadOnlyList<SonnerToast>> GetToasts(CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes the toast operation.
    /// </summary>
    /// <param name="title">The title.</param>
    /// <param name="configure">The configure.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task containing the result of the operation.</returns>
    ValueTask<string> Toast(string title, Action<SonnerToastOptions>? configure = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes the toast operation.
    /// </summary>
    /// <param name="content">The content.</param>
    /// <param name="configure">The configure.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task containing the result of the operation.</returns>
    ValueTask<string> Toast(RenderFragment content, Action<SonnerToastOptions>? configure = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes the success operation.
    /// </summary>
    /// <param name="title">The title.</param>
    /// <param name="configure">The configure.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task containing the result of the operation.</returns>
    ValueTask<string> Success(string title, Action<SonnerToastOptions>? configure = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes the info operation.
    /// </summary>
    /// <param name="title">The title.</param>
    /// <param name="configure">The configure.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task containing the result of the operation.</returns>
    ValueTask<string> Info(string title, Action<SonnerToastOptions>? configure = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes the warning operation.
    /// </summary>
    /// <param name="title">The title.</param>
    /// <param name="configure">The configure.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task containing the result of the operation.</returns>
    ValueTask<string> Warning(string title, Action<SonnerToastOptions>? configure = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes the error operation.
    /// </summary>
    /// <param name="title">The title.</param>
    /// <param name="configure">The configure.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task containing the result of the operation.</returns>
    ValueTask<string> Error(string title, Action<SonnerToastOptions>? configure = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes the loading operation.
    /// </summary>
    /// <param name="title">The title.</param>
    /// <param name="configure">The configure.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task containing the result of the operation.</returns>
    ValueTask<string> Loading(string title, Action<SonnerToastOptions>? configure = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes the custom operation.
    /// </summary>
    /// <param name="content">The content.</param>
    /// <param name="configure">The configure.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task containing the result of the operation.</returns>
    ValueTask<string> Custom(RenderFragment content, Action<SonnerToastOptions>? configure = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes the promise operation.
    /// </summary>
    /// <param name="task">The task.</param>
    /// <param name="options">The options.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task containing the result of the operation.</returns>
    ValueTask<string> Promise(ValueTask task, SonnerPromiseOptions options, CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes the promise operation.
    /// </summary>
    /// <param name="taskFactory">The task factory.</param>
    /// <param name="options">The options.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task containing the result of the operation.</returns>
    ValueTask<string> Promise(Func<ValueTask> taskFactory, SonnerPromiseOptions options, CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes the register toaster operation.
    /// </summary>
    /// <param name="toasterId">The toaster id.</param>
    /// <param name="defaultPosition">The default position.</param>
    /// <param name="defaultDuration">The default duration.</param>
    /// <param name="closeButton">The close button.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    ValueTask RegisterToaster(string? toasterId, SonnerPosition? defaultPosition, int? defaultDuration, bool? closeButton,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes the unregister toaster operation.
    /// </summary>
    /// <param name="toasterId">The toaster id.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    ValueTask UnregisterToaster(string? toasterId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes the pause operation.
    /// </summary>
    /// <param name="toasterId">The toaster id.</param>
    /// <param name="position">The position.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    ValueTask Pause(string? toasterId, SonnerPosition position, CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes the resume operation.
    /// </summary>
    /// <param name="toasterId">The toaster id.</param>
    /// <param name="position">The position.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    ValueTask Resume(string? toasterId, SonnerPosition position, CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes the dismiss operation.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    ValueTask Dismiss(string? id = null, CancellationToken cancellationToken = default);
}
