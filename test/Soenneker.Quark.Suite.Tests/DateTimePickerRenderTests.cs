using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AwesomeAssertions;
using Bunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public async Task Audit_date_time_picker_keeps_validation_ownership_when_panel_inputs_render()
    {
        object? observed = null;
        var initial = new DateTime(2026, 10, 11, 10, 30, 0);
        var cut = Render<Validation>(p => p.Add(c => c.AsyncFunc, (args, _) =>
            {
                observed = args.Value;
                args.Status = ValidationStatus.Success;
                return Task.CompletedTask;
            }).AddChildContent<DateTimePicker>(d => d.Add(c => c.SelectedDateTime, initial)));
        cut.Find("input[type=text]").Focus();
        cut.Find("input[aria-label=Hour]").Input("11");
        observed.Should().Be(initial.AddHours(1));
        cut.FindComponent<DateTimePicker>().Render(p => p.Add(c => c.ReadOnly, true));
        await cut.InvokeAsync(() => cut.Instance.ValidateCurrent());
        observed.Should().Be(initial.AddHours(1));
    }

    [Test]
    public void Audit_date_time_picker_enforces_time_bounds_and_disabled_updates()
    {
        var minimum = new DateTime(2026, 10, 11, 10, 30, 0);
        var cut = Render<DateTimePicker>(p => p.Add(c => c.SelectedDateTime, minimum)
            .Add(c => c.Min, minimum).Add(c => c.Max, minimum.AddHours(2)));
        cut.Find("input[type=text]").ClassList.Should().Contain("placeholder:text-muted-foreground")
            .And.NotContain("text-muted-foreground");
        cut.Find("input[type=text]").Input("2026-10-11 09:00 AM");
        cut.Instance.SelectedDateTime.Should().Be(minimum);
        cut.Find("input[type=text]").Focus();
        cut.Find("button[aria-label='Decrease hour']").Click();
        cut.Instance.SelectedDateTime.Should().Be(minimum);
        cut.Render(p => p.Add(c => c.ReadOnly, true));
        cut.Find("input[type=text]").GetAttribute("aria-expanded").Should().Be("false");
        cut.Find("input[type=text]").Input("2026-10-11 11:00 AM");
        cut.Instance.SelectedDateTime.Should().Be(minimum);
    }

    [Test]
    public void Audit_date_time_picker_reuses_accessible_calendar_at_date_bounds()
    {
        var cut = Render<DateTimePicker>(p => p.Add(c => c.SelectedDateTime, DateTime.MinValue));
        cut.Find("input[type=text]").Focus();
        cut.FindComponent<Calendar>().Should().NotBeNull();
        cut.FindAll("[role=grid] button[tabindex='0']").Should().ContainSingle();
        cut.Render(p => p.Add(c => c.SelectedDateTime, DateTime.MaxValue));
        cut.FindAll("[role=grid] button[tabindex='0']").Should().ContainSingle();
    }

    [Test]
    public void Date_time_picker_preserves_partial_edits_and_syncs_completed_input()
    {
        var initial = new DateTime(2026, 9, 21, 6, 41, 0);
        var changes = new List<DateTime?>();
        var cut = Render<DateTimePicker>(p => p
            .Add(x => x.SelectedDateTime, initial)
            .Add(x => x.SelectedDateTimeChanged, (DateTime? value) => changes.Add(value)));
        var input = cut.Find("input[type=text]");
        input.Focus();
        input.Input("2026-09-21 06:41 A");

        cut.Find("input[type=text]").GetAttribute("value").Should().Be("2026-09-21 06:41 A");
        cut.Instance.SelectedDateTime.Should().Be(initial);
        changes.Should().BeEmpty();
        cut.Find("input[type=text]").GetAttribute("aria-expanded").Should().Be("true");

        input.Input("2026-10-22 09:15 PM");
        cut.Instance.SelectedDateTime.Should().Be(new DateTime(2026, 10, 22, 21, 15, 0));
        cut.Find("input[aria-label=Hour]").GetAttribute("value").Should().Be("9");
        cut.Find("input[aria-label=Minute]").GetAttribute("value").Should().Be("15");
        cut.Find("button[title='Click to toggle']").TextContent.Should().Be("PM");

        cut.Find("button[title='Click to toggle']").Click();
        cut.Find("input[type=text]").GetAttribute("value").Should().Be("2026-10-22 09:15 AM");
        cut.Instance.SelectedDateTime.Should().Be(new DateTime(2026, 10, 22, 9, 15, 0));
    }

    [Test]
    public void Date_time_picker_clear_and_external_updates_sync_input()
    {
        var cut = Render<DateTimePicker>(p => p.Add(x => x.SelectedDateTime, new DateTime(2026, 9, 21, 6, 41, 0)));
        cut.Find("input[type=text]").Input("");
        cut.Instance.SelectedDateTime.Should().BeNull();
        cut.Find("input[type=text]").GetAttribute("value").Should().BeEmpty();

        cut.Render(p => p.Add(x => x.SelectedDateTime, new DateTime(2027, 1, 2, 12, 30, 0)));
        cut.Find("input[type=text]").GetAttribute("value").Should().Be("2027-01-02 12:30 PM");
    }
}
