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
    /// Registers canvas.
    /// </summary>
    /// <param name="root">Root directory or repository to process.</param>
    /// <param name="callbackReference">callback Reference to invoke when the operation runs.</param>
    /// <param name="disabled">Whether disabled.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>true if registers canvas for the Color Picker; otherwise, false.</returns>
    ValueTask<bool> RegisterCanvas(ElementReference root, DotNetObjectReference<ColorPicker> callbackReference, bool disabled, CancellationToken cancellationToken = default);

    /// <summary>
    /// Unregisters canvas.
    /// </summary>
    /// <param name="root">Root directory or repository to process.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the canvas registration has been removed.</returns>
    ValueTask UnregisterCanvas(ElementReference root, CancellationToken cancellationToken = default);
}
