using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>
/// Provides browser interop for native HTML dialog elements.
/// </summary>
public interface INativeDialogInterop : IAsyncDisposable
{
    /// <summary>
    /// Opens a dialog as a non-modal dialog.
    /// </summary>
    ValueTask Show(ElementReference element, CancellationToken cancellationToken = default);

    /// <summary>
    /// Opens a dialog as a modal dialog.
    /// </summary>
    ValueTask ShowModal(ElementReference element, CancellationToken cancellationToken = default);

    /// <summary>
    /// Closes a dialog with an optional return value.
    /// </summary>
    ValueTask Close(ElementReference element, string? returnValue = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the dialog's current return value.
    /// </summary>
    ValueTask<string?> GetReturnValue(ElementReference element, CancellationToken cancellationToken = default);
}
