using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;
using AspectRatioComponent = Soenneker.Quark.Components.Layout.AspectRatio;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void AspectRatio_matches_shadcn_base_classes()
    {
        var cut = Render<AspectRatioComponent>(parameters => parameters
            .Add(p => p.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenElement(0, "img");
                builder.AddAttribute(1, "src", "https://example.com/photo.png");
                builder.AddAttribute(2, "alt", "Photo");
                builder.CloseElement();
            })));

        var classes = cut.Find("[data-slot='aspect-ratio']").GetAttribute("class")!;

        classes.Should().Contain("relative");
        classes.Should().Contain("w-full");
        classes.Should().Contain("overflow-hidden");
        classes.Should().Contain("[&>img]:object-cover");
        classes.Should().Contain("[&>video]:object-cover");
        classes.Should().Contain("[&>iframe]:object-cover");
        classes.Should().NotContain("q-aspect-ratio");
    }
}
