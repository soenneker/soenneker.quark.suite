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
    /// Executes the show operation.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    ValueTask Show(CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes the hide operation.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    ValueTask Hide(CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes the toggle operation.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    ValueTask Toggle(CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes the close operation.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    ValueTask Close(CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets position.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task containing the result of the operation.</returns>
    ValueTask<(int x, int y)> GetPosition(CancellationToken cancellationToken = default);

    /// <summary>
    /// Sets position.
    /// </summary>
    /// <param name="x">The x.</param>
    /// <param name="y">The y.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    ValueTask SetPosition(int x, int y, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets size.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task containing the result of the operation.</returns>
    ValueTask<FloatingWindowSize> GetSize(CancellationToken cancellationToken = default);

    /// <summary>
    /// Sets size.
    /// </summary>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    ValueTask SetSize(int width, int height, CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes the center operation.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    ValueTask Center(CancellationToken cancellationToken = default);
}
