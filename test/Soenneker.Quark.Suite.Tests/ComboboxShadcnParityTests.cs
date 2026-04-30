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

        source.Should().Contain("Align=\"@Align\"");
        source.Should().Contain("AlignOffset=\"@AlignOffset\"");
        source.Should().Contain("Slot=\"combobox-content\"");
        source.Should().Contain("data-[state=open]:animate-in");
        source.Should().Contain("data-[state=closed]:animate-out");
        source.Should().NotContain("data-open:");
        source.Should().NotContain("data-closed:");
        source.Should().Contain("[&:has([data-slot=combobox-item]:not([hidden]))>[data-slot=combobox-empty]]:hidden");
        source.Should().NotContain($"{Environment.NewLine}                Align=\"@Align\"");
    }

    [Test]
    public void Combobox_disabled_state_disables_input_primitives()
    {
        var single = Render<Combobox>(parameters => parameters
            .Add(p => p.Disabled, true)
            .Add(p => p.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenComponent<ComboboxInput>(0);
                builder.CloseComponent();
            })));

        single.Find("[data-combobox-slot='combobox-input']").HasAttribute("disabled").Should().BeTrue();

        var multiple = Render<Combobox>(parameters => parameters
            .Add(p => p.Disabled, true)
            .Add(p => p.Multiple, true)
            .Add(p => p.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenComponent<ComboboxChips>(0);
                builder.AddAttribute(1, "ChildContent", (RenderFragment)(chips =>
                {
                    chips.OpenComponent<ComboboxChipsInput>(0);
                    chips.CloseComponent();
                }));
                builder.CloseComponent();
            })));

        multiple.Find("[data-combobox-slot='combobox-chip-input']").HasAttribute("disabled").Should().BeTrue();
    }

    [Test]
    public void Combobox_trigger_does_not_show_selected_state_when_open()
    {
        var source = File.ReadAllText(Path.Combine(GetSuiteRootForCombobox(), "src", "Soenneker.Quark.Suite", "Components", "Combobox", "ComboboxInput.razor"));

        source.Should().Contain("hover:bg-muted hover:text-foreground dark:hover:bg-muted/50");
        source.Should().NotContain("aria-expanded:bg-muted");
        source.Should().NotContain("aria-expanded:text-foreground");
    }

    [Test]
    public void Combobox_demo_copy_matches_current_shadcn_docs()
    {
        var source = File.ReadAllText(Path.Combine(GetSuiteRootForCombobox(), "test", "Soenneker.Quark.Suite.Demo", "Pages", "Components", "Comboboxes.razor"));

        source.Should().Contain("Autocomplete input with a list of suggestions.");
        source.Should().Contain("A simple combobox with a list of frameworks.");
        source.Should().Contain("A combobox with multiple selection using `multiple` and `ComboboxChips`.");
        source.Should().Contain("Use the `showClear` prop to show a clear button.");
        source.Should().Contain("Use `ComboboxGroup` and `ComboboxSeparator` to group items.");
        source.Should().Contain("You can render a custom component inside `ComboboxItem`.");
        source.Should().Contain("Use the `aria-invalid` prop to make the combobox invalid.");
        source.Should().Contain("Use the `disabled` prop to disable the combobox.");
        source.Should().Contain("Use the `autoHighlight` prop to automatically highlight the first item on filter.");
        source.Should().Contain("You can trigger the combobox from a button or any other component by using the `render` prop. Move the `ComboboxInput` inside the `ComboboxContent`.");
        source.Should().Contain("<ComboboxInput Anchor=\"false\" ShowTrigger=\"false\" Placeholder=\"Search framework...\" />");
        source.Should().NotContain("<CommandInput Placeholder=\"Search framework...\"");
        source.Should().NotContain("Use disabled to prevent changes while preserving the selected value.");
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
