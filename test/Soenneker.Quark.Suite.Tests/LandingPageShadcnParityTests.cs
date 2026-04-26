using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Main_layout_landing_branch_provides_sidebar_context()
    {
        var layout = Render<global::Soenneker.Quark.Suite.Demo.MainLayout>(parameters => parameters
            .Add(p => p.Body, (RenderFragment)(builder =>
            {
                builder.OpenElement(0, "div");
                builder.AddAttribute(1, "id", "layout-body-probe");
                builder.CloseElement();
            })));

        var root = layout.Find("[data-slot='sidebar-wrapper']");
        root.GetAttribute("class")!.Should().ContainAll("group/sidebar-wrapper", "flex", "min-h-svh", "flex-col", "bg-background");
        layout.Find("[data-slot='sidebar']");

        layout.Find("#layout-body-probe");
    }

    [Test]
    public void Index_matches_shadcn_page_contracts()
    {
        var page = Render<global::Soenneker.Quark.Suite.Demo.Pages.Index>();

        page.Find("div.flex.flex-1.flex-col");
        page.Find("section.border-grid");
        page.Find("div.mx-auto.w-full.flex-1.pb-6");
        page.Markup.Should().Contain("heard about us.");
        page.Markup.Should().Contain("href=\"components\"");
        page.Markup.Should().NotContain("href=\"docs/components\"");
    }

    [Test]
    public void Index_uses_default_layout_so_sidebar_trigger_is_wired()
    {
        var source = System.IO.File.ReadAllText(System.IO.Path.Combine(GetSuiteRootForLandingPage(), "test", "Soenneker.Quark.Suite.Demo", "Pages", "Index.razor"));

        source.Should().NotContain("@layout LandingLayout");
    }

    [Test]
    public void Main_layout_mobile_sidebar_offset_survives_portal_rendering()
    {
        var source = System.IO.File.ReadAllText(System.IO.Path.Combine(GetSuiteRootForLandingPage(), "test", "Soenneker.Quark.Suite.Demo", "MainLayout.razor"));

        source.Should().Contain("MobileOffsetTop=\"4.5rem\"");
        source.Should().NotContain("MobileOffsetTop=\"var(--header-height)\"");
    }

    private static string GetSuiteRootForLandingPage()
    {
        var directory = System.AppContext.BaseDirectory;

        while (!System.IO.File.Exists(System.IO.Path.Combine(directory, "Soenneker.Quark.Suite.slnx")))
            directory = System.IO.Directory.GetParent(directory)!.FullName;

        return directory;
    }
}
