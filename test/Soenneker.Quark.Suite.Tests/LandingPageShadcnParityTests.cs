using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Landing_layout_matches_shadcn_root_contract()
    {
        var layout = Render<global::Soenneker.Quark.Suite.Demo.LandingLayout>(parameters => parameters
            .Add(p => p.Body, (RenderFragment)(builder =>
            {
                builder.OpenElement(0, "div");
                builder.AddAttribute(1, "id", "layout-body-probe");
                builder.CloseElement();
            })));

        var root = layout.Find("[data-slot='layout']");
        root.GetAttribute("class")!.Should().ContainAll("group/layout", "relative", "z-10", "flex", "min-h-svh", "flex-col", "bg-background");

        layout.Find("#layout-body-probe");
    }

    [Test]
    public void Index_matches_shadcn_page_contracts()
    {
        var page = Render<global::Soenneker.Quark.Suite.Demo.Pages.Index>();

        page.Find("div.flex.flex-1.flex-col");
        page.Find("section.border-grid");
        page.Find("div.container-wrapper.flex-1.pb-6");
        page.Markup.Should().Contain("heard about us.");
    }
}
