using AwesomeAssertions;
using Bunit;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void FieldSeparator_matches_shadcn_base_classes()
    {
        var cut = Render<FieldSeparator>(parameters => parameters
            .AddChildContent("Appearance Settings"));

        string classes = cut.Find("[data-slot='field-separator']").GetAttribute("class")!;

        classes.Should().Contain("relative");
        classes.Should().Contain("h-5");
        classes.Should().Contain("text-sm");
        classes.Should().Contain("group-data-[variant=outline]/field-group:-mb-2");
        classes.Should().NotContain("-my-2");
    }

    [Fact]
    public void FieldLabel_matches_shadcn_base_classes()
    {
        var cut = Render<FieldLabel>(parameters => parameters
            .Add(p => p.For, "email")
            .Add(p => p.ChildContent, "Email"));

        string classes = cut.Find("[data-slot='field-label']").GetAttribute("class")!;

        classes.Should().Contain("items-center");
        classes.Should().Contain("group/field-label");
        classes.Should().Contain("peer/field-label");
        classes.Should().Contain("has-data-checked:border-primary/30");
        classes.Should().Contain("has-[>[data-slot=field]]:rounded-lg");
        classes.Should().Contain("*:data-[slot=field]:p-2.5");
        classes.Should().NotContain("has-data-[state=checked]");
        classes.Should().NotContain("rounded-md");
        classes.Should().NotContain("p-4");
    }

    [Fact]
    public void FieldTitle_matches_shadcn_slider_demo_classes()
    {
        var cut = Render<FieldTitle>(parameters => parameters
            .AddChildContent("Price Range"));

        string classes = cut.Find("[data-slot='field-label']").GetAttribute("class")!;

        classes.Should().Contain("flex");
        classes.Should().Contain("w-fit");
        classes.Should().Contain("items-center");
        classes.Should().Contain("gap-2");
        classes.Should().Contain("text-sm");
        classes.Should().Contain("font-medium");
        classes.Should().Contain("leading-snug");
    }

    [Fact]
    public void FieldDescription_matches_shadcn_text_alignment_classes()
    {
        var cut = Render<FieldDescription>(parameters => parameters
            .AddChildContent("Set your budget range."));

        string classes = cut.Find("[data-slot='field-description']").GetAttribute("class")!;

        classes.Should().Contain("text-left");
        classes.Should().Contain("text-sm");
        classes.Should().Contain("font-normal");
        classes.Should().Contain("leading-normal");
        classes.Should().Contain("text-muted-foreground");
        classes.Should().NotContain("text-start");
    }
}
