using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void SidebarRail_matches_current_rtl_positioning_contract()
    {
        var cut = Render<SidebarProvider>(parameters => parameters
            .Add(p => p.PersistState, false)
            .Add(p => p.ChildContent, (builder) =>
            {
                builder.OpenComponent<Sidebar>(0);
                builder.AddAttribute(1, "ChildContent", (RenderFragment)(contentBuilder =>
                {
                    contentBuilder.OpenComponent<SidebarRail>(0);
                    contentBuilder.CloseComponent();
                }));
                builder.CloseComponent();
            }));

        var classes = cut.Find("[data-slot='sidebar-rail']").GetAttribute("class")!;

        classes.Should().Contain("group-data-[side=left]:-right-4");
        classes.Should().Contain("group-data-[side=right]:left-0");
        classes.Should().Contain("after:start-1/2");
        classes.Should().Contain("ltr:-translate-x-1/2");
        classes.Should().Contain("rtl:-translate-x-1/2");
        classes.Should().NotContain("after:left-1/2");
    }
}
