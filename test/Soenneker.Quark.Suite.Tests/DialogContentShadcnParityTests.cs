using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
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

        var overlayClasses = overlay.GetAttribute("class")!;
        var contentClasses = content.GetAttribute("class")!;
        var closeClasses = close.GetAttribute("class")!;

        overlayClasses.Should().Contain("fixed");
        overlayClasses.Should().Contain("inset-0");
        overlayClasses.Should().Contain("z-50");
        overlayClasses.Should().Contain("bg-black/10");
        overlayClasses.Should().Contain("data-[state=open]:animate-in");
        overlayClasses.Should().Contain("data-[state=closed]:animate-out");
        overlayClasses.Should().NotContain("bg-black/50");

        contentClasses.Should().Contain("fixed");
        contentClasses.Should().Contain("top-1/2");
        contentClasses.Should().Contain("left-1/2");
        contentClasses.Should().Contain("z-50");
        contentClasses.Should().Contain("grid");
        contentClasses.Should().Contain("w-full");
        contentClasses.Should().Contain("max-w-[calc(100%-2rem)]");
        contentClasses.Should().Contain("-translate-x-1/2");
        contentClasses.Should().Contain("-translate-y-1/2");
        contentClasses.Should().Contain("gap-4");
        contentClasses.Should().Contain("rounded-xl");
        contentClasses.Should().Contain("ring-1");
        contentClasses.Should().Contain("bg-popover");
        contentClasses.Should().Contain("p-6");
        contentClasses.Should().Contain("ring-foreground/10");
        contentClasses.Should().Contain("duration-100");
        contentClasses.Should().Contain("outline-none");
        contentClasses.Should().Contain("sm:max-w-[425px]");
        contentClasses.Should().Contain("data-[state=open]:animate-in");
        contentClasses.Should().Contain("data-[state=open]:fade-in-0");
        contentClasses.Should().Contain("data-[state=open]:zoom-in-95");
        contentClasses.Should().Contain("data-[state=closed]:animate-out");
        contentClasses.Should().Contain("data-[state=closed]:fade-out-0");
        contentClasses.Should().Contain("data-[state=closed]:zoom-out-95");
        contentClasses.Should().NotContain("bg-background");
        contentClasses.Should().NotContain("rounded-lg");
        contentClasses.Should().NotContain("shadow-lg");
        contentClasses.Should().NotContain("data-open:animate-in");

        closeClasses.Should().Contain("absolute");
        closeClasses.Should().Contain("top-4");
        closeClasses.Should().Contain("right-4");
        closeClasses.Should().Contain("rounded-md");
        closeClasses.Should().Contain("opacity-70");
        closeClasses.Should().Contain("hover:opacity-100");
        closeClasses.Should().Contain("focus-visible:ring-[3px]");
        closeClasses.Should().NotContain("data-[state=open]:bg-accent");
    }
}
