using System;
using AwesomeAssertions;
using Bunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Cascader_updates_selected_labels_when_array_contents_change()
    {
        CascaderOption[] children = [new() { Value = "child", Label = "Before" }];
        CascaderOption[] options = [new() { Value = "root", Label = "Root", Children = children }];
        var cut = Render<Cascader>(p => p.Add(x => x.Options, options).Add(x => x.Value, ["root", "child"]));
        cut.Find("button").TextContent.Should().Contain("Root / Before");
        children[0] = new() { Value = "child", Label = "After" };
        cut.Render(p => p.Add(x => x.Options, options).Add(x => x.Value, ["root", "child"]));
        cut.Find("button").TextContent.Should().Contain("Root / After");
    }

    [Test]
    public void Calendar_updates_disabled_cells_when_array_contents_change()
    {
        var month = new DateOnly(2026, 5, 1);
        DateOnly[] disabled = [month.AddDays(9)];
        var cut = Render<Calendar>(p => p.Add(x => x.DisplayMonth, month).Add(x => x.DisabledDates, disabled));
        cut.Find("td[data-day='2026-05-10'] button").HasAttribute("disabled").Should().BeTrue();
        disabled[0] = month.AddDays(10);
        cut.Render(p => p.Add(x => x.DisplayMonth, month).Add(x => x.DisabledDates, disabled));
        cut.Find("td[data-day='2026-05-10'] button").HasAttribute("disabled").Should().BeFalse();
        cut.Find("td[data-day='2026-05-11'] button").HasAttribute("disabled").Should().BeTrue();
    }
}
