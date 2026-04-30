using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void ButtonGroup_matches_shadcn_base_classes()
    {
        var cut = Render<ButtonGroup>(parameters => parameters
            .Add(p => p.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenComponent<Button>(0);
                builder.AddAttribute(1, "ChildContent", (RenderFragment)(contentBuilder => contentBuilder.AddContent(0, "Previous")));
                builder.CloseComponent();

                builder.OpenComponent<Button>(2);
                builder.AddAttribute(3, "ChildContent", (RenderFragment)(contentBuilder => contentBuilder.AddContent(0, "Next")));
                builder.CloseComponent();
            })));

        var classes = cut.Find("[data-slot='button-group']").GetAttribute("class")!;

        classes.Should().Contain("flex");
        classes.Should().Contain("w-fit");
        classes.Should().Contain("items-stretch");
        classes.Should().Contain("[&>*]:focus-visible:relative");
        classes.Should().Contain("[&>*]:focus-visible:z-10");
        classes.Should().Contain("has-[>[data-slot=button-group]]:gap-2");
        classes.Should().Contain("has-[select[aria-hidden=true]:last-child]:[&>[data-slot=select-trigger]:last-of-type]:rounded-r-md");
        classes.Should().Contain("[&>[data-slot=select-trigger]:not([class*='w-'])]:w-fit");
        classes.Should().Contain("[&>input]:flex-1");
        classes.Should().Contain("[&>*:not(:first-child)]:rounded-l-none");
        classes.Should().Contain("[&>*:not(:first-child)]:border-l-0");
        classes.Should().Contain("[&>*:not(:last-child)]:rounded-r-none");
        classes.Should().NotContain("group/button-group");
        classes.Should().NotContain("q-button-group");
    }

    [Test]
    public void ButtonGroupText_matches_shadcn_base_classes()
    {
        var cut = Render<ButtonGroupText>(parameters => parameters
            .Add(p => p.ChildContent, (RenderFragment)(builder => builder.AddContent(0, "Text"))));

        var classes = cut.Find("[data-slot='button-group-text']").GetAttribute("class")!;

        classes.Should().Contain("flex");
        classes.Should().Contain("items-center");
        classes.Should().Contain("gap-2");
        classes.Should().Contain("rounded-md");
        classes.Should().Contain("border");
        classes.Should().Contain("bg-muted");
        classes.Should().Contain("px-4");
        classes.Should().Contain("text-sm");
        classes.Should().Contain("font-medium");
        classes.Should().Contain("shadow-xs");
        classes.Should().Contain("[&_svg]:pointer-events-none");
        classes.Should().Contain("[&_svg:not([class*='size-'])]:size-4");
    }

    [Test]
    public void ButtonGroupSeparator_matches_shadcn_base_classes()
    {
        var cut = Render<ButtonGroupSeparator>();

        var separator = cut.Find("[data-slot='button-group-separator']");
        var classes = separator.GetAttribute("class")!;

        separator.GetAttribute("data-orientation").Should().Be("vertical");
        classes.Should().Contain("bg-input");
        classes.Should().Contain("relative");
        classes.Should().Contain("m-0!");
        classes.Should().NotContain("!m-0");
        classes.Should().Contain("self-stretch");
        classes.Should().Contain("data-[orientation=vertical]:h-auto");
    }

    [Test]
    public void ButtonGroupSeparator_default_orientation_does_not_follow_parent_group()
    {
        var cut = Render<ButtonGroup>(parameters => parameters
            .Add(p => p.Vertical, true)
            .Add(p => p.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenComponent<ButtonGroupSeparator>(0);
                builder.CloseComponent();
            })));

        cut.Find("[data-slot='button-group-separator']").GetAttribute("data-orientation").Should().Be("vertical");
    }
}
