using AwesomeAssertions;
using Bunit;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Empty_matches_shadcn_base_classes()
    {
        var cut = Render<Empty>(parameters => parameters
            .Add(p => p.ChildContent, "Empty"));

        var classes = cut.Find("[data-slot='empty']").GetAttribute("class")!;

        classes.Should().Contain("flex");
        classes.Should().Contain("flex-1");
        classes.Should().Contain("min-w-0");
        classes.Should().Contain("flex-col");
        classes.Should().Contain("items-center");
        classes.Should().Contain("justify-center");
        classes.Should().Contain("gap-6");
        classes.Should().Contain("rounded-lg");
        classes.Should().Contain("border-dashed");
        classes.Should().Contain("p-6");
        classes.Should().Contain("text-center");
        classes.Should().Contain("text-balance");
        classes.Should().Contain("md:p-12");
        classes.Should().NotContain("gap-4");
        classes.Should().NotContain("rounded-xl");
        classes.Should().NotContain("q-empty");
        classes.Should().NotContain("flex-none");
        classes.Should().NotContain("w-full");
        classes.Should().NotContain("border ");
    }

    [Test]
    public void Empty_subslots_match_shadcn_v4_classes()
    {
        var header = Render<EmptyHeader>(parameters => parameters.Add(p => p.ChildContent, "Header"));
        var media = Render<EmptyMedia>(parameters => parameters
            .Add(p => p.Variant, EmptyMediaVariant.Icon)
            .Add(p => p.ChildContent, "Icon"));
        var title = Render<EmptyTitle>(parameters => parameters.Add(p => p.ChildContent, "Title"));
        var description = Render<EmptyDescription>(parameters => parameters.Add(p => p.ChildContent, "Description"));
        var content = Render<EmptyContent>(parameters => parameters.Add(p => p.ChildContent, "Content"));

        var headerClasses = header.Find("[data-slot='empty-header']").GetAttribute("class")!;
        var mediaElement = media.Find("[data-slot='empty-icon']");
        var mediaClasses = mediaElement.GetAttribute("class")!;
        var titleClasses = title.Find("[data-slot='empty-title']").GetAttribute("class")!;
        var descriptionClasses = description.Find("[data-slot='empty-description']").GetAttribute("class")!;
        var contentClasses = content.Find("[data-slot='empty-content']").GetAttribute("class")!;

        headerClasses.Should().Contain("flex");
        headerClasses.Should().Contain("max-w-sm");
        headerClasses.Should().Contain("flex-col");
        headerClasses.Should().Contain("items-center");
        headerClasses.Should().Contain("gap-2");
        headerClasses.Should().Contain("text-center");

        mediaElement.GetAttribute("data-variant").Should().Be("icon");
        mediaClasses.Should().Contain("mb-2");
        mediaClasses.Should().Contain("flex");
        mediaClasses.Should().Contain("shrink-0");
        mediaClasses.Should().Contain("items-center");
        mediaClasses.Should().Contain("justify-center");
        mediaClasses.Should().Contain("size-10");
        mediaClasses.Should().Contain("rounded-lg");
        mediaClasses.Should().Contain("bg-muted");
        mediaClasses.Should().Contain("text-foreground");
        mediaClasses.Should().Contain("[&_svg:not([class*='size-'])]:size-6");

        titleClasses.Should().Contain("text-lg");
        titleClasses.Should().Contain("font-medium");
        titleClasses.Should().Contain("tracking-tight");

        descriptionClasses.Should().Contain("text-sm/relaxed");
        descriptionClasses.Should().Contain("text-muted-foreground");
        descriptionClasses.Should().Contain("[&>a]:underline");
        descriptionClasses.Should().Contain("[&>a]:underline-offset-4");
        descriptionClasses.Should().Contain("[&>a:hover]:text-primary");

        contentClasses.Should().Contain("flex");
        contentClasses.Should().Contain("w-full");
        contentClasses.Should().Contain("max-w-sm");
        contentClasses.Should().Contain("min-w-0");
        contentClasses.Should().Contain("flex-col");
        contentClasses.Should().Contain("items-center");
        contentClasses.Should().Contain("gap-4");
        contentClasses.Should().Contain("text-sm");
        contentClasses.Should().Contain("text-balance");
    }
}
