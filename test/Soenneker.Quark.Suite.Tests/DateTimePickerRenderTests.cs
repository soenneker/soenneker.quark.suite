using System;
using System.Collections.Generic;
using AwesomeAssertions;
using Bunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
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
