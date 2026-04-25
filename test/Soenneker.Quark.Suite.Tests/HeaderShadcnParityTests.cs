using System.Linq;
using Microsoft.AspNetCore.Components;
using AwesomeAssertions;
using Bunit;
using System;
using System.IO;
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
        var containerClasses = cut.Find("header > div").GetAttribute("class")!;
        var triggerClasses = cut.Find("[data-slot='sidebar-trigger']").GetAttribute("class")!;
        var nav = cut.Find("nav");
        var actions = cut.FindAll("div").First(div => (div.GetAttribute("class") ?? string.Empty).Contains("ml-auto"));

        headerClasses.Should().Contain("sticky");
        headerClasses.Should().Contain("top-0");
        headerClasses.Should().Contain("z-50");
        headerClasses.Should().Contain("w-full");
        headerClasses.Should().Contain("bg-background");
        headerClasses.Should().NotContain("border-b");

        containerClasses.Should().Contain("mx-auto");
        containerClasses.Should().Contain("w-full");
        containerClasses.Should().Contain("max-w-[1400px]");
        containerClasses.Should().Contain("px-2");
        containerClasses.Should().Contain("px-6");
        containerClasses.Should().Contain("container-wrapper");

        triggerClasses.Should().Contain("touch-manipulation");
        triggerClasses.Should().Contain("hover:bg-transparent");
        triggerClasses.Should().Contain("lg:hidden");
        triggerClasses.Should().Contain("justify-start");
        triggerClasses.Should().NotContain("hover:bg-accent");

        nav.GetAttribute("class")!.Should().Contain("gap-0");
        actions.GetAttribute("class")!.Should().Contain("md:justify-end");
        triggerClasses.Should().Contain("gap-2.5");
        cut.Markup.Should().Contain(">Menu<");
    }

    [Test]
    public void Main_layout_mobile_trigger_uses_shadcn_menu_label()
    {
        var source = File.ReadAllText(Path.Combine(GetSuiteRootForHeader(), "test", "Soenneker.Quark.Suite.Demo", "MainLayout.razor"));

        source.Should().NotContain("SidebarTriggerText=\"Navigation\"");
        source.Should().Contain("<Header ShowSidebarTrigger=\"@ShowSidebarTrigger\">");
    }

    private static string GetSuiteRootForHeader()
    {
        var directory = AppContext.BaseDirectory;

        for (var i = 0; i < 6; i++)
        {
            directory = Directory.GetParent(directory)!.FullName;
        }

        return directory;
    }
}
