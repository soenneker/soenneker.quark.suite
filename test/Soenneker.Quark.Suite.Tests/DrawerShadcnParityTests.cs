using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void Drawer_content_and_overlay_match_shadcn_default_component_contract()
    {
        var cut = Render<Drawer>(parameters => parameters
            .Add(p => p.Visible, true)
            .Add(p => p.ChildContent, (Microsoft.AspNetCore.Components.RenderFragment)(builder =>
            {
                builder.OpenComponent<DrawerContent>(0);
                builder.AddAttribute(1, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)(contentBuilder =>
                {
                    contentBuilder.OpenComponent<DrawerHeader>(0);
                    contentBuilder.AddAttribute(1, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)(headerBuilder =>
                    {
                        headerBuilder.OpenComponent<DrawerTitle>(0);
                        headerBuilder.AddAttribute(1, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)(titleBuilder => titleBuilder.AddContent(0, "Edit profile")));
                        headerBuilder.CloseComponent();

                        headerBuilder.OpenComponent<DrawerDescription>(2);
                        headerBuilder.AddAttribute(3, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)(descriptionBuilder => descriptionBuilder.AddContent(0, "Make changes to your profile here.")));
                        headerBuilder.CloseComponent();
                    }));
                    contentBuilder.CloseComponent();

                    contentBuilder.OpenComponent<DrawerFooter>(2);
                    contentBuilder.AddAttribute(3, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)(footerBuilder =>
                    {
                        footerBuilder.OpenComponent<DrawerClose>(0);
                        footerBuilder.AddAttribute(1, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)(closeBuilder => closeBuilder.AddContent(0, "Close")));
                        footerBuilder.CloseComponent();
                    }));
                    contentBuilder.CloseComponent();
                }));
                builder.CloseComponent();
            })));

        string overlayClasses = cut.Find("[data-slot='drawer-overlay']").GetAttribute("class")!;
        string contentClasses = cut.Find("[data-slot='drawer-content']").GetAttribute("class")!;

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
        contentClasses.Should().Contain("flex-col");
        contentClasses.Should().Contain("shadow-lg");
        contentClasses.Should().Contain("inset-x-0");
        contentClasses.Should().Contain("bottom-0");
        contentClasses.Should().Contain("max-h-[80vh]");
        contentClasses.Should().Contain("rounded-t-lg");
        contentClasses.Should().Contain("border-t");
        contentClasses.Should().Contain("data-[state=open]:slide-in-from-bottom");
    }

    [Fact]
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
}
