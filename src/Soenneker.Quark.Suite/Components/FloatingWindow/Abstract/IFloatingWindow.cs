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
    bool Visible { get; set; }

    EventCallback<bool> VisibleChanged { get; set; }

    EventCallback OnShow { get; set; }

    EventCallback OnHide { get; set; }

    EventCallback OnDragStart { get; set; }

    EventCallback OnDragEnd { get; set; }

    FloatingWindowOptions Options { get; set; }

    bool? Draggable { get; set; }

    bool? Resizable { get; set; }

    bool? ShowCloseButton { get; set; }

    bool? ShowTitleBar { get; set; }

    bool? Enabled { get; set; }

    int? InitialX { get; set; }

    int? InitialY { get; set; }

    bool? UseCdn { get; set; }

    bool? AutoSizeToContent { get; set; }

    bool? DynamicAutoSizeToContent { get; set; }

    bool? RecenterOnResize { get; set; }

    ValueTask Show(CancellationToken cancellationToken = default);

    ValueTask Hide(CancellationToken cancellationToken = default);

    ValueTask Toggle(CancellationToken cancellationToken = default);

    ValueTask Close(CancellationToken cancellationToken = default);

    ValueTask<(int x, int y)> GetPosition(CancellationToken cancellationToken = default);

    ValueTask SetPosition(int x, int y, CancellationToken cancellationToken = default);

    ValueTask<FloatingWindowSize> GetSize(CancellationToken cancellationToken = default);

    ValueTask SetSize(int width, int height, CancellationToken cancellationToken = default);

    ValueTask Center(CancellationToken cancellationToken = default);
}
