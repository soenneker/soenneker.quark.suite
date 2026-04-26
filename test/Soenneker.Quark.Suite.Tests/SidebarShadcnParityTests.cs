using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;
using System;
using System.IO;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void SidebarProvider_matches_shadcn_wrapper_contract()
    {
        var provider = Render<SidebarProvider>(parameters => parameters
            .Add(p => p.PersistState, false)
            .Add(p => p.ChildContent, (RenderFragment)(builder => builder.AddContent(0, "Content"))));

        var wrapper = provider.Find("[data-slot='sidebar-wrapper']");
        var classes = wrapper.GetAttribute("class")!;
        var style = wrapper.GetAttribute("style")!;

        classes.Should().Contain("group/sidebar-wrapper");
        classes.Should().Contain("flex");
        classes.Should().Contain("min-h-svh");
        classes.Should().Contain("w-full");
        classes.Should().Contain("has-data-[variant=inset]:bg-sidebar");
        style.Should().Contain("--sidebar-width: 16rem");
        style.Should().Contain("--sidebar-width-icon: 3rem");
        style.Should().Contain("--sidebar-width-mobile: 18rem");
    }

    [Test]
    public void Sidebar_matches_shadcn_base_classes()
    {
        var sidebar = Render<Sidebar>(parameters => parameters
            .Add(p => p.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenComponent<SidebarHeader>(0);
                builder.AddAttribute(1, "ChildContent", (RenderFragment)(contentBuilder => contentBuilder.AddContent(0, "Workspace")));
                builder.CloseComponent();

                builder.OpenComponent<SidebarContent>(2);
                builder.AddAttribute(3, "ChildContent", (RenderFragment)(contentBuilder =>
                {
                    contentBuilder.OpenComponent<SidebarGroup>(0);
                    contentBuilder.AddAttribute(1, "ChildContent", (RenderFragment)(groupBuilder =>
                    {
                        groupBuilder.OpenComponent<SidebarGroupLabel>(0);
                        groupBuilder.AddAttribute(1, "ChildContent", (RenderFragment)(labelBuilder => labelBuilder.AddContent(0, "Platform")));
                        groupBuilder.CloseComponent();

                        groupBuilder.OpenComponent<SidebarGroupContent>(2);
                        groupBuilder.AddAttribute(3, "ChildContent", (RenderFragment)(groupContentBuilder =>
                        {
                            groupContentBuilder.OpenComponent<SidebarMenu>(0);
                            groupContentBuilder.AddAttribute(1, "ChildContent", (RenderFragment)(menuBuilder =>
                            {
                                menuBuilder.OpenComponent<SidebarMenuItem>(0);
                                menuBuilder.AddAttribute(1, "ChildContent", (RenderFragment)(itemBuilder =>
                                {
                                    itemBuilder.OpenComponent<SidebarMenuButton>(0);
                                    itemBuilder.AddAttribute(1, "Active", true);
                                    itemBuilder.AddAttribute(2, "ChildContent", (RenderFragment)(buttonBuilder => buttonBuilder.AddContent(0, "Dashboard")));
                                    itemBuilder.CloseComponent();

                                    itemBuilder.OpenComponent<SidebarMenuAction>(3);
                                    itemBuilder.AddAttribute(4, "ChildContent", (RenderFragment)(actionBuilder => actionBuilder.AddContent(0, "+")));
                                    itemBuilder.CloseComponent();

                                    itemBuilder.OpenComponent<SidebarMenuBadge>(5);
                                    itemBuilder.AddAttribute(6, "ChildContent", (RenderFragment)(badgeBuilder => badgeBuilder.AddContent(0, "4")));
                                    itemBuilder.CloseComponent();
                                }));
                                menuBuilder.CloseComponent();
                            }));
                            groupContentBuilder.CloseComponent();
                        }));
                        groupBuilder.CloseComponent();
                    }));
                    contentBuilder.CloseComponent();
                }));
                builder.CloseComponent();
            })));

        var sidebarClasses = sidebar.Find("[data-slot='sidebar']").GetAttribute("class")!;
        var gapClasses = sidebar.Find("[data-slot='sidebar-gap']").GetAttribute("class")!;
        var containerClasses = sidebar.Find("[data-slot='sidebar-container']").GetAttribute("class")!;
        var innerClasses = sidebar.Find("[data-slot='sidebar-inner']").GetAttribute("class")!;

        sidebarClasses.Should().Contain("group");
        sidebarClasses.Should().Contain("peer");
        sidebarClasses.Should().Contain("hidden");
        sidebarClasses.Should().Contain("text-sidebar-foreground");
        sidebarClasses.Should().Contain("md:block");
        sidebarClasses.Should().NotContain("q-sidebar");

        gapClasses.Should().Contain("relative");
        gapClasses.Should().Contain("transition-[width]");
        gapClasses.Should().Contain("group-data-[collapsible=offcanvas]:w-0");

        containerClasses.Should().Contain("fixed");
        containerClasses.Should().Contain("inset-y-0");
        containerClasses.Should().Contain("md:flex");

        innerClasses.Should().Contain("flex");
        innerClasses.Should().Contain("h-full");
        innerClasses.Should().Contain("w-full");
        innerClasses.Should().Contain("bg-sidebar");
    }

    [Test]
    public void Sidebar_right_floating_and_inset_variants_match_shadcn_contract()
    {
        var right = Render<Sidebar>(parameters => parameters
            .Add(p => p.Side, SidebarSide.Right)
            .Add(p => p.ChildContent, (RenderFragment)(builder => builder.AddContent(0, "Right"))));

        var rightRoot = right.Find("[data-slot='sidebar']");
        var rightContainerClasses = right.Find("[data-slot='sidebar-container']").GetAttribute("class")!;

        rightRoot.GetAttribute("data-side").Should().Be("right");
        rightContainerClasses.Should().Contain("right-0");
        rightContainerClasses.Should().Contain("group-data-[collapsible=offcanvas]:right-[calc(var(--sidebar-width)*-1)]");
        rightContainerClasses.Should().NotContain("left-0");

        var floating = Render<Sidebar>(parameters => parameters
            .Add(p => p.Variant, SidebarVariant.Floating)
            .Add(p => p.Collapsible, SidebarCollapsible.Icon)
            .Add(p => p.ChildContent, (RenderFragment)(builder => builder.AddContent(0, "Floating"))));

        var floatingRoot = floating.Find("[data-slot='sidebar']");
        var floatingGapClasses = floating.Find("[data-slot='sidebar-gap']").GetAttribute("class")!;
        var floatingContainerClasses = floating.Find("[data-slot='sidebar-container']").GetAttribute("class")!;
        var floatingInnerClasses = floating.Find("[data-slot='sidebar-inner']").GetAttribute("class")!;

        floatingRoot.GetAttribute("data-variant").Should().Be("floating");
        floatingGapClasses.Should().Contain("group-data-[collapsible=icon]:w-[calc(var(--sidebar-width-icon)+(--spacing(4)))]");
        floatingContainerClasses.Should().Contain("p-2");
        floatingContainerClasses.Should().Contain("group-data-[collapsible=icon]:w-[calc(var(--sidebar-width-icon)+(--spacing(4))+2px)]");
        floatingInnerClasses.Should().Contain("group-data-[variant=floating]:rounded-lg");
        floatingInnerClasses.Should().Contain("group-data-[variant=floating]:border");
        floatingInnerClasses.Should().Contain("group-data-[variant=floating]:shadow-sm");

        var inset = Render<Sidebar>(parameters => parameters
            .Add(p => p.Variant, SidebarVariant.Inset)
            .Add(p => p.Collapsible, SidebarCollapsible.Icon)
            .Add(p => p.ChildContent, (RenderFragment)(builder => builder.AddContent(0, "Inset"))));

        var insetRoot = inset.Find("[data-slot='sidebar']");
        var insetContainerClasses = inset.Find("[data-slot='sidebar-container']").GetAttribute("class")!;

        insetRoot.GetAttribute("data-variant").Should().Be("inset");
        insetContainerClasses.Should().Contain("p-2");
        insetContainerClasses.Should().Contain("group-data-[collapsible=icon]:w-[calc(var(--sidebar-width-icon)+(--spacing(4))+2px)]");
    }

    [Test]
    public void Sidebar_full_screen_mobile_mode_overrides_sheet_side_width()
    {
        var source = File.ReadAllText(Path.Combine(GetSuiteRootForSidebar(), "src", "Soenneker.Quark.Suite", "Components", "Sidebar", "Sidebar.razor"));

        source.Should().Contain("w-screen!");
        source.Should().Contain("max-w-none!");
        source.Should().Contain("sm:max-w-none!");
        source.Should().NotContain("w-screen max-w-none rounded-none border-0 sm:max-w-none");
    }

    private static string GetSuiteRootForSidebar()
    {
        var directory = AppContext.BaseDirectory;

        while (!File.Exists(Path.Combine(directory, "Soenneker.Quark.Suite.slnx")))
            directory = Directory.GetParent(directory)!.FullName;

        return directory;
    }
}
