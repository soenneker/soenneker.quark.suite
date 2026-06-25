using System;
using System.Linq;
using System.Threading.Tasks;
using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components.Web;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Preset_date_range_picker_marks_active_preset_and_disables_dates_outside_constrained_window()
    {
        var cut = Render<PresetDateRangePicker>(parameters => parameters
            .Add(component => component.SelectedRange, new CalendarDateRange(new DateOnly(2026, 6, 1), new DateOnly(2026, 6, 30)))
            .Add(component => component.Max, new DateOnly(2026, 6, 30))
            .Add(component => component.MaxRangeDays, 30)
            .Add(component => component.DisableDatesOutsideMaxRangeWindow, true)
            .Add(component => component.DefaultOpen, true));

        cut.Find("button[data-slot='preset-date-range-picker-trigger']").TextContent.Should().Contain("Last 30 days");
        cut.Find("button[role='menuitemradio'][aria-pressed='true']").TextContent.Should().Contain("Last 30 days");

        cut.Find("td[data-day='2026-05-31'] button").HasAttribute("disabled").Should().BeTrue();
        cut.Find("td[data-day='2026-06-01'] button").HasAttribute("disabled").Should().BeFalse();
    }

    [Test]
    public async Task Preset_date_range_picker_emits_normalized_preset_range()
    {
        CalendarDateRange? changed = null;

        var cut = Render<PresetDateRangePicker>(parameters => parameters
            .Add(component => component.Max, new DateOnly(2026, 6, 24))
            .Add(component => component.MaxRangeDays, 30)
            .Add(component => component.DisableDatesOutsideMaxRangeWindow, true)
            .Add(component => component.DefaultOpen, true)
            .Add(component => component.SelectedRangeChanged, range =>
            {
                changed = range;
                return Task.CompletedTask;
            }));

        await cut.FindAll("button").First(button => button.TextContent.Contains("Last 30 days", StringComparison.Ordinal)).ClickAsync(new MouseEventArgs());

        changed.Should().Be(new CalendarDateRange(new DateOnly(2026, 5, 26), new DateOnly(2026, 6, 24)));
    }

    [Test]
    public void Preset_date_range_picker_supports_custom_presets()
    {
        PresetDateRangePickerOption[] presets =
        [
            PresetDateRangePickerOption.LastDays(7),
            PresetDateRangePickerOption.LastDays(14)
        ];

        var cut = Render<PresetDateRangePicker>(parameters => parameters
            .Add(component => component.SelectedRange, new CalendarDateRange(new DateOnly(2026, 6, 18), new DateOnly(2026, 6, 24)))
            .Add(component => component.Max, new DateOnly(2026, 6, 24))
            .Add(component => component.RangePresets, presets)
            .Add(component => component.DefaultOpen, true));

        cut.FindAll("button[role='menuitemradio']").Should().HaveCount(2);
        cut.Markup.Should().Contain("Last 7 days");
        cut.Markup.Should().Contain("Last 14 days");
        cut.Markup.Should().NotContain("Last 30 days");
    }

    [Test]
    public void Preset_date_range_picker_does_not_disable_max_window_dates_unless_configured()
    {
        var cut = Render<PresetDateRangePicker>(parameters => parameters
            .Add(component => component.Max, new DateOnly(2026, 5, 31))
            .Add(component => component.MaxRangeDays, 30)
            .Add(component => component.DefaultOpen, true));

        cut.Find("td[data-day='2026-05-01'] button").HasAttribute("disabled").Should().BeFalse();
    }
}
