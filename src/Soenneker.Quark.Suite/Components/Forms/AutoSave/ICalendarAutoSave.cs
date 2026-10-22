using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Quark;

/// <summary>Autosave callbacks for single, multiple, and range calendar selections.</summary>
public interface ICalendarAutoSave : IMultipleAutoSaveInput<DateOnly?, DateOnly[]>
{
    /// <summary>Persists the selected range, including an incomplete range while choosing its end.</summary>
    Func<CalendarDateRange?, CancellationToken, ValueTask>? OnAutoSaveRange { get; set; }
}
