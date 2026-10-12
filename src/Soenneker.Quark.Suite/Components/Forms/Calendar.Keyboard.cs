using Soenneker.Atomics.ValueBools;
using System;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Soenneker.Asyncs.Locks;

namespace Soenneker.Quark;

public partial class Calendar
{
    private ElementReference _calendarElement;
    private DotNetObjectReference<Calendar>? _calendarReference;
    private readonly AsyncLock _calendarLock = new();
    private ValueAtomicBool _calendarDisposed;
    private DateOnly? _focusedDate;
    private DateOnly? _pendingFocus;
    private DateOnly CalendarMinimum => UseCultureCalendar ? DateOnly.FromDateTime(_resolvedCulture.Calendar.MinSupportedDateTime) : DateOnly.MinValue;
    private DateOnly CalendarMaximum => UseCultureCalendar ? DateOnly.FromDateTime(_resolvedCulture.Calendar.MaxSupportedDateTime) : DateOnly.MaxValue;
    private int VisibleMonthCount
    {
        get
        {
            var requested = Math.Max(1, NumberOfMonths);
            if (!UseCultureCalendar)
                return Math.Min(requested, 9999 * 12 - (_displayMonth.Year - 1) * 12 - _displayMonth.Month + 1);
            var previous = _displayMonth;
            var count = 1;
            while (count < requested)
            {
                var next = GetDisplayMonthStart(AddCalendarMonths(_displayMonth, count));
                if (next <= previous) break;
                previous = next;
                count++;
            }
            return count;
        }
    }

    private DateOnly GetDisplayMonthStart(DateOnly date)
    {
        date = PresetDateRangePickerUtil.Clamp(date, CalendarMinimum, CalendarMaximum);
        if (!UseCultureCalendar) return new DateOnly(date.Year, date.Month, 1);
        var calendar = _resolvedCulture.Calendar;
        var value = date.ToDateTime(TimeOnly.MinValue);
        return DateOnly.FromDayNumber(Math.Max(CalendarMinimum.DayNumber, date.DayNumber - calendar.GetDayOfMonth(value) + 1));
    }

    private DateOnly AddCalendarMonths(DateOnly date, int months)
    {
        if (!UseCultureCalendar) return AddMonthsClamped(date, months);
        try { return DateOnly.FromDateTime(_resolvedCulture.Calendar.AddMonths(date.ToDateTime(TimeOnly.MinValue), months)); }
        catch (ArgumentException) { return months < 0 ? CalendarMinimum : CalendarMaximum; }
    }

    private int GetCalendarYear(DateOnly date) => UseCultureCalendar ? _resolvedCulture.Calendar.GetYear(date.ToDateTime(TimeOnly.MinValue)) : date.Year;
    private int GetCalendarMonth(DateOnly date) => UseCultureCalendar ? _resolvedCulture.Calendar.GetMonth(date.ToDateTime(TimeOnly.MinValue)) : date.Month;
    private int GetCalendarMonthsInYear(DateOnly date) => UseCultureCalendar ? _resolvedCulture.Calendar.GetMonthsInYear(GetCalendarYear(date)) : 12;

    private async Task SetCaptionMonth(int year, int month, int displayedIndex)
    {
        try
        {
            var date = UseCultureCalendar
                ? DateOnly.FromDateTime(_resolvedCulture.Calendar.ToDateTime(year, Math.Min(month, _resolvedCulture.Calendar.GetMonthsInYear(year)), 1, 0, 0, 0, 0))
                : new DateOnly(year, month, 1);
            _displayMonth = GetDisplayMonthStart(AddCalendarMonths(date, -displayedIndex));
        }
        catch (ArgumentException) { return; }
        await DisplayMonthChanged.InvokeAsync(_displayMonth);
        Refresh();
    }

