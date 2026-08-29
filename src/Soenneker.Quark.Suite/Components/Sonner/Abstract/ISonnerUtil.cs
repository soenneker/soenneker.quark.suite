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
    /// Creates sonner.
    /// </summary>
    /// <param name="title">Page title, when available.</param>
    /// <param name="configure">Callback used to configure the registered service or operation.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task whose result is the text returned by toast.</returns>
    ValueTask<string> Toast(string title, Action<SonnerToastOptions>? configure = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates sonner.
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
    /// Creates sonner.
    /// </summary>
    /// <param name="task">Asynchronous operation to run.</param>
    /// <param name="options">Options to configure for the Sonner.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task whose result is the text returned by promise.</returns>
    ValueTask<string> Promise(ValueTask task, SonnerPromiseOptions options, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates sonner.
    /// </summary>
    /// <param name="taskFactory">Callback used by promise.</param>
    /// <param name="options">Options to configure for the Sonner.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task whose result is the text returned by promise.</returns>
    ValueTask<string> Promise(Func<ValueTask> taskFactory, SonnerPromiseOptions options, CancellationToken cancellationToken = default);

    /// <summary>
    /// Dismisses sonner.
    /// </summary>
    /// <param name="id">Identifier of the Sonner instance or registration to target.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the dismiss operation is complete.</returns>
    ValueTask Dismiss(string? id = null, CancellationToken cancellationToken = default);
}
