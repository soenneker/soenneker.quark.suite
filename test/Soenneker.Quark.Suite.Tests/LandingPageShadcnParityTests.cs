using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Main_layout_landing_branch_hides_desktop_sidebar()
    {
        var layout = Render<Demo.MainLayout>(parameters => parameters
            .Add(p => p.Body, (RenderFragment)(builder =>
            {
                builder.OpenElement(0, "div");
                builder.AddAttribute(1, "id", "layout-body-probe");
                builder.CloseElement();
            })));

        var root = layout.Find("[data-slot='sidebar-wrapper']");
        root.GetAttribute("class")!.Should().ContainAll("group/sidebar-wrapper", "flex", "min-h-svh", "flex-col", "bg-background");
        layout.FindAll("[data-slot='sidebar']").Should().BeEmpty();

        layout.Find("#layout-body-probe");
    }

    [Test]
    public void Index_matches_shadcn_page_contracts()
    {
        var page = Render<Demo.Pages.Index>();

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

    [Test]
    public void Main_layout_docs_sidebar_hides_native_scrollbar()
    {
        var source = System.IO.File.ReadAllText(System.IO.Path.Combine(GetSuiteRootForLandingPage(), "test", "Soenneker.Quark.Suite.Demo", "MainLayout.razor"));

        source.Should().Contain("<SidebarContent HideScrollbar=\"true\">");
    }

    [Test]
    public void Demo_host_uses_local_base_href_and_publish_rewrites_it_for_github_pages()
    {
        var root = GetSuiteRootForLandingPage();
        var index = System.IO.File.ReadAllText(System.IO.Path.Combine(root, "test", "Soenneker.Quark.Suite.Demo", "wwwroot", "index.html"));
        var deployWorkflow = System.IO.File.ReadAllText(System.IO.Path.Combine(root, ".github", "workflows", "deploy-demo.yml"));

        index.Should().Contain("<base href=\"/\" />");
        index.Should().NotContain("%BASE_URL%");
        deployWorkflow.Should().Contain("s|<base href=\"/\" />|<base href=\"/${{ github.event.repository.name }}/\" />|g");
        deployWorkflow.Should().NotContain("%BASE_URL%");
    }

    private static string GetSuiteRootForLandingPage()
    {
        var directory = System.AppContext.BaseDirectory;

        while (!System.IO.File.Exists(System.IO.Path.Combine(directory, "Soenneker.Quark.Suite.slnx")))
            directory = System.IO.Directory.GetParent(directory)!.FullName;

        return directory;
    }
}
