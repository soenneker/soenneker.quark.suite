using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Drawer_content_and_overlay_match_shadcn_default_component_contract()
    {
        var cut = Render<Drawer>(parameters => parameters
            .Add(p => p.Visible, true)
            .Add(p => p.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenComponent<DrawerContent>(0);
                builder.AddAttribute(1, "ChildContent", (RenderFragment)(contentBuilder =>
                {
                    contentBuilder.OpenComponent<DrawerHeader>(0);
                    contentBuilder.AddAttribute(1, "ChildContent", (RenderFragment)(headerBuilder =>
                    {
                        headerBuilder.OpenComponent<DrawerTitle>(0);
                        headerBuilder.AddAttribute(1, "ChildContent", (RenderFragment)(titleBuilder => titleBuilder.AddContent(0, "Edit profile")));
                        headerBuilder.CloseComponent();

                        headerBuilder.OpenComponent<DrawerDescription>(2);
                        headerBuilder.AddAttribute(3, "ChildContent", (RenderFragment)(descriptionBuilder => descriptionBuilder.AddContent(0, "Make changes to your profile here.")));
                        headerBuilder.CloseComponent();
                    }));
                    contentBuilder.CloseComponent();

                    contentBuilder.OpenComponent<DrawerFooter>(2);
                    contentBuilder.AddAttribute(3, "ChildContent", (RenderFragment)(footerBuilder =>
                    {
                        footerBuilder.OpenComponent<DrawerClose>(0);
                        footerBuilder.AddAttribute(1, "ChildContent", (RenderFragment)(closeBuilder => closeBuilder.AddContent(0, "Close")));
                        footerBuilder.CloseComponent();
                    }));
                    contentBuilder.CloseComponent();
                }));
                builder.CloseComponent();
            })));

        var overlayClasses = cut.Find("[data-slot='drawer-overlay']").GetAttribute("class")!;
        var contentClasses = cut.Find("[data-slot='drawer-content']").GetAttribute("class")!;

        overlayClasses.Should().Contain("fixed");
        overlayClasses.Should().Contain("inset-0");
        overlayClasses.Should().Contain("z-50");
        overlayClasses.Should().Contain("bg-black/50");
        overlayClasses.Should().Contain("data-[state=open]:animate-in");
        overlayClasses.Should().Contain("data-[state=open]:fade-in-0");

        contentClasses.Should().Contain("group/drawer-content");
        contentClasses.Should().Contain("bg-background");
        contentClasses.Should().Contain("fixed");
        contentClasses.Should().Contain("z-50");
        contentClasses.Should().Contain("flex");
        contentClasses.Should().Contain("h-auto");
        contentClasses.Should().Contain("flex-col");
        contentClasses.Should().Contain("transition");
        contentClasses.Should().Contain("ease-in-out");
        contentClasses.Should().Contain("data-[state=open]:animate-in");
        contentClasses.Should().Contain("data-[state=open]:duration-500");
        contentClasses.Should().Contain("data-[state=open]:slide-in-from-bottom");
        contentClasses.Should().Contain("data-[state=closed]:animate-out");
        contentClasses.Should().Contain("data-[state=closed]:duration-300");
        contentClasses.Should().Contain("data-[state=closed]:slide-out-to-bottom");
        contentClasses.Should().Contain("data-[vaul-drawer-direction=bottom]:inset-x-0");
        contentClasses.Should().Contain("data-[vaul-drawer-direction=bottom]:bottom-0");
        contentClasses.Should().Contain("data-[vaul-drawer-direction=bottom]:mt-24");
        contentClasses.Should().Contain("data-[vaul-drawer-direction=bottom]:max-h-[80vh]");
        contentClasses.Should().Contain("data-[vaul-drawer-direction=bottom]:rounded-t-lg");
        contentClasses.Should().Contain("data-[vaul-drawer-direction=bottom]:border-t");
        contentClasses.Should().Contain("data-[vaul-drawer-direction=right]:sm:max-w-sm");
        contentClasses.Should().NotContain("rounded-t-xl");
        contentClasses.Should().NotContain("data-[direction=bottom]");
    }

    [Test]
    public void Drawer_trigger_aschild_preserves_drawer_trigger_slot_on_child_button()
    {
        var cut = Render<DrawerTrigger>(parameters => parameters
            .Add(p => p.AsChild, true)
            .Add(p => p.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenComponent<Button>(0);
                builder.AddAttribute(1, "ChildContent", (RenderFragment)(contentBuilder => contentBuilder.AddContent(0, "Open")));
                builder.CloseComponent();
            })));

        cut.Find("button[data-slot='drawer-trigger']");
        cut.FindAll("button[data-slot='button']").Should().BeEmpty();
    }

    [Test]
    public void Drawer_direction_root_prop_sets_current_shadcn_side_contract()
    {
        var cut = Render<Drawer>(parameters => parameters
            .Add(p => p.Visible, true)
            .Add(p => p.Direction, "right")
            .AddChildContent<DrawerContent>());

        var content = cut.Find("[data-slot='drawer-content']");

        content.GetAttribute("data-vaul-drawer-direction").Should().Be("right");
        content.GetAttribute("data-side").Should().Be("right");
    }

    [Test]
    public void Drawer_close_aschild_preserves_drawer_close_slot_on_child_button()
    {
        var cut = Render<Drawer>(parameters => parameters
            .Add(p => p.Visible, true)
            .AddChildContent<DrawerContent>(contentParameters => contentParameters
                .AddChildContent<DrawerFooter>(footerParameters => footerParameters
                    .AddChildContent<DrawerClose>(closeParameters => closeParameters
                        .Add(p => p.AsChild, true)
                        .AddChildContent<Button>(buttonParameters => buttonParameters
                            .Add(p => p.Variant, ButtonVariant.Outline)
                            .Add(p => p.ChildContent, "Cancel"))))));

        cut.Find("button[data-slot='drawer-close']");
        cut.FindAll("[data-slot='drawer-close-wrapper']").Should().BeEmpty();
    }
}
