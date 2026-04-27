using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;
using System;
using System.IO;


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
        listClasses.Should().Contain("gap-1");
        listClasses.Should().NotContain("q-navigation-menu-list");

        itemClasses.Should().Contain("relative");
        itemClasses.Should().NotContain("q-navigation-menu-item");

        triggerClasses.Should().Contain("group");
        triggerClasses.Should().Contain("rounded-md");
        triggerClasses.Should().Contain("px-4");
        triggerClasses.Should().Contain("py-2");
        triggerClasses.Should().Contain("transition-[color,shadow]");
        triggerClasses.Should().Contain("hover:bg-accent");
        triggerClasses.Should().Contain("focus-visible:ring-[3px]");
        triggerClasses.Should().Contain("data-popup-open:bg-accent/50");
        triggerClasses.Should().Contain("data-open:bg-accent/50");
        triggerClasses.Should().Contain("bg-background");
        triggerClasses.Should().Contain("hover:bg-accent");
        triggerClasses.Should().NotContain("q-navigation-menu-trigger");

        linkClasses.Should().Contain("flex");
        linkClasses.Should().Contain("gap-1");
        linkClasses.Should().Contain("rounded-sm");
        linkClasses.Should().Contain("p-2");
        linkClasses.Should().Contain("transition-all");
        linkClasses.Should().Contain("hover:bg-accent");
        linkClasses.Should().Contain("focus-visible:ring-[3px]");
        linkClasses.Should().Contain("data-active:bg-accent/50");
        linkClasses.Should().Contain("data-open:bg-accent/50");
        linkClasses.Should().Contain("[&_svg:not([class*='text-'])]:text-muted-foreground");
        linkClasses.Should().Contain("hover:bg-accent");
        linkClasses.Should().NotContain("q-navigation-menu-link");
    }

    [Test]
    public void NavigationMenu_content_viewport_and_trigger_source_match_shadcn_v4()
    {
        var root = ReadNavigationMenuSource("NavigationMenu.razor");
        var trigger = ReadNavigationMenuSource("NavigationMenuTrigger.razor");
        var content = ReadNavigationMenuSource("NavigationMenuContent.razor");
        var viewport = ReadNavigationMenuSource("NavigationMenuViewport.razor");

        root.Should().Contain("@ChildContent");
        root.IndexOf("@ChildContent", StringComparison.Ordinal).Should().BeLessThan(root.IndexOf("<NavigationMenuViewport />", StringComparison.Ordinal));

        trigger.Should().Contain("group-data-[state=open]:rotate-180");
        trigger.Should().Contain("aria-hidden=\"true\"");
        trigger.Should().Contain("Padding ??= Quark.Padding.Is4.OnX.Is2.OnY");
        trigger.Should().Contain("BackgroundColor ??= Quark.BackgroundColor.Background");

        content.Should().Contain("group-data-[viewport=false]/navigation-menu:overflow-hidden");
        content.Should().Contain("group-data-[viewport=false]/navigation-menu:rounded-md");
        content.Should().Contain("**:data-[slot=navigation-menu-link]:focus:ring-0");
        content.Should().NotContain("data-[state=closed]:hidden");
        content.Should().NotContain("rtl:data-[motion");

        viewport.Should().Contain("absolute top-full left-1/2 isolate z-50 flex w-[calc(100vw-2rem)] -translate-x-1/2 justify-center md:left-0 md:w-auto md:translate-x-0");
        viewport.Should().Contain("Overflow ??= Quark.Overflow.Hidden");
        viewport.Should().Contain("Width ??= Quark.Width.IsFull");
        viewport.Should().Contain("md:w-[var(--radix-navigation-menu-viewport-width)]");
        viewport.Should().NotContain("rtl:left-auto");
    }

    private static string ReadNavigationMenuSource(string fileName)
    {
        return File.ReadAllText(Path.Combine(GetSuiteRootForNavigationMenu(), "src", "Soenneker.Quark.Suite", "Components", "NavigationMenu", fileName));
    }

    private static string GetSuiteRootForNavigationMenu()
    {
        var directory = AppContext.BaseDirectory;

        for (var i = 0; i < 6; i++)
        {
            directory = Directory.GetParent(directory)!.FullName;
        }

        return directory;
    }
}
