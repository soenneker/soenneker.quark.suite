using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

internal sealed partial class ColorPickerCanvasGrid
{
    private sealed class CellEvents : IHandleEvent
    {
        private readonly ColorPickerCanvasGrid _owner;
        private readonly int _index;

        internal CellEvents(ColorPickerCanvasGrid owner, int index)
        {
            _owner = owner;
            _index = index;
            Click = EventCallback.Factory.Create(this, Select);
        }

        internal EventCallback Click { get; }

        private Task Select()
        {
            Cell cell = _cells[_index];
            return _owner.Owner.SetCanvasColor(cell.Saturation, cell.Lightness);
        }

        public Task HandleEventAsync(EventCallbackWorkItem callback, object? argument) =>
            ((IHandleEvent)_owner).HandleEventAsync(callback, argument);
    }
}
