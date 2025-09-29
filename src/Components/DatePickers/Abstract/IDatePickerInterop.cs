using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Soenneker.Quark;

/// <summary>
/// Provides initialization for DatePicker resources (eg. CSS) and lifetime management.
/// </summary>
public interface IDatePickerInterop : IAsyncDisposable
{
    /// <summary>
    /// Ensures required static assets are loaded.
    /// </summary>
    /// <param name="cancellationToken">Optional cancellation token.</param>
    ValueTask Initialize(CancellationToken cancellationToken = default);

    /// <summary>
    /// Attaches interactive behavior to the given input element (eg. open picker on click/focus).
    /// </summary>
    /// <param name="element">Input element reference.</param>
    /// <param name="cancellationToken">Optional cancellation token.</param>
    ValueTask Attach(ElementReference element, CancellationToken cancellationToken = default);

    /// <summary>
    /// Registers a global outside-click watcher to close the calendar when clicking outside the container.
    /// </summary>
    /// <param name="container">Root container that wraps the input and panel.</param>
    /// <param name="dotNetRef">DotNet object reference to invoke back into the component.</param>
    /// <param name="methodName">Method to invoke on outside click (should be [JSInvokable]).</param>
    /// <param name="cancellationToken">Optional cancellation token.</param>
    ValueTask RegisterOutsideClose<T>(ElementReference container, DotNetObjectReference<T> dotNetRef, string methodName, CancellationToken cancellationToken = default) where T : class;

    /// <summary>
    /// Disposes any internal state.
    /// </summary>
    new ValueTask DisposeAsync();
}
