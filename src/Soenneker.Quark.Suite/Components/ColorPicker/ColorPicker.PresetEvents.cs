using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

public partial class ColorPicker
{
    private sealed class PresetEvents : IHandleEvent
    {
        private readonly ColorPicker _owner;
        private readonly string _color;

        public PresetEvents(ColorPicker owner, string color)
        {
            _owner = owner;
            _color = color;
            Label = $"Select {color}";
            Style = $"background:{color};";
            Click = EventCallback.Factory.Create(this, Select);
        }

        public int Generation { get; set; }
        public string Label { get; }
        public string Style { get; }
        public EventCallback Click { get; }

        private Task Select() => _owner.SetFromInput(_color);

        public Task HandleEventAsync(EventCallbackWorkItem callback, object? argument) =>
            ((IHandleEvent)_owner).HandleEventAsync(callback, argument);
    }
}
