using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;
using Soenneker.Quark;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void Dialog_content_and_close_match_shadcn_default_component_contract()
    {
        var cut = Render<Dialog>(parameters => parameters
            .Add(p => p.Visible, true)
            .Add(p => p.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenComponent<DialogContent>(0);
                builder.AddAttribute(1, "ChildContent", (RenderFragment)(contentBuilder =>
                {
                    contentBuilder.OpenComponent<DialogHeader>(0);
                    contentBuilder.AddAttribute(1, "ChildContent", (RenderFragment)(headerBuilder =>
                    {
                        headerBuilder.OpenComponent<DialogTitle>(0);
                        headerBuilder.AddAttribute(1, "ChildContent", (RenderFragment)(titleBuilder => titleBuilder.AddContent(0, "Edit profile")));
                        headerBuilder.CloseComponent();

                        headerBuilder.OpenComponent<DialogDescription>(2);
                        headerBuilder.AddAttribute(3, "ChildContent", (RenderFragment)(descriptionBuilder => descriptionBuilder.AddContent(0, "Make changes.")));
                        headerBuilder.CloseComponent();
                    }));
                    contentBuilder.CloseComponent();

                    contentBuilder.OpenComponent<DialogFooter>(2);
                    contentBuilder.AddAttribute(3, "ChildContent", (RenderFragment)(footerBuilder => footerBuilder.AddContent(0, "Actions")));
                    contentBuilder.CloseComponent();
                }));
                builder.CloseComponent();
            })));

        var overlay = cut.Find("[data-slot='dialog-overlay']");
        var content = cut.Find("[data-slot='dialog-content']");
        var close = cut.Find("[data-slot='dialog-close']");

        string overlayClasses = overlay.GetAttribute("class")!;
        string contentClasses = content.GetAttribute("class")!;
        string closeClasses = close.GetAttribute("class")!;

        overlayClasses.Should().Contain("fixed");
        overlayClasses.Should().Contain("inset-0");
        overlayClasses.Should().Contain("isolate");
        overlayClasses.Should().Contain("z-50");
        overlayClasses.Should().Contain("bg-black/10");
        overlayClasses.Should().Contain("duration-100");
        overlayClasses.Should().Contain("supports-backdrop-filter:backdrop-blur-xs");
        overlayClasses.Should().Contain("data-open:animate-in");
        overlayClasses.Should().Contain("data-closed:animate-out");
        overlayClasses.Should().NotContain("bg-black/50");

        contentClasses.Should().Contain("fixed");
        contentClasses.Should().Contain("top-50");
        contentClasses.Should().Contain("start-50");
        contentClasses.Should().Contain("z-50");
        contentClasses.Should().Contain("grid");
        contentClasses.Should().Contain("w-full");
        contentClasses.Should().Contain("max-w-[calc(100%-2rem)]");
        contentClasses.Should().Contain("translate-middle");
        contentClasses.Should().Contain("gap-4");
        contentClasses.Should().Contain("rounded-xl");
        contentClasses.Should().Contain("bg-popover");
        contentClasses.Should().Contain("p-4");
        contentClasses.Should().Contain("text-sm");
        contentClasses.Should().Contain("text-popover-foreground");
        contentClasses.Should().Contain("ring-1");
        contentClasses.Should().Contain("ring-foreground/10");
        contentClasses.Should().Contain("duration-100");
        contentClasses.Should().Contain("outline-none");
        contentClasses.Should().Contain("sm:max-w-sm");
        contentClasses.Should().Contain("data-open:animate-in");
        contentClasses.Should().Contain("data-open:fade-in-0");
        contentClasses.Should().Contain("data-open:zoom-in-95");
        contentClasses.Should().Contain("data-closed:animate-out");
        contentClasses.Should().Contain("data-closed:fade-out-0");
        contentClasses.Should().Contain("data-closed:zoom-out-95");
        contentClasses.Should().NotContain("bg-background");
        contentClasses.Should().NotContain("border");
        contentClasses.Should().NotContain("shadow-lg");
        contentClasses.Should().NotContain("rounded-lg");
        contentClasses.Should().NotContain("data-[state=open]:animate-in");

        closeClasses.Should().Contain("group/button");
        closeClasses.Should().Contain("inline-flex");
        closeClasses.Should().Contain("size-7");
        closeClasses.Should().Contain("absolute");
        closeClasses.Should().Contain("top-2");
        closeClasses.Should().Contain("right-2");
        closeClasses.Should().Contain("border-transparent");
        closeClasses.Should().Contain("bg-transparent");
    }
}
