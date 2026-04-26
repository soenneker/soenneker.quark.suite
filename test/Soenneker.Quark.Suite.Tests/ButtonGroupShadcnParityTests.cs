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
        classes.Should().Contain("*:focus-visible:relative");
        classes.Should().Contain("*:focus-visible:z-10");
        classes.Should().Contain("[&>*:not(:first-child)]:rounded-l-none");
        classes.Should().Contain("[&>*:not(:first-child)]:border-l-0");
        classes.Should().Contain("[&>*:not(:last-child)]:rounded-r-none");
        classes.Should().Contain("[&>[data-slot]:not(:has(~[data-slot]))]:rounded-r-lg!");
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
        classes.Should().Contain("rounded-lg");
        classes.Should().Contain("border");
        classes.Should().Contain("bg-muted");
        classes.Should().Contain("px-2.5");
        classes.Should().Contain("text-sm");
        classes.Should().Contain("font-medium");
        classes.Should().NotContain("shadow-xs");
        classes.Should().Contain("[&_svg]:pointer-events-none");
        classes.Should().Contain("[&_svg:not([class*='size-'])]:size-4");
    }
}
