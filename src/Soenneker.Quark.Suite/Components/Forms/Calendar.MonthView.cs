using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

public partial class Calendar
{
    private readonly System.Collections.Generic.List<MonthView> _monthViews = [];

    private MonthView GetMonthView(int index)
    {
        while (_monthViews.Count <= index) _monthViews.Add(new MonthView(this, _monthViews.Count));
        return _monthViews[index];
    }

    private sealed class MonthView
    {
        private readonly Calendar _owner;
        private readonly int _index;
        internal Func<ChangeEventArgs, Task> MonthChanged { get; }
        internal Func<ChangeEventArgs, Task> YearChanged { get; }
        internal MonthView(Calendar owner, int index)
        {
            _owner = owner;
            _index = index;
            MonthChanged = ChangeMonth;
            YearChanged = ChangeYear;
        }
        private Task ChangeMonth(ChangeEventArgs args) => _owner.OnMonthChanged(args, _index);
        private Task ChangeYear(ChangeEventArgs args) => _owner.OnYearChanged(args, _index);
    }
}
