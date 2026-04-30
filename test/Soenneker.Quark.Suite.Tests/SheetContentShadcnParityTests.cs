using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Sheet_content_and_close_match_shadcn_default_component_contract()
    {
        var cut = Render<Sheet>(parameters => parameters
            .Add(p => p.Visible, true)
            .Add(p => p.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenComponent<SheetContent>(0);
                builder.AddAttribute(1, "ChildContent", (RenderFragment)(contentBuilder =>
                {
                    contentBuilder.OpenComponent<SheetHeader>(0);
                    contentBuilder.AddAttribute(1, "ChildContent", (RenderFragment)(headerBuilder =>
                    {
                        headerBuilder.OpenComponent<SheetTitle>(0);
                        headerBuilder.AddAttribute(1, "ChildContent", (RenderFragment)(titleBuilder => titleBuilder.AddContent(0, "Edit profile")));
                        headerBuilder.CloseComponent();

                        headerBuilder.OpenComponent<SheetDescription>(2);
                        headerBuilder.AddAttribute(3, "ChildContent", (RenderFragment)(descriptionBuilder => descriptionBuilder.AddContent(0, "Make changes.")));
                        headerBuilder.CloseComponent();
                    }));
                    contentBuilder.CloseComponent();
                }));
                builder.CloseComponent();
            })));

        var overlay = cut.Find("[data-slot='sheet-overlay']");
        var content = cut.Find("[data-slot='sheet-content']");
        var close = cut.Find("button[aria-label='Close']");

        var overlayClasses = overlay.GetAttribute("class")!;
        var contentClasses = content.GetAttribute("class")!;
        var closeClasses = close.GetAttribute("class")!;

        overlayClasses.Should().Contain("fixed");
        overlayClasses.Should().Contain("inset-0");
        overlayClasses.Should().Contain("z-50");
        overlayClasses.Should().Contain("bg-black/50");
        overlayClasses.Should().Contain("data-[state=open]:animate-in");
        overlayClasses.Should().Contain("data-[state=open]:fade-in-0");
        overlayClasses.Should().Contain("data-[state=closed]:animate-out");
        overlayClasses.Should().Contain("data-[state=closed]:fade-out-0");
        overlayClasses.Should().NotContain("bg-black/10");
        overlayClasses.Should().NotContain("duration-100");
        overlayClasses.Should().NotContain("supports-backdrop-filter:backdrop-blur-xs");
        overlayClasses.Should().NotContain("data-open:animate-in");

        contentClasses.Should().Contain("fixed");
        contentClasses.Should().Contain("z-50");
        contentClasses.Should().Contain("flex");
        contentClasses.Should().Contain("flex-col");
        contentClasses.Should().Contain("gap-4");
        contentClasses.Should().Contain("bg-background");
        contentClasses.Should().Contain("shadow-lg");
        contentClasses.Should().Contain("transition");
        contentClasses.Should().Contain("ease-in-out");
        contentClasses.Should().Contain("data-[state=open]:animate-in");
        contentClasses.Should().Contain("data-[state=open]:duration-500");
        contentClasses.Should().Contain("data-[state=closed]:animate-out");
        contentClasses.Should().Contain("data-[state=closed]:duration-300");
        contentClasses.Should().Contain("right-0");
        contentClasses.Should().Contain("border-l");
        contentClasses.Should().Contain("data-[state=open]:slide-in-from-right");
        contentClasses.Should().Contain("data-[state=closed]:slide-out-to-right");
        contentClasses.Should().NotContain("bg-popover");
        contentClasses.Should().NotContain("bg-clip-padding");
        contentClasses.Should().NotContain("text-popover-foreground");
        contentClasses.Should().NotContain("duration-200");
        contentClasses.Should().NotContain("data-open:animate-in");

        closeClasses.Should().Contain("absolute");
        closeClasses.Should().Contain("top-4");
        closeClasses.Should().Contain("right-4");
        closeClasses.Should().Contain("rounded-xs");
        closeClasses.Should().Contain("opacity-70");
        closeClasses.Should().Contain("ring-offset-background");
        closeClasses.Should().Contain("transition-opacity");
        closeClasses.Should().Contain("hover:opacity-100");
        closeClasses.Should().Contain("focus:ring-2");
        closeClasses.Should().Contain("focus:ring-ring");
        closeClasses.Should().Contain("focus:ring-offset-2");
        closeClasses.Should().Contain("focus:outline-hidden");
        closeClasses.Should().Contain("data-[state=open]:bg-secondary");
        close.GetAttribute("data-slot").Should().BeNull();
        close.GetAttribute("data-state").Should().Be("open");
        closeClasses.Should().NotContain("size-7");
        closeClasses.Should().NotContain("hover:bg-muted");
        closeClasses.Should().NotContain("focus-visible:ring-3");
        closeClasses.Should().NotContain("transition-all");
    }
}
