using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void AlertDialogContent_matches_shadcn_default_component_contract()
    {
        var cut = Render<AlertDialog>(parameters => parameters
            .Add(p => p.Visible, true)
            .Add(p => p.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenComponent<AlertDialogContent>(0);
                builder.AddAttribute(1, "ChildContent", (RenderFragment)(contentBuilder =>
                {
                    contentBuilder.OpenComponent<AlertDialogHeader>(0);
                    contentBuilder.AddAttribute(1, "ChildContent", (RenderFragment)(headerBuilder =>
                    {
                        headerBuilder.OpenComponent<AlertDialogTitle>(0);
                        headerBuilder.AddAttribute(1, "ChildContent", (RenderFragment)(titleBuilder => titleBuilder.AddContent(0, "Delete")));
                        headerBuilder.CloseComponent();

                        headerBuilder.OpenComponent<AlertDialogDescription>(2);
                        headerBuilder.AddAttribute(3, "ChildContent", (RenderFragment)(descriptionBuilder => descriptionBuilder.AddContent(0, "Permanent action.")));
                        headerBuilder.CloseComponent();
                    }));
                    contentBuilder.CloseComponent();

                    contentBuilder.OpenComponent<AlertDialogFooter>(2);
                    contentBuilder.AddAttribute(3, "ChildContent", (RenderFragment)(footerBuilder =>
                    {
                        footerBuilder.OpenComponent<AlertDialogCancel>(0);
                        footerBuilder.AddAttribute(1, "ChildContent", (RenderFragment)(cancelBuilder => cancelBuilder.AddContent(0, "Cancel")));
                        footerBuilder.CloseComponent();

                        footerBuilder.OpenComponent<AlertDialogAction>(2);
                        footerBuilder.AddAttribute(3, "ChildContent", (RenderFragment)(actionBuilder => actionBuilder.AddContent(0, "Continue")));
                        footerBuilder.CloseComponent();
                    }));
                    contentBuilder.CloseComponent();
                }));
                builder.CloseComponent();
            })));

        var overlay = cut.Find("[data-slot='alert-dialog-overlay']");
        var content = cut.Find("[data-slot='alert-dialog-content']");

        var overlayClasses = overlay.GetAttribute("class")!;
        var contentClasses = content.GetAttribute("class")!;

        overlayClasses.Should().Contain("fixed");
        overlayClasses.Should().Contain("inset-0");
        overlayClasses.Should().Contain("z-50");
        overlayClasses.Should().Contain("bg-black/50");
        overlayClasses.Should().Contain("data-[state=open]:animate-in");
        overlayClasses.Should().Contain("data-[state=open]:fade-in-0");
        overlayClasses.Should().Contain("data-[state=closed]:animate-out");
        overlayClasses.Should().Contain("data-[state=closed]:fade-out-0");
        overlayClasses.Should().NotContain("bg-black/10");
        overlayClasses.Should().NotContain("supports-backdrop-filter:backdrop-blur-xs");

        contentClasses.Should().Contain("group/alert-dialog-content");
        contentClasses.Should().Contain("fixed");
        contentClasses.Should().Contain("top-[50%]");
        contentClasses.Should().Contain("left-[50%]");
        contentClasses.Should().Contain("z-50");
        contentClasses.Should().Contain("grid");
        contentClasses.Should().Contain("w-full");
        contentClasses.Should().Contain("max-w-[calc(100%-2rem)]");
        contentClasses.Should().Contain("translate-x-[-50%]");
        contentClasses.Should().Contain("translate-y-[-50%]");
        contentClasses.Should().Contain("gap-4");
        contentClasses.Should().Contain("rounded-lg");
        contentClasses.Should().Contain("border");
        contentClasses.Should().Contain("bg-background");
        contentClasses.Should().Contain("p-6");
        contentClasses.Should().Contain("shadow-lg");
        contentClasses.Should().Contain("duration-200");
        contentClasses.Should().Contain("data-[size=sm]:max-w-xs");
        contentClasses.Should().Contain("data-[size=default]:sm:max-w-lg");
        contentClasses.Should().Contain("data-[state=open]:animate-in");
        contentClasses.Should().Contain("data-[state=open]:fade-in-0");
        contentClasses.Should().Contain("data-[state=open]:zoom-in-95");
        contentClasses.Should().Contain("data-[state=closed]:animate-out");
        contentClasses.Should().Contain("data-[state=closed]:fade-out-0");
        contentClasses.Should().Contain("data-[state=closed]:zoom-out-95");
        contentClasses.Should().NotContain("bg-popover");
        contentClasses.Should().NotContain("rounded-xl");
        contentClasses.Should().NotContain("ring-1");
        contentClasses.Should().NotContain("data-open:animate-in");
    }
}
