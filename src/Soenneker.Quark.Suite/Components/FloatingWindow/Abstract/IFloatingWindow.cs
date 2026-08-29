using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Soenneker.Lepton.Suite.Abstract;

namespace Soenneker.Quark;

/// <summary>
/// Represents a floating, draggable, and resizable Quark window.
/// </summary>
public interface IFloatingWindow : ILeptonCancellableIdentifiableContentElement
{
    /// <summary>
    /// Gets or sets a value indicating whether visible.
    /// </summary>
    bool Visible { get; set; }

    /// <summary>
    /// Gets or sets visible changed.
    /// </summary>
    EventCallback<bool> VisibleChanged { get; set; }

    /// <summary>
    /// Gets or sets on show.
    /// </summary>
    EventCallback OnShow { get; set; }

    /// <summary>
    /// Gets or sets on hide.
    /// </summary>
    EventCallback OnHide { get; set; }

    /// <summary>
    /// Gets or sets on drag start.
    /// </summary>
    EventCallback OnDragStart { get; set; }

    /// <summary>
    /// Gets or sets on drag end.
    /// </summary>
    EventCallback OnDragEnd { get; set; }

    /// <summary>
    /// Gets or sets options.
    /// </summary>
    FloatingWindowOptions Options { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether draggable.
    /// </summary>
    bool? Draggable { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether resizable.
    /// </summary>
    bool? Resizable { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether show close button.
    /// </summary>
    bool? ShowCloseButton { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether show title bar.
    /// </summary>
    bool? ShowTitleBar { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether enabled.
    /// </summary>
    bool? Enabled { get; set; }

    /// <summary>
    /// Gets or sets initial x.
    /// </summary>
    int? InitialX { get; set; }

    /// <summary>
    /// Gets or sets initial y.
    /// </summary>
    int? InitialY { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether use cdn.
    /// </summary>
    bool? UseCdn { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether auto size to content.
    /// </summary>
    bool? AutoSizeToContent { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether dynamic auto size to content.
    /// </summary>
    bool? DynamicAutoSizeToContent { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether recenter on resize.
    /// </summary>
    bool? RecenterOnResize { get; set; }

    /// <summary>
    /// Shows floating Window for the Floating Window.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the show operation is complete.</returns>
    ValueTask Show(CancellationToken cancellationToken = default);

    /// <summary>
    /// Hides floating Window for the Floating Window.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the hide operation is complete.</returns>
    ValueTask Hide(CancellationToken cancellationToken = default);

    /// <summary>
    /// Toggles floating Window for the Floating Window.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the toggle operation is complete.</returns>
    ValueTask Toggle(CancellationToken cancellationToken = default);

    /// <summary>
    /// Closes floating Window for the Floating Window.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the close operation is complete.</returns>
    ValueTask Close(CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets position.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task whose result is the requested (int x, int y).</returns>
    ValueTask<(int x, int y)> GetPosition(CancellationToken cancellationToken = default);

    /// <summary>
    /// Sets position.
    /// </summary>
    /// <param name="x">Operand passed to the accumulator function.</param>
    /// <param name="y">Vertical coordinate to apply.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the position has been stored.</returns>
    ValueTask SetPosition(int x, int y, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets size.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task whose result is the requested floating Window Size.</returns>
    ValueTask<FloatingWindowSize> GetSize(CancellationToken cancellationToken = default);

    /// <summary>
    /// Sets size.
    /// </summary>
    /// <param name="width">Width to apply.</param>
    /// <param name="height">Height to apply.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the size has been stored.</returns>
    ValueTask SetSize(int width, int height, CancellationToken cancellationToken = default);

    /// <summary>
    /// Centers floating Window.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the center operation is complete.</returns>
    ValueTask Center(CancellationToken cancellationToken = default);
}
