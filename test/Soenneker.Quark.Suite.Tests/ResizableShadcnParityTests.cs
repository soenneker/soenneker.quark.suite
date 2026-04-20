using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void Resizable_components_match_shadcn_default_component_contract()
    {
        var cut = Render<ResizablePanelGroup>(parameters => parameters
            .Add(p => p.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenComponent<ResizablePanel>(0);
                builder.AddAttribute(1, "ChildContent", (RenderFragment)(contentBuilder => contentBuilder.AddContent(0, "One")));
                builder.CloseComponent();

                builder.OpenComponent<ResizableHandle>(2);
                builder.AddAttribute(3, nameof(ResizableHandle.WithHandle), true);
                builder.CloseComponent();

                builder.OpenComponent<ResizablePanel>(4);
                builder.AddAttribute(5, "ChildContent", (RenderFragment)(contentBuilder => contentBuilder.AddContent(0, "Two")));
                builder.CloseComponent();
            })));

        var group = cut.Find("[data-slot='resizable-panel-group']");
        var handle = cut.Find("[data-slot='resizable-handle']");
        var handleGrip = handle.FirstElementChild;
        var panels = cut.FindAll("[data-slot='resizable-panel']");

        var groupClasses = group.GetAttribute("class")!;
        var handleClasses = handle.GetAttribute("class")!;

        groupClasses.Should().Contain("flex");
        groupClasses.Should().Contain("h-full");
        groupClasses.Should().Contain("w-full");
        groupClasses.Should().Contain("aria-[orientation=vertical]:flex-col");

        handleClasses.Should().Contain("relative");
        handleClasses.Should().Contain("flex");
        handleClasses.Should().Contain("w-px");
        handleClasses.Should().Contain("items-center");
        handleClasses.Should().Contain("justify-center");
        handleClasses.Should().Contain("bg-border");
        handleClasses.Should().Contain("ring-offset-background");
        handleClasses.Should().Contain("after:absolute");
        handleClasses.Should().Contain("after:inset-y-0");
        handleClasses.Should().Contain("after:left-1/2");
        handleClasses.Should().Contain("after:w-1");
        handleClasses.Should().Contain("after:-translate-x-1/2");
        handleClasses.Should().Contain("focus-visible:ring-1");
        handleClasses.Should().Contain("focus-visible:ring-ring");
        handleClasses.Should().Contain("focus-visible:outline-hidden");
        handleClasses.Should().Contain("aria-[orientation=horizontal]:h-px");
        handleClasses.Should().Contain("aria-[orientation=horizontal]:w-full");
        handleClasses.Should().Contain("[&[aria-orientation=horizontal]>div]:rotate-90");
        handleClasses.Should().NotContain("bg-transparent");
        handleClasses.Should().NotContain("cursor-col-resize");
        handleClasses.Should().NotContain("cursor-row-resize");

        handleGrip.Should().NotBeNull();
        handleGrip!.GetAttribute("class")!.Should().Contain("z-10 flex h-6 w-1 shrink-0 rounded-lg bg-border");

        panels.Should().HaveCount(2);
        foreach (var panel in panels)
        {
            panel.GetAttribute("class").Should().BeNull();
        }

        cut.Markup.Should().NotContain("overflow-auto");
    }
}
