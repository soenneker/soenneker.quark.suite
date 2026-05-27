using System;
using System.Threading.Tasks;
using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components.Web;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    private static readonly DateOnly InitialRangeFrom = new(2026, 1, 20);
    private static readonly DateOnly InitialRangeTo = new(2026, 2, 9);

    [Test]
    public async Task Calendar_range_selection_adjusts_completed_range_without_resetting()
    {
        (await SelectCompletedRangeDate(new DateOnly(2026, 1, 15))).Should().Be(new CalendarDateRange(new DateOnly(2026, 1, 15), InitialRangeTo));
        (await SelectCompletedRangeDate(new DateOnly(2026, 2, 12))).Should().Be(new CalendarDateRange(InitialRangeFrom, new DateOnly(2026, 2, 12)));
        (await SelectCompletedRangeDate(new DateOnly(2026, 1, 24))).Should().Be(new CalendarDateRange(new DateOnly(2026, 1, 24), InitialRangeTo));
        (await SelectCompletedRangeDate(new DateOnly(2026, 2, 5))).Should().Be(new CalendarDateRange(InitialRangeFrom, new DateOnly(2026, 2, 5)));
    }

    private async Task<CalendarDateRange?> SelectCompletedRangeDate(DateOnly date)
    {
        CalendarDateRange? selected = new(InitialRangeFrom, InitialRangeTo);
        CalendarDateRange? changed = null;

        var cut = Render<Calendar>(parameters => parameters
            .Add(component => component.Mode, CalendarSelectionMode.Range)
            .Add(component => component.DisplayMonth, new DateOnly(2026, 1, 1))
            .Add(component => component.NumberOfMonths, 2)
            .Add(component => component.SelectedRange, selected)
            .Add(component => component.SelectedRangeChanged, next =>
            {
                selected = next;
                changed = next;
                return Task.CompletedTask;
            }));

        await cut.Find($"td[data-day='{date:yyyy-MM-dd}'] button").ClickAsync(new MouseEventArgs());

        return changed;
    }
}
