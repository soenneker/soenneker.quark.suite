using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Select_slots_match_shadcn_base_classes()
    {
        var triggerCut = Render<Select<string>>(parameters => parameters
            .Add(p => p.DefaultItemText, "Select a fruit")
            .Add(p => p.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenComponent<SelectTrigger>(0);
                builder.AddAttribute(1, "ChildContent", (RenderFragment)(triggerBuilder =>
                {
                    triggerBuilder.OpenComponent<SelectValue>(0);
                    triggerBuilder.CloseComponent();
                }));
                builder.CloseComponent();
            })));

        var selectClasses = triggerCut.Find("[data-slot='select']").GetAttribute("class")!;
        var triggerClasses = triggerCut.Find("[data-slot='select-trigger']").GetAttribute("class")!;
        var valueClasses = triggerCut.Find("[data-slot='select-value']").GetAttribute("class");

        selectClasses.Should().Contain("group/select");
        selectClasses.Should().Contain("relative");
        selectClasses.Should().Contain("w-full");
        selectClasses.Should().Contain("max-w-full");
        selectClasses.Should().NotContain("q-select");

        triggerClasses.Should().Contain("flex");
        triggerClasses.Should().Contain("items-center");
        triggerClasses.Should().Contain("justify-between");
        triggerClasses.Should().Contain("gap-1.5");
        triggerClasses.Should().Contain("rounded-lg");
        triggerClasses.Should().Contain("border");
        triggerClasses.Should().Contain("bg-transparent");
        triggerClasses.Should().Contain("py-2");
        triggerClasses.Should().Contain("pr-2");
        triggerClasses.Should().Contain("pl-2.5");
        triggerClasses.Should().Contain("text-sm");
        triggerClasses.Should().Contain("transition-colors");
        triggerClasses.Should().Contain("outline-none");
        triggerClasses.Should().Contain("data-[size=default]:h-8");
        triggerClasses.Should().Contain("data-[size=sm]:h-7");
        triggerClasses.Should().Contain("*:data-[slot=select-value]:line-clamp-1");
        triggerClasses.Should().Contain("*:data-[slot=select-value]:flex");
        triggerClasses.Should().Contain("*:data-[slot=select-value]:items-center");
        triggerClasses.Should().Contain("*:data-[slot=select-value]:gap-1.5");
        triggerClasses.Should().NotContain("q-select-trigger");

        valueClasses.Should().BeNullOrEmpty();
        valueClasses.Should().NotContain("q-select-value");
        valueClasses.Should().NotContain("text-left");
    }

    [Test]
    public void SelectTrigger_inside_field_defaults_to_full_width()
    {
        var cut = Render<Field>(parameters => parameters
            .AddChildContent<FieldLabel>(child => child.AddChildContent("Month"))
            .AddChildContent<Select<string>>(child => child
                .Add(p => p.DefaultItemText, "MM")
                .Add(p => p.ChildContent, (RenderFragment)(builder =>
                {
                    builder.OpenComponent<SelectTrigger>(0);
                    builder.AddAttribute(1, "ChildContent", (RenderFragment)(triggerBuilder =>
                    {
                        triggerBuilder.OpenComponent<SelectValue>(0);
                        triggerBuilder.CloseComponent();
                    }));
                    builder.CloseComponent();
                }))));

        var triggerClasses = cut.Find("[data-slot='select-trigger']").GetAttribute("class")!;

        triggerClasses.Should().Contain("w-full");
    }
}
