using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

public partial class PresetDateRangePicker
{
    private sealed class PresetView : IHandleEvent
    {
        private readonly PresetDateRangePicker _owner;

        internal PresetView(PresetDateRangePicker owner, PresetDateRangePickerOption preset)
        {
            _owner = owner;
            Preset = preset;
            Click = Apply;
        }

        internal PresetDateRangePickerOption Preset { get; }
        internal Func<Task> Click { get; }
        private Task Apply() => _owner.ApplyPreset(Preset);
        public Task HandleEventAsync(EventCallbackWorkItem callback, object? argument) =>
            ((IHandleEvent)_owner).HandleEventAsync(callback, argument);
    }
}
