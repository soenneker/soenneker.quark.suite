using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;
using Soenneker.Bradix;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Select_slots_match_shadcn_base_classes()
    {
        var triggerCut = Render<Select<string>>(parameters => parameters
            .Add(p => p.DefaultItemText, "Select a fruit")
            .Add(p => p.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenComponent<SelectTrigger>(0);
                builder.AddAttribute(1, "ChildContent", (RenderFragment)(triggerBuilder =>
                {
                    triggerBuilder.OpenComponent<SelectValue>(0);
                    triggerBuilder.CloseComponent();
                }));
                builder.CloseComponent();
            })));

        var selectClasses = triggerCut.Find("[data-slot='select']").GetAttribute("class")!;
        var triggerClasses = triggerCut.Find("[data-slot='select-trigger']").GetAttribute("class")!;
        var valueClasses = triggerCut.Find("[data-slot='select-value']").GetAttribute("class");

        selectClasses.Should().Contain("group/select");
        selectClasses.Should().Contain("relative");
        selectClasses.Should().Contain("w-full");
        selectClasses.Should().Contain("max-w-full");
        selectClasses.Should().NotContain("q-select");

        triggerClasses.Should().Contain("flex");
        triggerClasses.Should().Contain("w-fit");
        triggerClasses.Should().Contain("items-center");
        triggerClasses.Should().Contain("justify-between");
        triggerClasses.Should().Contain("gap-2");
        triggerClasses.Should().Contain("rounded-md");
        triggerClasses.Should().Contain("border");
        triggerClasses.Should().Contain("border-input");
        triggerClasses.Should().Contain("bg-transparent");
        triggerClasses.Should().Contain("px-3");
        triggerClasses.Should().Contain("py-2");
        triggerClasses.Should().Contain("text-sm");
        triggerClasses.Should().Contain("shadow-xs");
        triggerClasses.Should().Contain("transition-[color,box-shadow]");
        triggerClasses.Should().Contain("outline-none");
        triggerClasses.Should().Contain("focus-visible:ring-[3px]");
        triggerClasses.Should().Contain("data-[size=default]:h-9");
        triggerClasses.Should().Contain("data-[size=sm]:h-8");
        triggerClasses.Should().Contain("*:data-[slot=select-value]:line-clamp-1");
        triggerClasses.Should().Contain("*:data-[slot=select-value]:flex");
        triggerClasses.Should().Contain("*:data-[slot=select-value]:items-center");
        triggerClasses.Should().Contain("*:data-[slot=select-value]:gap-2");
        triggerClasses.Should().NotContain("q-select-trigger");

        valueClasses.Should().BeNullOrEmpty();
        valueClasses.Should().NotContain("q-select-value");
        valueClasses.Should().NotContain("text-left");
    }

    [Test]
    public void SelectTrigger_inside_field_defaults_to_full_width()
    {
        var cut = Render<Field>(parameters => parameters
            .AddChildContent<FieldLabel>(child => child.AddChildContent("Month"))
            .AddChildContent<Select<string>>(child => child
                .Add(p => p.DefaultItemText, "MM")
                .Add(p => p.ChildContent, (RenderFragment)(builder =>
                {
                    builder.OpenComponent<SelectTrigger>(0);
                    builder.AddAttribute(1, "ChildContent", (RenderFragment)(triggerBuilder =>
                    {
                        triggerBuilder.OpenComponent<SelectValue>(0);
                        triggerBuilder.CloseComponent();
                    }));
                    builder.CloseComponent();
                }))));

        var triggerClasses = cut.Find("[data-slot='select-trigger']").GetAttribute("class")!;

        triggerClasses.Should().Contain("w-full");
    }

    [Test]
    public void SelectContent_matches_shadcn_v4_content_and_viewport_classes()
    {
        var cut = Render<Select<string>>(parameters => parameters
            .Add(p => p.DefaultItemText, "Select a fruit")
            .Add(p => p.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenComponent<SelectTrigger>(0);
                builder.AddAttribute(1, "ChildContent", (RenderFragment)(triggerBuilder =>
                {
                    triggerBuilder.OpenComponent<SelectValue>(0);
                    triggerBuilder.CloseComponent();
                }));
                builder.CloseComponent();

                builder.OpenComponent<SelectContent>(2);
                builder.AddAttribute(3, nameof(SelectContent.ForceMount), true);
                builder.AddAttribute(4, nameof(SelectContent.ContentPosition), "popper");
                builder.AddAttribute(5, "ChildContent", (RenderFragment)(contentBuilder => contentBuilder.AddContent(0, "Items")));
                builder.CloseComponent();
            })));

        var contentClasses = cut.Find("[data-slot='select-content']").GetAttribute("class")!;
        var viewportClasses = cut.Find("[data-radix-select-viewport]").GetAttribute("class")!;

        contentClasses.Should().Contain("relative");
        contentClasses.Should().Contain("z-50");
        contentClasses.Should().Contain("max-h-(--radix-select-content-available-height)");
        contentClasses.Should().Contain("min-w-[8rem]");
        contentClasses.Should().Contain("origin-(--radix-select-content-transform-origin)");
        contentClasses.Should().Contain("overflow-x-hidden");
        contentClasses.Should().Contain("overflow-y-auto");
        contentClasses.Should().Contain("rounded-md");
        contentClasses.Should().Contain("border");
        contentClasses.Should().Contain("bg-popover");
        contentClasses.Should().Contain("text-popover-foreground");
        contentClasses.Should().Contain("shadow-md");
        contentClasses.Should().Contain("data-[state=open]:animate-in");
        contentClasses.Should().Contain("data-[position=popper]:data-[side=bottom]:translate-y-1");
        contentClasses.Should().NotContain("data-open:");

        viewportClasses.Should().Contain("p-1");
        viewportClasses.Should().Contain("h-[var(--radix-select-trigger-height)]");
        viewportClasses.Should().Contain("min-w-[var(--radix-select-trigger-width)]");
    }

    [Test]
    public void SelectContent_forwards_radix_popper_collision_options()
    {
        var cut = Render<Select<string>>(parameters => parameters
            .Add(p => p.DefaultItemText, "Select a fruit")
            .Add(p => p.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenComponent<SelectTrigger>(0);
                builder.AddAttribute(1, "ChildContent", (RenderFragment)(triggerBuilder =>
                {
                    triggerBuilder.OpenComponent<SelectValue>(0);
                    triggerBuilder.CloseComponent();
                }));
                builder.CloseComponent();

                builder.OpenComponent<SelectContent>(2);
                builder.AddAttribute(3, nameof(SelectContent.ForceMount), true);
                builder.AddAttribute(4, nameof(SelectContent.ContentPosition), "popper");
                builder.AddAttribute(5, nameof(SelectContent.CollisionBoundarySelector), "#select-boundary-a");
                builder.AddAttribute(6, nameof(SelectContent.CollisionBoundarySelectors), new[] { "#select-boundary-b", "#select-boundary-a" });
                builder.AddAttribute(7, nameof(SelectContent.Sticky), "always");
                builder.AddAttribute(8, nameof(SelectContent.AvoidCollisions), false);
                builder.AddAttribute(9, nameof(SelectContent.HideWhenDetached), true);
                builder.AddAttribute(10, "ChildContent", (RenderFragment)(contentBuilder => contentBuilder.AddContent(0, "Items")));
                builder.CloseComponent();
            })));

        BradixSelectContent content = cut.FindComponent<BradixSelectContent>().Instance;

        content.Position.Should().Be("popper");
        content.CollisionBoundarySelector.Should().Be("#select-boundary-a");
        content.CollisionBoundarySelectors.Should().BeEquivalentTo(["#select-boundary-b", "#select-boundary-a"]);
        content.Sticky.Should().Be("always");
        content.AvoidCollisions.Should().BeFalse();
        content.HideWhenDetached.Should().BeTrue();
    }
}
