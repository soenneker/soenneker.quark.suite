using AwesomeAssertions;
using Bunit;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Check_matches_shadcn_checkbox_component_contract()
    {
        var cut = Render<Check>(parameters => parameters
            .Add(p => p.Id, "terms-checkbox-basic")
            .Add(p => p.Checked, true)
            .AddChildContent("Accept terms and conditions"));

        var checkbox = cut.Find("[data-slot='checkbox']");
        var label = cut.Find("label");
        var indicator = cut.Find("[data-slot='checkbox-indicator']");

        var checkboxClasses = checkbox.GetAttribute("class")!;
        var labelClasses = label.GetAttribute("class")!;
        var indicatorClasses = indicator.GetAttribute("class")!;

        checkboxClasses.Should().Contain("peer");
        checkboxClasses.Should().Contain("size-4");
        checkboxClasses.Should().Contain("shrink-0");
        checkboxClasses.Should().Contain("rounded-[4px]");
        checkboxClasses.Should().Contain("border");
        checkboxClasses.Should().Contain("border-input");
        checkboxClasses.Should().Contain("shadow-xs");
        checkboxClasses.Should().Contain("transition-shadow");
        checkboxClasses.Should().Contain("outline-none");
        checkboxClasses.Should().Contain("focus-visible:border-ring");
        checkboxClasses.Should().Contain("focus-visible:ring-[3px]");
        checkboxClasses.Should().Contain("focus-visible:ring-ring/50");
        checkboxClasses.Should().Contain("disabled:cursor-not-allowed");
        checkboxClasses.Should().Contain("disabled:opacity-50");
        checkboxClasses.Should().Contain("aria-invalid:border-destructive");
        checkboxClasses.Should().Contain("aria-invalid:ring-destructive/20");
        checkboxClasses.Should().Contain("dark:bg-input/30");
        checkboxClasses.Should().Contain("dark:aria-invalid:ring-destructive/40");
        checkboxClasses.Should().Contain("data-[state=checked]:border-primary");
        checkboxClasses.Should().Contain("data-[state=checked]:bg-primary");
        checkboxClasses.Should().Contain("data-[state=checked]:text-primary-foreground");
        checkboxClasses.Should().Contain("dark:data-[state=checked]:bg-primary");
        checkboxClasses.Should().Contain("data-[state=indeterminate]:border-primary");
        checkboxClasses.Should().NotContain("data-checked:");
        checkboxClasses.Should().NotContain(" transition-colors");

        labelClasses.Should().Contain("flex");
        labelClasses.Should().Contain("items-center");
        labelClasses.Should().Contain("gap-2");
        labelClasses.Should().Contain("text-sm");
        labelClasses.Should().Contain("font-medium");
        labelClasses.Should().Contain("leading-none");
        labelClasses.Should().Contain("select-none");
        labelClasses.Should().Contain("group-data-[disabled=true]:pointer-events-none");
        labelClasses.Should().Contain("group-data-[disabled=true]:opacity-50");
        labelClasses.Should().Contain("peer-disabled:cursor-not-allowed");
        labelClasses.Should().Contain("peer-disabled:opacity-50");

        indicatorClasses.Should().Contain("grid");
        indicatorClasses.Should().Contain("place-content-center");
        indicatorClasses.Should().Contain("text-current");
        indicatorClasses.Should().Contain("transition-none");
    }

    [Test]
    public void Check_inside_field_preserves_explicit_id_and_renders_without_wrapper()
    {
        var cut = Render<Field>(parameters => parameters
            .Add(p => p.Horizontal, true)
            .AddChildContent<Check>(child => child
                .Add(p => p.Id, "idx-terms")
                .Add(p => p.Checked, true))
            .AddChildContent<FieldLabel>(child => child
                .Add(p => p.For, "idx-terms")
                .Add(p => p.ChildContent, "I agree to the terms and conditions")));

        var field = cut.Find("[data-slot='field']");
        var checkbox = field.Children[0];
        var label = cut.Find("[data-slot='field-label']");

        checkbox.GetAttribute("data-slot").Should().Be("checkbox");
        checkbox.GetAttribute("id").Should().Be("idx-terms");
        label.GetAttribute("for").Should().Be("idx-terms");
    }
}
