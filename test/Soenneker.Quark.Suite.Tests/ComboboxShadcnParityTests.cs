using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;
using Soenneker.Quark;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void Combobox_matches_shadcn_default_component_contract()
    {
        var cut = Render<Combobox>(parameters => parameters
            .Add(p => p.Open, true)
            .Add(p => p.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenComponent<ComboboxInput>(0);
                builder.CloseComponent();
            })));

        string comboboxClasses = cut.Find("[data-slot='combobox']").GetAttribute("class")!;
        string inputGroupClasses = cut.Find("[data-slot='input-group']").GetAttribute("class")!;
        string inputClasses = cut.Find("[data-combobox-slot='combobox-input']").GetAttribute("class")!;
        string triggerClasses = cut.Find("[data-slot='combobox-trigger']").GetAttribute("class")!;

        comboboxClasses.Should().Contain("relative");
        comboboxClasses.Should().NotContain("q-combobox");

        inputGroupClasses.Should().Contain("group/input-group");
        inputGroupClasses.Should().Contain("relative");
        inputGroupClasses.Should().Contain("flex");
        inputGroupClasses.Should().Contain("h-8");
        inputGroupClasses.Should().Contain("min-w-0");
        inputGroupClasses.Should().Contain("items-center");
        inputGroupClasses.Should().Contain("rounded-lg");
        inputGroupClasses.Should().Contain("border");

        inputClasses.Should().Contain("h-8");
        inputClasses.Should().Contain("w-full");
        inputClasses.Should().Contain("min-w-0");
        inputClasses.Should().Contain("px-2.5");
        inputClasses.Should().Contain("py-1");
        inputClasses.Should().Contain("text-base");
        inputClasses.Should().Contain("flex-1");
        inputClasses.Should().Contain("bg-transparent");

        triggerClasses.Should().Contain("group/button");
        triggerClasses.Should().Contain("shrink-0");
        triggerClasses.Should().Contain("items-center");
        triggerClasses.Should().Contain("gap-2");
        triggerClasses.Should().Contain("text-sm");
        triggerClasses.Should().Contain("size-6");
        triggerClasses.Should().Contain("p-0");
    }
}
