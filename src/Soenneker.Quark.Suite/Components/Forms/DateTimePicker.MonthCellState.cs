using System;

namespace Soenneker.Quark;

public partial class DateTimePicker
{
    private readonly record struct MonthCellState(DateOnly DisplayMonth, string CultureName, DayOfWeek FirstDayOfWeek, DateOnly? Min, DateOnly? Max);
}
