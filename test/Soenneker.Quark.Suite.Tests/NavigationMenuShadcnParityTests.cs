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
        triggerClasses.Should().Contain("bg-background");
        triggerClasses.Should().Contain("px-4");
        triggerClasses.Should().Contain("py-2");
        triggerClasses.Should().Contain("transition-[color,box-shadow]");
        triggerClasses.Should().Contain("hover:bg-accent");
        triggerClasses.Should().Contain("hover:text-accent-foreground");
        triggerClasses.Should().Contain("focus:bg-accent");
        triggerClasses.Should().Contain("focus:text-accent-foreground");
        triggerClasses.Should().Contain("focus-visible:ring-[3px]");
        triggerClasses.Should().Contain("data-[state=open]:bg-accent/50");
        triggerClasses.Should().Contain("data-[state=open]:text-accent-foreground");
        triggerClasses.Should().NotContain("group/navigation-menu-trigger");
        triggerClasses.Should().NotContain("rounded-lg");
        triggerClasses.Should().NotContain("px-2.5");
        triggerClasses.Should().NotContain("py-1.5");
        triggerClasses.Should().NotContain("hover:bg-muted");
        triggerClasses.Should().NotContain("data-popup-open:bg-muted/50");
        triggerClasses.Should().NotContain("data-open:bg-muted/50");
        triggerClasses.Should().NotContain("q-navigation-menu-trigger");

        linkClasses.Should().Contain("flex");
        linkClasses.Should().Contain("flex-col");
        linkClasses.Should().Contain("gap-1");
        linkClasses.Should().Contain("rounded-sm");
        linkClasses.Should().Contain("p-2");
        linkClasses.Should().Contain("transition-all");
        linkClasses.Should().Contain("hover:bg-accent");
        linkClasses.Should().Contain("hover:text-accent-foreground");
        linkClasses.Should().Contain("focus:bg-accent");
        linkClasses.Should().Contain("focus:text-accent-foreground");
        linkClasses.Should().Contain("focus-visible:ring-[3px]");
        linkClasses.Should().Contain("data-[active=true]:bg-accent/50");
        linkClasses.Should().Contain("data-[active=true]:text-accent-foreground");
        linkClasses.Should().Contain("[&_svg:not([class*='size-'])]:size-4");
        linkClasses.Should().Contain("[&_svg:not([class*='text-'])]:text-muted-foreground");
        linkClasses.Should().NotContain("items-center");
        linkClasses.Should().NotContain("gap-2");
        linkClasses.Should().NotContain("rounded-lg");
        linkClasses.Should().NotContain("hover:bg-muted");
        linkClasses.Should().NotContain("data-active:bg-muted/50");
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
        trigger.Should().Contain("data-[state=open]:bg-accent/50");
        trigger.Should().NotContain("data-popup-open:bg-muted/50");
        trigger.Should().NotContain("data-open:bg-muted/50");

        content.Should().Contain("group-data-[viewport=false]/navigation-menu:overflow-hidden");
        content.Should().Contain("group-data-[viewport=false]/navigation-menu:rounded-md");
        content.Should().Contain("group-data-[viewport=false]/navigation-menu:border");
        content.Should().Contain("group-data-[viewport=false]/navigation-menu:data-[state=open]:animate-in");
        content.Should().Contain("**:data-[slot=navigation-menu-link]:focus:ring-0");
        content.Should().NotContain("data-[state=closed]:hidden");
        content.Should().NotContain("group-data-[viewport=false]/navigation-menu:ring-1");
        content.Should().NotContain("group-data-[viewport=false]/navigation-menu:data-open");
        content.Should().NotContain("group-data-[viewport=false]/navigation-menu:data-closed");
        content.Should().NotContain("rtl:data-[motion");

        viewport.Should().Contain("absolute top-full left-0 isolate z-50 flex justify-center");
        viewport.Should().Contain("Overflow ??= Quark.Overflow.Hidden");
        viewport.Should().Contain("Width ??= Quark.Width.IsFull");
        viewport.Should().Contain("Border ??= Quark.Border.Default");
        viewport.Should().Contain("h-[var(--radix-navigation-menu-viewport-height)]");
        viewport.Should().Contain("md:w-[var(--radix-navigation-menu-viewport-width)]");
        viewport.Should().NotContain("ring-1 ring-foreground/10");
        viewport.Should().NotContain("data-open");
        viewport.Should().NotContain("data-closed");
        viewport.Should().NotContain("rtl:left-auto");
    }

    [Test]
    public void NavigationMenu_active_link_uses_current_data_active_contract()
    {
        var cut = Render<NavigationMenu>(parameters => parameters
            .Add(p => p.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenComponent<NavigationMenuList>(0);
                builder.AddAttribute(1, "ChildContent", (RenderFragment)(listBuilder =>
                {
                    listBuilder.OpenComponent<NavigationMenuItem>(0);
                    listBuilder.AddAttribute(1, "ChildContent", (RenderFragment)(itemBuilder =>
                    {
                        itemBuilder.OpenComponent<NavigationMenuLink>(0);
                        itemBuilder.AddAttribute(1, "Href", "/docs");
                        itemBuilder.AddAttribute(2, "Active", true);
                        itemBuilder.AddAttribute(3, "ChildContent", (RenderFragment)(contentBuilder => contentBuilder.AddContent(0, "Docs")));
                        itemBuilder.CloseComponent();
                    }));
                    listBuilder.CloseComponent();
                }));
                builder.CloseComponent();
            })));

        var link = cut.Find("[data-slot='navigation-menu-link']");

        link.GetAttribute("data-active").Should().Be("true");
        link.GetAttribute("aria-current").Should().Be("page");
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
