using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void Tabs_slots_match_shadcn_base_classes()
    {
        var cut = Render<Tabs>(parameters => parameters
            .Add(p => p.SelectedTab, "overview")
            .Add(p => p.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenComponent<TabsList>(0);
                builder.AddAttribute(1, "ChildContent", (RenderFragment)(listBuilder =>
                {
                    listBuilder.OpenComponent<TabsTrigger>(0);
                    listBuilder.AddAttribute(1, "Value", "overview");
                    listBuilder.AddAttribute(2, "ChildContent", (RenderFragment)(contentBuilder => contentBuilder.AddContent(0, "Overview")));
                    listBuilder.CloseComponent();

                    listBuilder.OpenComponent<TabsTrigger>(3);
                    listBuilder.AddAttribute(4, "Value", "analytics");
                    listBuilder.AddAttribute(5, "ChildContent", (RenderFragment)(contentBuilder => contentBuilder.AddContent(0, "Analytics")));
                    listBuilder.CloseComponent();
                }));
                builder.CloseComponent();

                builder.OpenComponent<TabsContent>(2);
                builder.AddAttribute(3, "Value", "overview");
                builder.AddAttribute(4, "ChildContent", (RenderFragment)(contentBuilder => contentBuilder.AddContent(0, "Summary")));
                builder.CloseComponent();

                builder.OpenComponent<TabsContent>(5);
                builder.AddAttribute(6, "Value", "analytics");
                builder.AddAttribute(7, "ChildContent", (RenderFragment)(contentBuilder => contentBuilder.AddContent(0, "Charts")));
                builder.CloseComponent();
            })));

        string tabsClasses = cut.Find("[data-slot='tabs']").GetAttribute("class")!;
        string listClasses = cut.Find("[data-slot='tabs-list']").GetAttribute("class")!;
        string triggerClasses = cut.Find("[data-slot='tabs-trigger']").GetAttribute("class")!;
        string contentClasses = cut.Find("[data-slot='tabs-content']").GetAttribute("class")!;

        tabsClasses.Should().Contain("group/tabs");
        tabsClasses.Should().Contain("flex");
        tabsClasses.Should().Contain("gap-2");
        tabsClasses.Should().Contain("data-horizontal:flex-col");
        tabsClasses.Should().NotContain("q-tabs");

        listClasses.Should().Contain("group/tabs-list");
        listClasses.Should().Contain("inline-flex");
        listClasses.Should().Contain("items-center");
        listClasses.Should().Contain("justify-center");
        listClasses.Should().Contain("rounded-lg");
        listClasses.Should().Contain("p-[3px]");
        listClasses.Should().Contain("text-muted-foreground");
        listClasses.Should().Contain("group-data-horizontal/tabs:h-8");
        listClasses.Should().Contain("bg-muted");
        listClasses.Should().NotContain("q-tabs-list");

        triggerClasses.Should().Contain("relative");
        triggerClasses.Should().Contain("inline-flex");
        triggerClasses.Should().Contain("h-[calc(100%-1px)]");
        triggerClasses.Should().Contain("flex-1");
        triggerClasses.Should().Contain("rounded-md");
        triggerClasses.Should().Contain("border-transparent");
        triggerClasses.Should().Contain("text-sm");
        triggerClasses.Should().Contain("font-medium");
        triggerClasses.Should().Contain("transition-all");
        triggerClasses.Should().Contain("data-active:bg-background");
        triggerClasses.Should().Contain("group-data-[variant=default]/tabs-list:data-active:shadow-sm");
        triggerClasses.Should().NotContain("q-tabs-trigger");

        contentClasses.Should().Contain("flex-1");
        contentClasses.Should().Contain("text-sm");
        contentClasses.Should().Contain("outline-none");
        contentClasses.Should().NotContain("q-tabs-content");
    }
}
