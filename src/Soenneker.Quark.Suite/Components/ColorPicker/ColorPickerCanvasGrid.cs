using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Soenneker.Quark;

// The accessible hit targets do not change as the hue, alpha or selected color changes.
internal sealed partial class ColorPickerCanvasGrid : ComponentBase
{
    private static readonly Cell[] _cells = CreateCells();
    private bool _renderedDisabled;
    private CellEvents[]? _events;

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
            builder.CloseElement();
        }
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
