using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void SidebarContent_can_fade_scroll_edges()
    {
        var cut = Render<SidebarContent>(parameters => parameters
            .Add(component => component.ShowFade, true)
            .Add(component => component.ChildContent, (RenderFragment) (builder => builder.AddContent(0, "Navigation"))));

        var content = cut.Find("[data-sidebar='content']");
        var style = content.GetAttribute("style");

        content.GetAttribute("data-fade").Should().Be("true");
        style.Should().Contain("mask-image:linear-gradient(to bottom,transparent,black 4rem,black calc(100% - 4rem),transparent)");
        style.Should().Contain("-webkit-mask-image:linear-gradient(to bottom,transparent,black 4rem,black calc(100% - 4rem),transparent)");
        cut.Render(parameters => parameters.Add(component => component.ShowFade, false));
        cut.Find("[data-sidebar='content']").GetAttribute("style").Should().BeNullOrEmpty();
    }
}
