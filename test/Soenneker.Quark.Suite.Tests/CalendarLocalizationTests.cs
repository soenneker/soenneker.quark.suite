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
}
