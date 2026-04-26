using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void ToggleGroup_matches_shadcn_base_classes()
    {
        var toggleGroup = Render<ToggleGroup>(parameters => parameters
            .Add(p => p.Spacing, 0)
            .Add(p => p.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenComponent<ToggleGroupItem>(0);
                builder.AddAttribute(1, "Value", "bold");
                builder.AddAttribute(2, "ChildContent", (RenderFragment)(contentBuilder => contentBuilder.AddContent(0, "Bold")));
                builder.CloseComponent();
            })));

        var toggleGroupClasses = toggleGroup.Find("[data-slot='toggle-group']").GetAttribute("class")!;
        var toggleGroupItemClasses = toggleGroup.Find("[data-slot='toggle-group-item']").GetAttribute("class")!;

        toggleGroupClasses.Should().Contain("group/toggle-group");
        toggleGroupClasses.Should().Contain("flex");
        toggleGroupClasses.Should().Contain("w-fit");
        toggleGroupClasses.Should().Contain("flex-row");
        toggleGroupClasses.Should().Contain("items-center");
        toggleGroupClasses.Should().Contain("rounded-md");
        toggleGroupClasses.Should().Contain("gap-[--spacing(var(--gap))]");
        toggleGroupClasses.Should().NotContain("data-[size=sm]:rounded-[min(var(--radius-md),10px)]");
        toggleGroupClasses.Should().Contain("data-vertical:flex-col");
        toggleGroupClasses.Should().Contain("data-vertical:items-stretch");
        toggleGroupClasses.Should().NotContain("rounded-lg");
        toggleGroupClasses.Should().Contain("shadow-xs");
        toggleGroupClasses.Should().NotContain("q-toggle-group");

        toggleGroupItemClasses.Should().Contain("inline-flex");
        toggleGroupItemClasses.Should().Contain("shrink-0");
        toggleGroupItemClasses.Should().Contain("data-[spacing=0]:rounded-none");
        toggleGroupItemClasses.Should().Contain("px-2");
        toggleGroupItemClasses.Should().Contain("data-[spacing=0]:first:rounded-l-md");
        toggleGroupItemClasses.Should().Contain("data-[spacing=0]:last:rounded-r-md");
        toggleGroupItemClasses.Should().NotContain("group/toggle");
        toggleGroupItemClasses.Should().Contain("gap-2");
        toggleGroupItemClasses.Should().Contain("rounded-md");
        toggleGroupItemClasses.Should().Contain("data-[state=on]:bg-accent");
        toggleGroupItemClasses.Should().Contain("h-9");
        toggleGroupItemClasses.Should().Contain("min-w-9");
        toggleGroupItemClasses.Should().Contain("px-2");
        toggleGroupItemClasses.Should().Contain("w-auto");
        toggleGroupItemClasses.Should().Contain("min-w-0");
        toggleGroupItemClasses.Should().Contain("px-3");
        toggleGroupItemClasses.Should().NotContain("data-[spacing=0]:first:rounded-l-lg");
        toggleGroupItemClasses.Should().NotContain("data-[state=on]:bg-muted");
        toggleGroupItemClasses.Should().NotContain("q-toggle-group-item");
    }
}
