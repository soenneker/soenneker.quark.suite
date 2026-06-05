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
    /// Executes the dismiss operation.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    ValueTask Dismiss(string? id = null, CancellationToken cancellationToken = default);
}