    private void EnsureFocusableDate()
    {
        var preferred = _focusedDate ?? (IsRangeMode ? EffectiveSelectedRange?.From : IsMultipleMode && EffectiveSelectedDates.Count > 0 ? EffectiveSelectedDates[0] : EffectiveSelectedDate) ?? _today;
        DateOnly? first = null;
        DateOnly? today = null;
        for (var index = 0; index < VisibleMonthCount; index++)
        {
            foreach (var week in BuildMonthWeeks(AddCalendarMonths(_displayMonth, index)))
            {
                foreach (var day in week)
                {
                    if (!day.Available || !day.InMonth || day.Disabled) continue;
                    first ??= day.Date;
                    if (day.Date == preferred) { _focusedDate = preferred; return; }
                    if (day.Date == _today) today = day.Date;
                }
            }
        }
        _focusedDate = today ?? first;
    }

    /// <summary>Moves focus by a calendar navigation key without changing the selected value.</summary>
    [JSInvokable]
    public async Task NavigateCalendar(string date, string key, bool shift)
    {
        if (_calendarDisposed.Value || Disabled || !DateOnly.TryParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var current)) return;
        var rtl = string.Equals(Dir ?? DirectionProviderDir, "rtl", StringComparison.OrdinalIgnoreCase);
        var weekOffset = ((int)current.DayOfWeek - (int)GetFirstDayOfWeek(_resolvedCulture) + 7) % 7;
        var delta = key switch
        {
            "ArrowLeft" => rtl ? 1 : -1,
            "ArrowRight" => rtl ? -1 : 1,
            "ArrowUp" => -7,
            "ArrowDown" => 7,
            "Home" => -weekOffset,
            "End" => 6 - weekOffset,
            "PageUp" or "PageDown" => 0,
            _ => int.MinValue
        };
        if (delta == int.MinValue) return;
        var next = key is "PageUp" or "PageDown"
            ? AddCalendarMonths(current, (key == "PageUp" ? -1 : 1) * (shift ? 12 : 1))
            : DateOnly.FromDayNumber(Math.Clamp(current.DayNumber + delta, 0, DateOnly.MaxValue.DayNumber));
        var minimum = Min is { } min && min > CalendarMinimum ? min : CalendarMinimum;
        var maximum = Max is { } max && max < CalendarMaximum ? max : CalendarMaximum;
        if (minimum > maximum) return;
        next = PresetDateRangePickerUtil.Clamp(next, minimum, maximum);
        var direction = key is "ArrowLeft" or "ArrowUp" or "Home" or "PageUp" ? -1 : 1;
        if (rtl && key is "ArrowLeft" or "ArrowRight") direction = -direction;
        while (IsDisabledDate(next))
        {
            var day = next.DayNumber + direction;
            if (day < minimum.DayNumber || day > maximum.DayNumber) return;
            next = DateOnly.FromDayNumber(day);
        }
        _focusedDate = _pendingFocus = next;
        var lastMonth = GetDisplayMonthStart(AddCalendarMonths(_displayMonth, VisibleMonthCount - 1));
        var nextMonth = GetDisplayMonthStart(next);
        if (nextMonth < _displayMonth || nextMonth > lastMonth)
        {
            _displayMonth = nextMonth;
            await DisplayMonthChanged.InvokeAsync(_displayMonth);
        }
        await RefreshOffThread();
    }

    private static DateOnly AddMonthsClamped(DateOnly date, int months)
    {
        var month = Math.Clamp((date.Year - 1) * 12 + date.Month - 1 + months, 0, 9999 * 12 - 1);
        var year = month / 12 + 1;
        var monthNumber = month % 12 + 1;
        return new DateOnly(year, monthNumber, Math.Min(date.Day, DateTime.DaysInMonth(year, monthNumber)));
    }

    public override async ValueTask DisposeAsync()
    {
        if (!_calendarDisposed.TrySetTrue()) return;
        try
        {
            using (await _calendarLock.Lock())
            {
                if (_calendarReference is not null) await CalendarInterop.Destroy(_calendarElement);
            }
        }
        catch (Exception ex) when (ex is JSDisconnectedException or TaskCanceledException) { }
        finally
        {
            _calendarReference?.Dispose();
            await base.DisposeAsync();
        }
    }
}
