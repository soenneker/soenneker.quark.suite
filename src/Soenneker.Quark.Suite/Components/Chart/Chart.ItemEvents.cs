using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Soenneker.Quark;

public partial class Chart
{
    private Dictionary<(int Index, int Series), ItemEvents>? _itemEvents;

    private ItemEvents GetItemEvents(int index, int series)
    {
        var events = _itemEvents ??= [];
        var key = (index, series);
        if (!events.TryGetValue(key, out var item)) events.Add(key, item = new ItemEvents(this, index, series));
        return item;
    }

    private void PruneItemEvents()
    {
        if (_itemEvents is null) return;
        foreach (var entry in _itemEvents)
        {
            var (index, series) = entry.Key;
            if (series == -3 ? index >= Series.Count || IsRadial :
                index >= PointCount || series >= Series.Count || (series == -2) != IsRadial)
                _itemEvents.Remove(entry.Key);
        }
    }

    private sealed class ItemEvents(Chart owner, int index, int series)
    {
        private EventCallback _hover, _press, _focus, _complete, _click;
        private EventCallback<KeyboardEventArgs> _keyDown;
        internal EventCallback Hover => _hover.HasDelegate ? _hover : _hover = EventCallback.Factory.Create(owner.HoverEvents, HandleHover);
        internal EventCallback Press => _press.HasDelegate ? _press : _press = EventCallback.Factory.Create(owner.HoverEvents, HandlePress);
        internal EventCallback Focus => _focus.HasDelegate ? _focus : _focus = EventCallback.Factory.Create(owner.HoverEvents, HandleFocus);
        internal EventCallback Complete => _complete.HasDelegate ? _complete : _complete = EventCallback.Factory.Create(owner, HandleComplete);
        internal EventCallback Click => _click.HasDelegate ? _click : _click = EventCallback.Factory.Create(owner, HandleClick);
        internal EventCallback<KeyboardEventArgs> KeyDown => _keyDown.HasDelegate ? _keyDown : _keyDown = EventCallback.Factory.Create<KeyboardEventArgs>(owner, HandleKeyDown);

        private void HandleHover()
        {
            if (series == -2) owner.HoverEvents.Slice(index);
            else if (series == -1) owner.HoverEvents.Category(index);
            else owner.HoverEvents.Point(index, series);
        }

        private Task HandlePress() => series == -1 ? owner.HoverEvents.PressCategory(index) : owner.HoverEvents.PressPoint(index, series);
        private Task HandleFocus() => owner.HoverEvents.FocusPoint(index, series);
        private Task HandleComplete() => owner.CompleteRangeSelection(index);
        private Task HandleKeyDown(KeyboardEventArgs args) => series == -2 ? owner.HandleSliceKeyDown(args, index) : owner.HandlePointKeyDown(args, index, series);
        private Task HandleClick()
        {
            if (series == -3)
            {
                owner.ToggleSeries(index);
                return Task.CompletedTask;
            }
            return series == -2 ? owner.SelectSlice(index) : owner.Options.EnableRangeSelection ? Task.CompletedTask : owner.SelectPoint(index, series);
        }
    }
}
