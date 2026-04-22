using System.Linq;
using Microsoft.AspNetCore.Components;
using AwesomeAssertions;
using Bunit;
using HeaderComponent = Soenneker.Quark.Header.Header;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Header_matches_shadcn_docs_shell_contract()
    {
        var cut = Render<HeaderComponent>(parameters => parameters
            .Add(p => p.ShowSidebarTrigger, true)
            .Add(p => p.BrandContent, (RenderFragment)(builder => builder.AddMarkupContent(0, "<a href=\"/\">Brand</a>")))
            .Add(p => p.NavigationContent, (RenderFragment)(builder => builder.AddMarkupContent(0, "<a href=\"/docs\">Docs</a>")))
            .Add(p => p.ActionsContent, (RenderFragment)(builder => builder.AddMarkupContent(0, "<button type=\"button\">Action</button>"))));

        var headerClasses = cut.Find("header").GetAttribute("class")!;
        var triggerClasses = cut.Find("[data-slot='sidebar-trigger']").GetAttribute("class")!;
        var nav = cut.Find("nav");
        var actions = cut.FindAll("div").First(div => (div.GetAttribute("class") ?? string.Empty).Contains("ml-auto"));

        headerClasses.Should().Contain("sticky");
        headerClasses.Should().Contain("top-0");
        headerClasses.Should().Contain("z-50");
        headerClasses.Should().Contain("w-full");
        headerClasses.Should().Contain("bg-background");
        headerClasses.Should().NotContain("border-b");

        triggerClasses.Should().Contain("touch-manipulation");
        triggerClasses.Should().Contain("hover:bg-transparent");
        triggerClasses.Should().Contain("lg:hidden");
        triggerClasses.Should().Contain("justify-start");
        triggerClasses.Should().NotContain("hover:bg-accent");

        nav.GetAttribute("class")!.Should().Contain("gap-0");
        actions.GetAttribute("class")!.Should().Contain("md:justify-end");
        cut.Markup.Should().Contain(">Menu<");
    }
}
