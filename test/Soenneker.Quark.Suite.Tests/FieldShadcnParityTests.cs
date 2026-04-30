using AwesomeAssertions;
using Bunit;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void FieldSeparator_matches_shadcn_base_classes()
    {
        var cut = Render<FieldSeparator>(parameters => parameters
            .AddChildContent("Appearance Settings"));

        var classes = cut.Find("[data-slot='field-separator']").GetAttribute("class")!;

        classes.Should().Contain("relative");
        classes.Should().Contain("-my-2");
        classes.Should().Contain("h-5");
        classes.Should().Contain("text-sm");
        classes.Should().Contain("group-data-[variant=outline]/field-group:-mb-2");
    }

    [Test]
    public void FieldLabel_matches_shadcn_base_classes()
    {
        var cut = Render<FieldLabel>(parameters => parameters
            .Add(p => p.For, "email")
            .Add(p => p.ChildContent, "Email"));

        var classes = cut.Find("[data-slot='field-label']").GetAttribute("class")!;

        classes.Should().Contain("group/field-label");
        classes.Should().Contain("peer/field-label");
        classes.Should().Contain("leading-snug");
        classes.Should().Contain("has-data-[state=checked]:border-primary");
        classes.Should().Contain("has-data-[state=checked]:bg-primary/5");
        classes.Should().Contain("has-[>[data-slot=field]]:rounded-md");
        classes.Should().Contain("[&>*]:data-[slot=field]:p-4");
        classes.Should().Contain("dark:has-data-[state=checked]:bg-primary/10");
        classes.Should().NotContain("has-data-checked:border-primary/30");
        classes.Should().NotContain("rounded-lg");
        classes.Should().NotContain("*:data-[slot=field]:p-2.5");
    }

    [Test]
    public void FieldTitle_matches_shadcn_slider_demo_classes()
    {
        var cut = Render<FieldTitle>(parameters => parameters
            .AddChildContent("Price Range"));

        var classes = cut.Find("[data-slot='field-label']").GetAttribute("class")!;

        classes.Should().Contain("flex");
        classes.Should().Contain("w-fit");
        classes.Should().Contain("items-center");
        classes.Should().Contain("gap-2");
        classes.Should().Contain("text-sm");
        classes.Should().Contain("font-medium");
        classes.Should().Contain("leading-snug");
    }

    [Test]
    public void FieldDescription_matches_shadcn_text_alignment_classes()
    {
        var cut = Render<FieldDescription>(parameters => parameters
            .AddChildContent("Set your budget range."));

        var classes = cut.Find("[data-slot='field-description']").GetAttribute("class")!;

        classes.Should().Contain("text-sm");
        classes.Should().Contain("font-normal");
        classes.Should().Contain("leading-normal");
        classes.Should().Contain("text-muted-foreground");
        classes.Should().Contain("group-has-[[data-orientation=horizontal]]/field:text-balance");
        classes.Should().NotContain("text-left");
        classes.Should().NotContain("text-start");
    }

    [Test]
    public void Selectable_horizontal_field_matches_shadcn_card_toggle_classes()
    {
        var cut = Render<Field>(parameters => parameters
            .Add(p => p.Horizontal, true)
            .Add(p => p.Selectable, true)
            .AddChildContent<Check>(child => child
                .Add(p => p.Id, "terms")
                .Add(p => p.Checked, true))
            .AddChildContent<FieldContent>(child => child
                .AddChildContent<FieldTitle>(title => title.AddChildContent("Terms"))));

        var classes = cut.Find("[data-slot='field']").GetAttribute("class")!;

        classes.Should().Contain("overflow-hidden");
        classes.Should().Contain("rounded-lg");
        classes.Should().Contain("border");
        classes.Should().Contain("px-3!");
        classes.Should().Contain("py-1.5!");
        classes.Should().Contain("transition-all");
        classes.Should().Contain("duration-100");
        classes.Should().Contain("ease-linear");
        classes.Should().Contain("has-[>[data-slot=checkbox][data-state=checked]]:border-primary/30");
        classes.Should().Contain("has-[>[data-slot=checkbox][data-state=checked]]:bg-muted");
    }

    [Test]
    public void Field_orientation_responsive_matches_shadcn_contract()
    {
        var cut = Render<Field>(parameters => parameters
            .Add(p => p.Orientation, "responsive")
            .AddChildContent<FieldLabel>(label => label.AddChildContent("Notifications"))
            .AddChildContent<FieldContent>(content => content
                .AddChildContent<FieldDescription>(description => description.AddChildContent("Choose how updates are delivered."))));

        var field = cut.Find("[data-slot='field']");
        var classes = field.GetAttribute("class")!;

        field.GetAttribute("data-orientation").Should().Be("responsive");
        classes.Should().Contain("flex-col");
        classes.Should().Contain("@md/field-group:flex-row");
        classes.Should().Contain("@md/field-group:items-center");
        classes.Should().Contain("[&>*]:w-full");
        classes.Should().Contain("@md/field-group:[&>*]:w-auto");
        classes.Should().Contain("@md/field-group:[&>[data-slot=field-label]]:flex-auto");
        classes.Should().Contain("@md/field-group:has-[>[data-slot=field-content]]:items-start");
    }

    [Test]
    public void FieldGroup_supports_shadcn_checkbox_group_slot()
    {
        var cut = Render<FieldGroup>(parameters => parameters
            .Add(p => p.Slot, "checkbox-group")
            .AddChildContent<Field>(field => field
                .Add(p => p.Horizontal, true)
                .AddChildContent<Check>(check => check.Add(p => p.Id, "hard-disks"))
                .AddChildContent<FieldLabel>(label => label
                    .Add(p => p.For, "hard-disks")
                    .AddChildContent("Hard disks"))));

        var group = cut.Find("[data-slot='checkbox-group']");
        var classes = group.GetAttribute("class")!;

        classes.Should().Contain("group/field-group");
        classes.Should().Contain("@container/field-group");
        classes.Should().Contain("data-[slot=checkbox-group]:gap-3");
    }

    [Test]
    public void FieldLabel_wrapped_horizontal_field_matches_shadcn_checkbox_chip_structure()
    {
        var cut = Render<FieldLabel>(parameters => parameters
            .Add(p => p.For, "social-media")
            .Add(p => p.Class, "w-fit!")
            .AddChildContent<Field>(field => field
                .Add(p => p.Horizontal, true)
                .Add(p => p.Gap, Gap.Token("1.5"))
                .Add(p => p.Overflow, Overflow.Hidden)
                .Add(p => p.Class, "px-3! py-1.5! transition-all duration-100 ease-linear group-has-data-[state=checked]/field-label:px-2!")
                .AddChildContent<Check>(child => child
                    .Add(p => p.Id, "social-media")
                    .Add(p => p.Value, "social-media")
                    .Add(p => p.Checked, true)
                    .Add(p => p.IndicatorClass, "-ml-6 -translate-x-1 rounded-full transition-all duration-100 ease-linear data-[state=checked]:ml-0 data-[state=checked]:translate-x-0"))
                .AddChildContent<FieldTitle>(title => title.AddChildContent("Social Media"))));

        var label = cut.Find("[data-slot='field-label']");
        var field = cut.Find("[data-slot='field']");

        label.TagName.Should().Be("LABEL");
        label.GetAttribute("for").Should().Be("social-media");
        label.GetAttribute("class")!.Should().Contain("w-fit!");

        var fieldClasses = field.GetAttribute("class")!;
        fieldClasses.Should().Contain("gap-1.5");
        fieldClasses.Should().Contain("overflow-hidden");
        fieldClasses.Should().Contain("px-3!");
        fieldClasses.Should().Contain("py-1.5!");
        fieldClasses.Should().Contain("group-has-data-[state=checked]/field-label:px-2!");
        field.Children[1].GetAttribute("data-slot").Should().Be("field-label");
    }

    [Test]
    public void FieldError_supports_shadcn_errors_collection_contract()
    {
        var empty = Render<FieldError>();
        empty.Markup.Should().BeEmpty();

        var single = Render<FieldError>(parameters => parameters
            .Add(p => p.Errors, ["Required", "Required", null]));
        single.Markup.Should().Contain("role=\"alert\"");
        single.Markup.Should().Contain("Required");
        single.FindAll("li").Should().BeEmpty();

        var multiple = Render<FieldError>(parameters => parameters
            .Add(p => p.Errors, ["Required", "Must be a valid email", "Required"]));
        multiple.Find("[data-slot='field-error']").GetAttribute("class")!.Should().Contain("text-destructive");
        multiple.Find("ul").GetAttribute("class")!.Should().Contain("ml-4");
        multiple.FindAll("li").Should().HaveCount(2);
    }
}
