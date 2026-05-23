using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Scrollspy_emits_reui_slot_and_preserves_anchor_attributes()
    {
        var cut = Render<Scrollspy>(parameters => parameters
            .Add(component => component.Offset, 96)
            .Add(component => component.Smooth, false)
            .Add(component => component.History, false)
            .Add(component => component.ChildContent, BuildScrollspyNav()));

        var scrollspy = cut.Find("[data-slot='scrollspy']");
        scrollspy.TagName.Should().Be("DIV");
        scrollspy.Children.Should().HaveCount(1);

        var anchor = cut.Find("[data-scrollspy-anchor='overview']");
        anchor.GetAttribute("href").Should().Be("#overview");
        anchor.GetAttribute("data-scrollspy-offset").Should().Be("96");
    }

    private static RenderFragment BuildScrollspyNav()
    {
        return builder =>
        {
            builder.OpenElement(0, "nav");
            builder.OpenElement(1, "a");
            builder.AddAttribute(2, "href", "#overview");
            builder.AddAttribute(3, "data-scrollspy-anchor", "overview");
            builder.AddAttribute(4, "data-scrollspy-offset", "96");
            builder.AddContent(5, "Overview");
            builder.CloseElement();
            builder.CloseElement();
        };
    }
}
