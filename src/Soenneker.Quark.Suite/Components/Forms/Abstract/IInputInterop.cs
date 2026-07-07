using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>
/// Provides JavaScript interop for native input behavior.
/// </summary>
public interface IInputInterop : IAsyncDisposable
{
    /// <summary>
    /// Ensures input resources are loaded.
    /// </summary>
    ValueTask Initialize(CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the current text selection for the input.
    /// </summary>
    ValueTask<InputSelectionSnapshot?> GetSelection(ElementReference input, CancellationToken cancellationToken = default);

    /// <summary>
    /// Restores the text selection for the input.
    /// </summary>
    ValueTask RestoreSelection(ElementReference input, int start, int end, string? value, CancellationToken cancellationToken = default);
}
