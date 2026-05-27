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
        var cls = content.GetAttribute("class");

        content.GetAttribute("data-fade").Should().Be("true");
        cls.Should().Contain("[mask-image:linear-gradient(to_bottom,transparent,black_4rem,black_calc(100%_-_4rem),transparent)]");
        cls.Should().Contain("[-webkit-mask-image:linear-gradient(to_bottom,transparent,black_4rem,black_calc(100%_-_4rem),transparent)]");
    }
}
