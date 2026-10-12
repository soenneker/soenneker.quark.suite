using Soenneker.Atomics.ValueBools;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components.Rendering;

namespace Soenneker.Quark;

// The accessible hit targets do not change as the hue, alpha or selected color changes.
internal sealed partial class ColorPickerCanvasGrid : ComponentBase, IAsyncDisposable
{
    private static readonly Cell[] _cells = CreateCells();
    private bool _renderedDisabled;
    private CellEvents[]? _events;
    private ElementReference _cellElement;
    private bool? _registeredDisabled;
    private ValueAtomicBool _disposed;
    private Action<ElementReference>? _captureElement;

    [Inject] private IColorPickerInterop Interop { get; set; } = null!;

    public ColorPickerCanvasGrid()
    {
    }

    [Parameter]
    public ColorPicker Owner { get; set; } = null!;

    [Parameter]
    public bool Disabled { get; set; }

    protected override bool ShouldRender() => Disabled != _renderedDisabled;

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        _renderedDisabled = Disabled;

        _events ??= new CellEvents[_cells.Length];
        for (int i = 0; i < _cells.Length; i++)
        {
            Cell cell = _cells[i];
            builder.OpenElement(0, "button");
            builder.AddAttribute(1, "type", "button");
            builder.AddAttribute(2, "data-slot", ColorPicker.CanvasCellSlot);
            builder.AddAttribute(3, "aria-label", cell.Label);
            builder.AddAttribute(4, "class", ColorPicker.CanvasCellClass);
            builder.AddAttribute(5, "style", cell.Style);
            builder.AddAttribute(6, "disabled", Disabled);
            builder.AddAttribute(7, "onclick", (_events[i] ??= new CellEvents(this, i)).Click);
            if (i == 0)
                builder.AddElementReferenceCapture(8, _captureElement ??= element => _cellElement = element);
            builder.CloseElement();
        }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (_disposed.Value || _registeredDisabled == Disabled) return;
        try
        {
            if (await Interop.RegisterCanvas(_cellElement, Owner.GetCanvasReference(), Disabled))
                _registeredDisabled = Disabled;
            if (_disposed.Value) await Interop.UnregisterCanvas(_cellElement);
        }
        catch (Exception ex) when (ex is JSDisconnectedException or InvalidOperationException or OperationCanceledException or ObjectDisposedException) { }
    }

    public async ValueTask DisposeAsync()
    {
        if (!_disposed.TrySetTrue())
            return;
        try { await Interop.UnregisterCanvas(_cellElement); }
        catch (Exception ex) when (ex is JSDisconnectedException or InvalidOperationException or OperationCanceledException or ObjectDisposedException) { }
    }

    private static Cell[] CreateCells()
    {
        var cells = new Cell[121];
        var index = 0;

        for (var lightnessIndex = 0; lightnessIndex <= 10; lightnessIndex++)
        {
            for (var saturationIndex = 0; saturationIndex <= 10; saturationIndex++)
            {
                var saturation = saturationIndex * 10;
                var lightness = 100 - lightnessIndex * 10;
                cells[index++] = new Cell(saturation, lightness,
                    $"Saturation {saturation}, lightness {lightness}",
                    $"left:{saturation}%;top:{lightnessIndex * 10}%;width:10%;height:10%;");
            }
        }

        return cells;
    }

}
