using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void NavigationMenu_slots_match_shadcn_base_classes()
    {
        var cut = Render<NavigationMenu>(parameters => parameters
            .Add(p => p.Viewport, true)
            .Add(p => p.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenComponent<NavigationMenuList>(0);
                builder.AddAttribute(1, "ChildContent", (RenderFragment)(listBuilder =>
                {
                    listBuilder.OpenComponent<NavigationMenuItem>(0);
                    listBuilder.AddAttribute(1, "ChildContent", (RenderFragment)(itemBuilder =>
                    {
                        itemBuilder.OpenComponent<NavigationMenuTrigger>(0);
                        itemBuilder.AddAttribute(1, "ChildContent", (RenderFragment)(contentBuilder => contentBuilder.AddContent(0, "Getting started")));
                        itemBuilder.CloseComponent();
                    }));
                    listBuilder.CloseComponent();

                    listBuilder.OpenComponent<NavigationMenuItem>(2);
                    listBuilder.AddAttribute(3, "ChildContent", (RenderFragment)(itemBuilder =>
                    {
                        itemBuilder.OpenComponent<NavigationMenuLink>(0);
                        itemBuilder.AddAttribute(1, "Href", "/docs");
                        itemBuilder.AddAttribute(2, "ChildContent", (RenderFragment)(contentBuilder => contentBuilder.AddContent(0, "Docs")));
                        itemBuilder.CloseComponent();
                    }));
                    listBuilder.CloseComponent();
                }));
                builder.CloseComponent();
            })));

        var rootClasses = cut.Find("[data-slot='navigation-menu']").GetAttribute("class")!;
        var listClasses = cut.Find("[data-slot='navigation-menu-list']").GetAttribute("class")!;
        var itemClasses = cut.Find("[data-slot='navigation-menu-item']").GetAttribute("class")!;
        var triggerClasses = cut.Find("[data-slot='navigation-menu-trigger']").GetAttribute("class")!;
        var linkClasses = cut.Find("[data-slot='navigation-menu-link']").GetAttribute("class")!;

        rootClasses.Should().Contain("group/navigation-menu");
        rootClasses.Should().Contain("relative");
        rootClasses.Should().Contain("flex");
        rootClasses.Should().Contain("max-w-max");
        rootClasses.Should().NotContain("q-navigation-menu");

        listClasses.Should().Contain("group");
        listClasses.Should().Contain("flex");
        listClasses.Should().Contain("list-none");
        listClasses.Should().Contain("gap-0");
        listClasses.Should().NotContain("q-navigation-menu-list");

        itemClasses.Should().Contain("relative");
        itemClasses.Should().NotContain("q-navigation-menu-item");

        triggerClasses.Should().Contain("group/navigation-menu-trigger");
        triggerClasses.Should().Contain("rounded-lg");
        triggerClasses.Should().Contain("px-2.5");
        triggerClasses.Should().Contain("py-1.5");
        triggerClasses.Should().Contain("hover:bg-muted");
        triggerClasses.Should().Contain("data-popup-open:bg-muted/50");
        triggerClasses.Should().Contain("data-open:bg-muted/50");
        triggerClasses.Should().NotContain("q-navigation-menu-trigger");

        linkClasses.Should().Contain("group/navigation-menu-trigger");
        linkClasses.Should().Contain("gap-2");
        linkClasses.Should().Contain("p-2");
        linkClasses.Should().Contain("in-data-[slot=navigation-menu-content]:rounded-md");
        linkClasses.Should().Contain("data-active:bg-muted/50");
        linkClasses.Should().NotContain("q-navigation-menu-link");
    }
}
