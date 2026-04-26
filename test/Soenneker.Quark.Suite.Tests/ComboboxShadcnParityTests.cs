using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;
using System;
using System.IO;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Combobox_matches_shadcn_default_component_contract()
    {
        var cut = Render<Combobox>(parameters => parameters
            .Add(p => p.Open, true)
            .Add(p => p.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenComponent<ComboboxInput>(0);
                builder.CloseComponent();
            })));

        var comboboxClasses = cut.Find("[data-slot='combobox']").GetAttribute("class")!;
        var inputGroupClasses = cut.Find("[data-slot='input-group']").GetAttribute("class")!;
        var inputClasses = cut.Find("[data-combobox-slot='combobox-input']").GetAttribute("class")!;
        var triggerClasses = cut.Find("[data-slot='combobox-trigger']").GetAttribute("class")!;

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

    [Test]
    public void Combobox_content_wires_popover_positioning_and_slot_to_shared_source()
    {
        var source = File.ReadAllText(Path.Combine(GetSuiteRootForCombobox(), "src", "Soenneker.Quark.Suite", "Components", "Combobox", "ComboboxContent.razor"));

        source.Should().Contain("PlacementAlign=\"@PlacementAlign\"");
        source.Should().Contain("AlignOffset=\"@AlignOffset\"");
        source.Should().Contain("Slot=\"combobox-content\"");
        source.Should().Contain("[&:has([data-slot=combobox-item]:not([hidden]))>[data-slot=combobox-empty]]:hidden");
        source.Should().NotContain($"{Environment.NewLine}                Align=\"@PlacementAlign\"");
    }

    [Test]
    public void Combobox_empty_hides_after_items_register()
    {
        var cut = Render<Combobox>(parameters => parameters
            .Add(p => p.Open, true)
            .Add(p => p.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenComponent<ComboboxInput>(0);
                builder.CloseComponent();

                builder.OpenComponent<ComboboxContent>(1);
                builder.AddAttribute(2, "ChildContent", (RenderFragment)(contentBuilder =>
                {
                    contentBuilder.OpenComponent<ComboboxEmpty>(0);
                    contentBuilder.AddAttribute(1, "ChildContent", (RenderFragment)(emptyBuilder => emptyBuilder.AddContent(0, "No framework found.")));
                    contentBuilder.CloseComponent();

                    contentBuilder.OpenComponent<ComboboxList>(2);
                    contentBuilder.AddAttribute(3, "ChildContent", (RenderFragment)(listBuilder =>
                    {
                        listBuilder.OpenComponent<ComboboxItem>(0);
                        listBuilder.AddAttribute(1, nameof(ComboboxItem.Value), "Next.js");
                        listBuilder.AddAttribute(2, "ChildContent", (RenderFragment)(itemBuilder => itemBuilder.AddContent(0, "Next.js")));
                        listBuilder.CloseComponent();
                    }));
                    contentBuilder.CloseComponent();
                }));
                builder.CloseComponent();
            })));

        cut.FindAll("[data-slot='combobox-item']").Should().ContainSingle();
        cut.Find("[data-slot='combobox-content']").GetAttribute("class")!
            .Should().Contain("[&:has([data-slot=combobox-item]:not([hidden]))>[data-slot=combobox-empty]]:hidden");
    }

    private static string GetSuiteRootForCombobox()
    {
        var directory = AppContext.BaseDirectory;

        while (!File.Exists(Path.Combine(directory, "Soenneker.Quark.Suite.slnx")))
            directory = Directory.GetParent(directory)!.FullName;

        return directory;
    }
}
