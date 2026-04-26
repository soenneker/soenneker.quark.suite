using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;
using System;
using System.IO;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
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
        var trackClasses = cut.Find("[data-slot='carousel-content'] > div").GetAttribute("class")!;
        var itemClasses = cut.Find("[data-slot='carousel-item']").GetAttribute("class")!;
        var previousClasses = cut.Find("[data-slot='carousel-previous']").GetAttribute("class")!;
        var nextClasses = cut.Find("[data-slot='carousel-next']").GetAttribute("class")!;

        rootClasses.Should().Contain("relative");
        rootClasses.Should().NotContain("q-carousel");

        contentClasses.Should().Contain("overflow-hidden");
        contentClasses.Should().Contain("touch-pan-y");
        contentClasses.Should().NotContain("-ml-4");
        contentClasses.Should().NotContain("q-carousel-content");

        trackClasses.Should().Contain("flex");
        trackClasses.Should().Contain("-ml-4");

        itemClasses.Should().Contain("min-w-0");
        itemClasses.Should().Contain("shrink-0");
        itemClasses.Should().Contain("grow-0");
        itemClasses.Should().Contain("basis-full");
        itemClasses.Should().Contain("pl-4");
        itemClasses.Should().NotContain("q-carousel-item");

        previousClasses.Should().Contain("absolute");
        previousClasses.Should().Contain("rounded-full");
        previousClasses.Should().Contain("top-1/2");
        previousClasses.Should().Contain("-left-12");
        previousClasses.Should().Contain("size-8");
        previousClasses.Should().NotContain("q-carousel-previous");

        nextClasses.Should().Contain("absolute");
        nextClasses.Should().Contain("rounded-full");
        nextClasses.Should().Contain("top-1/2");
        nextClasses.Should().Contain("-right-12");
        nextClasses.Should().Contain("size-8");
        nextClasses.Should().NotContain("q-carousel-next");
    }

    [Test]
    public void Carousel_content_class_applies_to_inner_track_like_shadcn()
    {
        var cut = Render<Carousel>(parameters => parameters
            .Add(p => p.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenComponent<CarouselContent>(0);
                builder.AddAttribute(1, "Class", "-ml-1");
                builder.AddAttribute(2, "ChildContent", (RenderFragment)(contentBuilder =>
                {
                    contentBuilder.OpenComponent<CarouselItem>(0);
                    contentBuilder.AddAttribute(1, "ChildContent", (RenderFragment)(itemBuilder => itemBuilder.AddContent(0, "One")));
                    contentBuilder.CloseComponent();
                }));
                builder.CloseComponent();
            })));

        var viewportClasses = cut.Find("[data-slot='carousel-content']").GetAttribute("class")!;
        var trackClasses = cut.Find("[data-slot='carousel-content'] > div").GetAttribute("class")!;

        viewportClasses.Should().Be("overflow-hidden touch-pan-y");
        trackClasses.Should().Contain("flex");
        trackClasses.Should().Contain("-ml-4");
        trackClasses.Should().Contain("-ml-1");
    }

    [Test]
    public void Carousel_content_wires_mobile_touch_swipe_to_shared_carousel_state()
    {
        var contentSource = File.ReadAllText(Path.Combine(GetSuiteRootForCarousel(), "src", "Soenneker.Quark.Suite", "Components", "Carousel", "CarouselContent.razor"));
        var carouselSource = File.ReadAllText(Path.Combine(GetSuiteRootForCarousel(), "src", "Soenneker.Quark.Suite", "Components", "Carousel", "Carousel.razor"));

        contentSource.Should().Contain("@ontouchstart=\"HandleTouchStart\"");
        contentSource.Should().Contain("@ontouchend=\"HandleTouchEnd\"");
        contentSource.Should().Contain("@ontouchcancel=\"HandleTouchCancel\"");
        contentSource.Should().Contain("\"overflow-hidden touch-pan-y\"");
        contentSource.Should().Contain("\"overflow-hidden touch-pan-x\"");
        carouselSource.Should().Contain("BeginTouchDrag(TouchEventArgs args)");
        carouselSource.Should().Contain("CompleteTouchDrag(TouchEventArgs args)");
        carouselSource.Should().Contain("args.PointerType != \"touch\" && args.Button != 0");
    }

    private static string GetSuiteRootForCarousel()
    {
        var directory = AppContext.BaseDirectory;

        while (!File.Exists(Path.Combine(directory, "Soenneker.Quark.Suite.slnx")))
            directory = Directory.GetParent(directory)!.FullName;

        return directory;
    }
}
