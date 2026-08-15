using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Image_without_alt_renders_decorative_alt()
    {
        var cut = Render<Image>(parameters => parameters.Add(p => p.Source, "/photo.jpg"));

        cut.Find("img").GetAttribute("alt").Should().Be(string.Empty);
    }

    [Test]
    public void Card_title_defaults_to_div_and_can_render_semantic_heading()
    {
        var neutral = Render<CardTitle>(parameters => parameters.Add(p => p.ChildContent, "Neutral title"));
        var semantic = Render<CardTitle>(parameters => parameters
            .Add(p => p.HeadingLevel, HeadingLevel.H2)
            .Add(p => p.ChildContent, "Semantic title"));

        neutral.Find("div[data-slot='card-title']").TextContent.Should().Be("Neutral title");
        semantic.Find("h2[data-slot='card-title']").TextContent.Should().Be("Semantic title");
    }

    [Test]
    public void Heading_level_is_independent_from_visual_scale()
    {
        var cut = Render<Heading>(parameters => parameters
            .Add(p => p.Level, HeadingLevel.H2)
            .Add(p => p.Scale, Quark.Scale.Scale105)
            .Add(p => p.ChildContent, "Section title"));

        cut.Find("h2[data-slot='heading']").TextContent.Should().Be("Section title");
    }

    [Test]
    public void Breadcrumb_page_is_current_text_not_disabled_link()
    {
        var cut = Render<BreadcrumbPage>(parameters => parameters.Add(p => p.ChildContent, "Current page"));
        var page = cut.Find("span[data-slot='breadcrumb-page']");

        page.GetAttribute("aria-current").Should().Be("page");
        page.HasAttribute("role").Should().BeFalse();
        page.HasAttribute("aria-disabled").Should().BeFalse();
    }

    [Test]
    public void Navigation_menu_force_mount_keeps_closed_links_in_markup()
    {
        var cut = Render<NavigationMenu>(parameters => parameters
            .Add(p => p.Viewport, false)
            .Add(p => p.ChildContent, BuildForceMountedNavigationContent()));

        cut.Find("a[href='/docs']").TextContent.Should().Be("Docs");
    }

    private static RenderFragment BuildForceMountedNavigationContent() => builder =>
    {
        builder.OpenComponent<NavigationMenuList>(0);
        builder.AddAttribute(1, nameof(NavigationMenuList.ChildContent), (RenderFragment) (listBuilder =>
        {
            listBuilder.OpenComponent<NavigationMenuItem>(0);
            listBuilder.AddAttribute(1, nameof(NavigationMenuItem.ChildContent), (RenderFragment) (itemBuilder =>
            {
                itemBuilder.OpenComponent<NavigationMenuTrigger>(0);
                itemBuilder.AddAttribute(1, nameof(NavigationMenuTrigger.ChildContent), (RenderFragment) (contentBuilder => contentBuilder.AddContent(0, "Products")));
                itemBuilder.CloseComponent();

                itemBuilder.OpenComponent<NavigationMenuContent>(2);
                itemBuilder.AddAttribute(3, nameof(NavigationMenuContent.ForceMount), true);
                itemBuilder.AddAttribute(4, nameof(NavigationMenuContent.ChildContent), (RenderFragment) (contentBuilder =>
                {
                    contentBuilder.OpenComponent<NavigationMenuLink>(0);
                    contentBuilder.AddAttribute(1, nameof(NavigationMenuLink.Href), "/docs");
                    contentBuilder.AddAttribute(2, nameof(NavigationMenuLink.ChildContent), (RenderFragment) (linkBuilder => linkBuilder.AddContent(0, "Docs")));
                    contentBuilder.CloseComponent();
                }));
                itemBuilder.CloseComponent();
            }));
            listBuilder.CloseComponent();
        }));
        builder.CloseComponent();
    };
}
