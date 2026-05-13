using System;
using AwesomeAssertions;
using Bunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Calendar_dropdown_month_uses_destination_culture_name()
    {
        var cut = Render<Calendar>(parameters => parameters
            .Add(component => component.CaptionLayout, "dropdown")
            .Add(component => component.DisplayMonth, new DateOnly(2026, 4, 1))
            .Add(component => component.CultureName, "es-ES"));

        cut.Find("select.rdp-months_dropdown option[value='3']").TextContent.Should().Be("abr");
        cut.Find(".rdp-caption_label span").TextContent.Should().Be("abr");
    }

    [Test]
    public void Calendar_label_caption_month_uses_destination_culture_name()
    {
        var cut = Render<Calendar>(parameters => parameters
            .Add(component => component.DisplayMonth, new DateOnly(2026, 4, 1))
            .Add(component => component.CultureName, "es-ES"));

        cut.Find("table.rdp-month_grid").GetAttribute("aria-label").Should().Be("abril 2026");
        cut.Find(".rdp-caption_label").TextContent.Should().Be("abril 2026");
    }

    [Test]
    public void Calendar_month_navigation_is_positioned_in_caption_row()
    {
        var cut = Render<Calendar>(parameters => parameters
            .Add(component => component.DisplayMonth, new DateOnly(2026, 5, 1)));

        string navClass = cut.Find("nav.rdp-nav").GetAttribute("class")!;

        navClass.Should().Contain("absolute");
        navClass.Should().Contain("top-0");
        navClass.Should().Contain("inset-x-0");
        navClass.Should().NotContain("inset-0");
    }

    [Test]
    public void Calendar_rtl_navigation_chevrons_keep_physical_direction()
    {
        var cut = Render<Calendar>(parameters => parameters
            .Add(component => component.DisplayMonth, new DateOnly(2026, 5, 1))
            .Add(component => component.Dir, "rtl"));

        string calendarClass = cut.Find("[data-slot='calendar']").GetAttribute("class")!;

        calendarClass.Should().NotContain("rdp-button_next>svg");
        calendarClass.Should().NotContain("rdp-button_previous>svg");
        cut.Find(".rdp-button_previous svg").GetAttribute("class").Should().NotContain("cn-rtl-flip");
        cut.Find(".rdp-button_next svg").GetAttribute("class").Should().NotContain("cn-rtl-flip");
    }
}
