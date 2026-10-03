using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Soenneker.Quark;

public partial class Cascader
{
    private sealed class OptionEventHandlers : IHandleEvent
    {
        private readonly Cascader _owner;
        private readonly CascaderOption _option;
        private readonly int _column;
        private readonly int _index;

        public OptionEventHandlers(Cascader owner, CascaderOption option, int column, int index)
        {
            _owner = owner;
            _option = option;
            _column = column;
            _index = index;
            Click = EventCallback.Factory.Create(this, Select);
            MouseEnter = EventCallback.Factory.Create(this, Expand);
            Focus = EventCallback.Factory.Create(this, SetFocus);
            KeyDown = EventCallback.Factory.Create<KeyboardEventArgs>(this, HandleKeyDown);
        }

        public int Generation { get; set; }
        public EventCallback Click { get; }
        public EventCallback MouseEnter { get; }
        public EventCallback Focus { get; }
        public EventCallback<KeyboardEventArgs> KeyDown { get; }

        private Task Select() => _owner.SelectOption(_option, _column);
        private Task Expand() => _owner.ExpandOnHover(_option, _column);
        private Task SetFocus() => _owner.SetFocused(_column, _index);
        private Task HandleKeyDown(KeyboardEventArgs args) => _owner.HandleOptionKeyDown(args, _option, _column, _index);

        public Task HandleEventAsync(EventCallbackWorkItem callback, object? argument) =>
            ((IHandleEvent)_owner).HandleEventAsync(callback, argument);
    }
}
