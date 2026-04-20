using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
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
        toggleGroupClasses.Should().Contain("gap-[--spacing(var(--gap))]");
        toggleGroupClasses.Should().NotContain("data-[variant=outline]:shadow-xs");
        toggleGroupClasses.Should().NotContain("q-toggle-group");

        toggleGroupItemClasses.Should().Contain("group/toggle");
        toggleGroupItemClasses.Should().Contain("group-data-[spacing=0]/toggle-group:rounded-none");
        toggleGroupItemClasses.Should().Contain("group-data-horizontal/toggle-group:data-[spacing=0]:first:rounded-l-lg");
        toggleGroupItemClasses.Should().Contain("data-[state=on]:bg-muted");
        toggleGroupItemClasses.Should().NotContain("q-toggle-group-item");
    }
}
