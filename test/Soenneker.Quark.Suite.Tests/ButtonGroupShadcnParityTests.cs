using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;
using Soenneker.Quark;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
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

        string classes = cut.Find("[data-slot='button-group']").GetAttribute("class")!;

        classes.Should().Contain("group/button-group");
        classes.Should().Contain("flex");
        classes.Should().Contain("w-fit");
        classes.Should().Contain("*:focus-visible:relative");
        classes.Should().Contain("*:focus-visible:z-10");
        classes.Should().Contain("[&>*:not(:first-child)]:rounded-l-none");
        classes.Should().Contain("[&>*:not(:first-child)]:border-l-0");
        classes.Should().Contain("[&>*:not(:last-child)]:rounded-r-none");
        classes.Should().NotContain("q-button-group");
    }
}
