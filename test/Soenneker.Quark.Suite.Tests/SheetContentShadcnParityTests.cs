using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
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
        var close = cut.Find("[data-slot='sheet-close']");

        var overlayClasses = overlay.GetAttribute("class")!;
        var contentClasses = content.GetAttribute("class")!;
        var closeClasses = close.GetAttribute("class")!;

        overlayClasses.Should().Contain("fixed");
        overlayClasses.Should().Contain("inset-0");
        overlayClasses.Should().Contain("z-50");
        overlayClasses.Should().Contain("bg-black/10");
        overlayClasses.Should().Contain("duration-100");
        overlayClasses.Should().Contain("supports-backdrop-filter:backdrop-blur-xs");
        overlayClasses.Should().Contain("data-open:animate-in");
        overlayClasses.Should().Contain("data-closed:animate-out");
        overlayClasses.Should().NotContain("bg-black/50");

        contentClasses.Should().Contain("fixed");
        contentClasses.Should().Contain("z-50");
        contentClasses.Should().Contain("flex");
        contentClasses.Should().Contain("flex-col");
        contentClasses.Should().Contain("gap-4");
        contentClasses.Should().Contain("bg-popover");
        contentClasses.Should().Contain("bg-clip-padding");
        contentClasses.Should().Contain("text-sm");
        contentClasses.Should().Contain("text-popover-foreground");
        contentClasses.Should().Contain("shadow-lg");
        contentClasses.Should().Contain("transition");
        contentClasses.Should().Contain("duration-200");
        contentClasses.Should().Contain("ease-in-out");
        contentClasses.Should().Contain("data-open:animate-in");
        contentClasses.Should().Contain("data-[side=right]:right-0");
        contentClasses.Should().Contain("data-[side=right]:border-l");
        contentClasses.Should().Contain("data-[side=right]:data-open:slide-in-from-right-10");
        contentClasses.Should().Contain("data-[side=right]:data-closed:slide-out-to-right-10");
        contentClasses.Should().NotContain("bg-background");
        contentClasses.Should().NotContain("data-[state=open]:animate-in");

        closeClasses.Should().Contain("group/button");
        closeClasses.Should().Contain("inline-flex");
        closeClasses.Should().Contain("size-7");
        closeClasses.Should().Contain("absolute");
        closeClasses.Should().Contain("top-3");
        closeClasses.Should().Contain("right-3");
        closeClasses.Should().Contain("border-transparent");
        closeClasses.Should().Contain("bg-transparent");
    }
}
