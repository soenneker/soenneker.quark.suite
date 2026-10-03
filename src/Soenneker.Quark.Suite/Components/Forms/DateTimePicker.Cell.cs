using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

public partial class DateTimePicker
{
    private sealed class Cell : IHandleEvent
    {
        private readonly DateTimePicker _owner;

        internal Cell(DateTimePicker owner, DateOnly date, bool inMonth, bool disabled)
        {
            _owner = owner;
            Date = date;
            InMonth = inMonth;
            Disabled = disabled;
            Click = Select;
        }

        internal DateOnly Date { get; }
        internal bool InMonth { get; }
        internal bool Disabled { get; }
        internal Func<Task> Click { get; }
        private Task Select() => _owner.SelectDate(Date);
        public Task HandleEventAsync(EventCallbackWorkItem callback, object? argument) =>
            ((IHandleEvent)_owner).HandleEventAsync(callback, argument);
    }
}
