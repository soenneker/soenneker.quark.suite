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
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the Input is ready for use.</returns>
    ValueTask Initialize(CancellationToken cancellationToken = default);

    /// <summary>
    /// Prevents native defaults for combobox navigation keys synchronously, while preserving text editing and composition.
    /// </summary>
    ValueTask RegisterComboboxKeys(ElementReference input, CancellationToken cancellationToken = default);

    /// <summary>
    /// Removes the combobox keyboard listener from an input.
    /// </summary>
    ValueTask UnregisterComboboxKeys(ElementReference input, CancellationToken cancellationToken = default);

    /// <summary>Prevents tag navigation and delimiter keys synchronously while preserving native text editing and IME composition.</summary>
    ValueTask RegisterTagInputKeys(ElementReference input, string[] delimiters, CancellationToken cancellationToken = default);

    /// <summary>Prevents default browser handling for the supplied navigation keys on a composite control.</summary>
    ValueTask RegisterNavigationKeys(ElementReference element, string[] keys, CancellationToken cancellationToken = default);



    /// <summary>
    /// Gets the current text selection for the input.
    /// </summary>
    /// <param name="input">input to read or transform.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task whose result is the requested input Selection Snapshot.</returns>
    ValueTask<InputSelectionSnapshot?> GetSelection(ElementReference input, CancellationToken cancellationToken = default);

    /// <summary>
    /// Restores the text selection for the input.
    /// </summary>
    /// <param name="input">input to read or transform.</param>
    /// <param name="start">Start for the restore selection operation.</param>
    /// <param name="end">End for the restore selection operation.</param>
    /// <param name="value">CSS value used to construct the utility class.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the restore selection operation is complete.</returns>
    ValueTask RestoreSelection(ElementReference input, int start, int end, string? value, CancellationToken cancellationToken = default);
}
