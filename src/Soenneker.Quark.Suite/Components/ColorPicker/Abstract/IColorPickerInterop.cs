using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Soenneker.Quark;

/// <summary>
/// Defines the color picker interop contract.
/// </summary>
public interface IColorPickerInterop : IAsyncDisposable
{
    /// <summary>
    /// Opens the browser's native eyedropper when it is available.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The selected sRGB hex color, or <see langword="null"/> when the API is unavailable or the picker is cancelled.</returns>
    ValueTask<string?> PickColor(CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes the register canvas operation.
    /// </summary>
    /// <param name="root">The color picker root.</param>
    /// <param name="callbackReference">The callback reference.</param>
    /// <param name="disabled">The disabled.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task containing the result of the operation.</returns>
    ValueTask<bool> RegisterCanvas(ElementReference root, DotNetObjectReference<ColorPicker> callbackReference, bool disabled, CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes the unregister canvas operation.
    /// </summary>
    /// <param name="root">The color picker root.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    ValueTask UnregisterCanvas(ElementReference root, CancellationToken cancellationToken = default);
}
