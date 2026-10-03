using System;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

public partial class Calendar
{
    private sealed class DayView : IHandleEvent
    {
        private readonly Calendar _owner;
        private readonly DateOnly _date;
        private int _formatVersion = -1;
        private string? _label, _ariaLabel, _dataDay;
        private bool _today, _selected;

        internal DayView(Calendar owner, DateOnly date)
        {
            _owner = owner;
            _date = date;
            Click = Select;
            IsoDate = date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            IsoMonth = date.ToString("yyyy-MM", CultureInfo.InvariantCulture);
        }

        internal int Generation { get; set; }
        internal CalendarDayContext? Context { get; set; }
        internal string? CellClass;
        internal Func<Task> Click { get; }
        internal string IsoDate { get; }
        internal string IsoMonth { get; }
        internal string Label => _label ??= _owner.GetDayLabel(_date);
        internal string DataDay => _dataDay ??= _owner.GetDataDay(_date);
        internal string AriaLabel => _ariaLabel ??= _owner.GetDayAriaLabel(Context!);

        internal void UpdateFormatting(int version, CalendarDayContext context)
        {
            bool selected = context.IsSelected || context.IsRangeStart || context.IsRangeEnd || context.IsRangeMiddle;
            if (_formatVersion != version)
            {
                _formatVersion = version;
                _label = _ariaLabel = _dataDay = null;
            }
            if (_today != context.IsToday || _selected != selected)
                _ariaLabel = null;
            _today = context.IsToday;
            _selected = selected;
        }

        private Task Select() => _owner.SelectDate(_date);

        public Task HandleEventAsync(EventCallbackWorkItem callback, object? argument) =>
            ((IHandleEvent)_owner).HandleEventAsync(callback, argument);
    }
}
