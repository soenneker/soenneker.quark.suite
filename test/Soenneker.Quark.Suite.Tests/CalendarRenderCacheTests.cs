using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public async ValueTask Calendar_cached_context_observes_mutated_modifiers_and_preserves_previous_snapshot()
    {
        var date = new DateOnly(2026, 9, 15);
        bool highlighted = true;
        var contexts = new Dictionary<DateOnly, CalendarDayContext>();
        RenderFragment<CalendarDayContext> template = context => builder =>
        {
            contexts[context.Date] = context;
            builder.AddContent(0, context.Date.Day);
        };
        var modifiers = new Dictionary<string, Func<DateOnly, bool>> { ["highlight"] = day => day == date && highlighted };
        var classes = new Dictionary<string, string> { ["highlight"] = "initial-highlight" };
        var cut = Render<Calendar>(parameters => parameters
            .Add(component => component.DisplayMonth, new DateOnly(2026, 9, 1))
            .Add(component => component.Modifiers, modifiers)
            .Add(component => component.ModifierClasses, classes)
            .Add(component => component.DayContent, template));
        var original = contexts[date];
        await cut.InvokeAsync(cut.Instance.Refresh);
        contexts[date].Should().BeSameAs(original);
        classes["highlight"] = "changed-highlight";
        await cut.InvokeAsync(cut.Instance.Refresh);
        cut.Find("td[data-day='2026-09-15']").ClassList.Should().Contain("changed-highlight");
        highlighted = false;
        await cut.InvokeAsync(cut.Instance.Refresh);
        contexts[date].Modifiers.Should().BeEmpty();
        original.Modifiers.Should().Equal("highlight");
        cut.Find("td[data-day='2026-09-15']").ClassList.Should().NotContain("changed-highlight");
    }

    [Test]
    public void Calendar_cached_grid_observes_same_array_disabled_date_changes()
    {
        var dates = new DateOnly[9];
        for (int i = 0; i < dates.Length; i++) dates[i] = new DateOnly(2026, 9, i + 1);
        var cut = Render<Calendar>(parameters => parameters
            .Add(component => component.DisplayMonth, new DateOnly(2026, 9, 1))
            .Add(component => component.DisabledDates, dates));
        cut.Find("td[data-day='2026-09-01'] button").HasAttribute("disabled").Should().BeTrue();
        dates[0] = new DateOnly(2026, 9, 15);
        cut.Render(parameters => parameters.Add(component => component.DisabledDates, dates));
        cut.Find("td[data-day='2026-09-01'] button").HasAttribute("disabled").Should().BeFalse();
        cut.Find("td[data-day='2026-09-15'] button").HasAttribute("disabled").Should().BeTrue();
    }
}
