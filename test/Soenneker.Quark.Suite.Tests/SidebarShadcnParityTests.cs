using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
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
}
