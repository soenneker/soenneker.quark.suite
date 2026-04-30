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

        var element = cut.Find("[data-slot='aspect-ratio']");
        var classes = element.GetAttribute("class");

        classes.Should().BeNullOrEmpty();
    }
}
