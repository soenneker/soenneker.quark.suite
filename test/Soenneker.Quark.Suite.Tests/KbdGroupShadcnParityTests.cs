using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void KbdGroup_matches_shadcn_base_classes()
    {
        var cut = Render<KbdGroup>(parameters => parameters
            .Add(p => p.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenComponent<Kbd>(0);
                builder.AddAttribute(1, "ChildContent", (RenderFragment)(contentBuilder => contentBuilder.AddContent(0, "Ctrl")));
                builder.CloseComponent();

                builder.OpenComponent<Kbd>(2);
                builder.AddAttribute(3, "ChildContent", (RenderFragment)(contentBuilder => contentBuilder.AddContent(0, "P")));
                builder.CloseComponent();
            })));

        var classes = cut.Find("[data-slot='kbd-group']").GetAttribute("class")!;

        classes.Should().Contain("inline-flex");
        classes.Should().Contain("items-center");
        classes.Should().Contain("gap-1");
        classes.Should().NotContain("q-kbd-group");
    }
}
