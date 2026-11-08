using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Composed_justified_tabs_list_expands_to_full_width()
    {
        var cut = Render<Tabs>(parameters => parameters
            .Add(component => component.Justified, true)
            .Add(component => component.ChildContent, BuildTabsList()));

        var listClass = cut.Find("[data-slot='tabs-list']").GetAttribute("class")!;

        listClass.Should().Contain("w-full");
        listClass.Should().Contain("[&>[data-slot=tabs-trigger]]:flex-1");
        listClass.Should().NotContain("w-fit");
    }

    [Test]
    public void Tabs_styles_target_bradix_state_and_orientation_attributes_directly()
    {
        var cut = Render<Tabs>(parameters => parameters
            .Add(component => component.SelectedTab, "explorer")
            .Add(component => component.Vertical, true)
            .Add(component => component.ChildContent, BuildTabsList()));

        var root = cut.Find("[data-slot='tabs']");
        var list = cut.Find("[data-slot='tabs-list']");
        var trigger = cut.Find("[data-slot='tabs-trigger']");

        root.GetAttribute("data-orientation").Should().Be("vertical");
        root.GetAttribute("class").Should().Contain("data-[orientation=horizontal]:flex-col");
        list.GetAttribute("class").Should().Contain("group-data-[orientation=vertical]/tabs:flex-col");
        trigger.GetAttribute("data-state").Should().Be("active");
        trigger.GetAttribute("class").Should().Contain("data-[state=active]:bg-background");
        trigger.GetAttribute("class").Should().NotContain("data-active:");
    }

    [Test]
    [Arguments(false, "h-8")]
    [Arguments(true, "h-fit")]
    public void Tabs_list_uses_orientation_height_default(bool vertical, string expectedHeight)
    {
        var cut = Render<Tabs>(parameters => parameters
            .Add(component => component.Vertical, vertical)
            .Add(component => component.ChildContent, BuildTabsList()));

        cut.Find("[data-slot='tabs-list']").ClassList.Should().Contain(expectedHeight);
    }

    [Test]
    [Arguments(false, false)]
    [Arguments(true, false)]
    [Arguments(false, true)]
    [Arguments(true, true)]
    public void Tabs_list_explicit_height_overrides_orientation_default(bool vertical, bool usePreset)
    {
        var cut = Render<Tabs>(parameters => parameters
            .Add(component => component.Vertical, vertical)
            .Add(component => component.ChildContent, BuildTabsList(true, usePreset)));

        var list = cut.Find("[data-slot='tabs-list']");
        list.ClassList.Should().Contain("h-auto");
        list.ClassList.Should().NotContain("h-8").And.NotContain("h-fit");
        list.GetAttribute("class").Should().NotContain("/tabs:h-");
    }

    private static RenderFragment BuildTabsList(bool autoHeight = false, bool usePreset = false)
    {
        return builder =>
        {
            builder.OpenComponent<TabsList>(0);
            if (autoHeight)
            {
                if (usePreset)
                    builder.AddAttribute(2, nameof(TabsList.Preset), new QuarkPresetToken("test-tabs-auto-height", static context => context.Height = Height.Auto));
                else
                    builder.AddAttribute(3, nameof(TabsList.Height), (object) (CssValue<HeightBuilder>) Height.Auto);
            }
            builder.AddAttribute(1, nameof(TabsList.ChildContent), (RenderFragment) (listBuilder =>
            {
                listBuilder.OpenComponent<TabsTrigger>(0);
                listBuilder.AddAttribute(1, nameof(TabsTrigger.Value), "explorer");
                listBuilder.AddAttribute(2, nameof(TabsTrigger.ChildContent), (RenderFragment) (triggerBuilder => triggerBuilder.AddContent(0, "Explorer")));
                listBuilder.CloseComponent();

                listBuilder.OpenComponent<TabsTrigger>(3);
                listBuilder.AddAttribute(4, nameof(TabsTrigger.Value), "outline");
                listBuilder.AddAttribute(5, nameof(TabsTrigger.ChildContent), (RenderFragment) (triggerBuilder => triggerBuilder.AddContent(0, "Outline")));
                listBuilder.CloseComponent();
            }));
            builder.CloseComponent();
        };
    }
}
