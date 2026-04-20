using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void Carousel_slots_match_shadcn_base_classes()
    {
        var cut = Render<Carousel>(parameters => parameters
            .Add(p => p.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenComponent<CarouselContent>(0);
                builder.AddAttribute(1, "ChildContent", (RenderFragment)(contentBuilder =>
                {
                    contentBuilder.OpenComponent<CarouselItem>(0);
                    contentBuilder.AddAttribute(1, "ChildContent", (RenderFragment)(itemBuilder => itemBuilder.AddContent(0, "One")));
                    contentBuilder.CloseComponent();
                }));
                builder.CloseComponent();

                builder.OpenComponent<CarouselPrevious>(2);
                builder.CloseComponent();

                builder.OpenComponent<CarouselNext>(3);
                builder.CloseComponent();
            })));

        var rootClasses = cut.Find("[data-slot='carousel']").GetAttribute("class")!;
        var contentClasses = cut.Find("[data-slot='carousel-content']").GetAttribute("class")!;
        var itemClasses = cut.Find("[data-slot='carousel-item']").GetAttribute("class")!;
        var previousClasses = cut.Find("[data-slot='carousel-previous']").GetAttribute("class")!;
        var nextClasses = cut.Find("[data-slot='carousel-next']").GetAttribute("class")!;

        rootClasses.Should().Contain("relative");
        rootClasses.Should().NotContain("q-carousel");

        contentClasses.Should().Contain("overflow-hidden");
        contentClasses.Should().NotContain("q-carousel-content");

        itemClasses.Should().Contain("min-w-0");
        itemClasses.Should().Contain("shrink-0");
        itemClasses.Should().Contain("grow-0");
        itemClasses.Should().Contain("basis-full");
        itemClasses.Should().Contain("pl-4");
        itemClasses.Should().NotContain("q-carousel-item");

        previousClasses.Should().Contain("absolute");
        previousClasses.Should().Contain("touch-manipulation");
        previousClasses.Should().Contain("rounded-full");
        previousClasses.Should().Contain("top-1/2");
        previousClasses.Should().Contain("-left-12");
        previousClasses.Should().Contain("size-7");
        previousClasses.Should().NotContain("q-carousel-previous");

        nextClasses.Should().Contain("absolute");
        nextClasses.Should().Contain("touch-manipulation");
        nextClasses.Should().Contain("rounded-full");
        nextClasses.Should().Contain("top-1/2");
        nextClasses.Should().Contain("-right-12");
        nextClasses.Should().Contain("size-7");
        nextClasses.Should().NotContain("q-carousel-next");
    }
}
