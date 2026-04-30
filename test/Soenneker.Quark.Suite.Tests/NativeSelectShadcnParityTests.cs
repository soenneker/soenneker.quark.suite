using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;
using System;
using System.IO;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void NativeSelect_matches_shadcn_base_classes()
    {
        var nativeSelect = Render<NativeSelect>(parameters => parameters
            .Add(p => p.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenElement(0, "option");
                builder.AddAttribute(1, "value", "");
                builder.AddContent(2, "Select status");
                builder.CloseElement();
            })));

        var nativeSelectClasses = nativeSelect.Find("[data-slot='native-select']").GetAttribute("class")!;
        var nativeSelectIconClasses = nativeSelect.Find("[data-slot='native-select-icon']").GetAttribute("class")!;

        var nativeSelectWrapperClasses = nativeSelect.Find("[data-slot='native-select-wrapper']").GetAttribute("class")!;

        nativeSelectWrapperClasses.Should().Contain("w-fit");
        nativeSelectWrapperClasses.Should().Contain("has-[select:disabled]:opacity-50");

        nativeSelectClasses.Should().Contain("h-9");
        nativeSelectClasses.Should().Contain("w-full");
        nativeSelectClasses.Should().Contain("min-w-0");
        nativeSelectClasses.Should().Contain("appearance-none");
        nativeSelectClasses.Should().Contain("rounded-md");
        nativeSelectClasses.Should().Contain("border-input");
        nativeSelectClasses.Should().Contain("bg-transparent");
        nativeSelectClasses.Should().Contain("px-3");
        nativeSelectClasses.Should().Contain("py-2");
        nativeSelectClasses.Should().Contain("pr-9");
        nativeSelectClasses.Should().Contain("shadow-xs");
        nativeSelectClasses.Should().Contain("transition-[color,box-shadow]");
        nativeSelectClasses.Should().Contain("focus-visible:ring-[3px]");
        nativeSelectClasses.Should().Contain("data-[size=sm]:h-8");
        nativeSelectClasses.Should().Contain("data-[size=sm]:py-1");
        nativeSelectClasses.Should().Contain("dark:bg-input/30");
        nativeSelectClasses.Should().Contain("dark:hover:bg-input/50");
        nativeSelectClasses.Should().Contain("dark:aria-invalid:ring-destructive/40");
        nativeSelectClasses.Should().NotContain("h-8 w-full");
        nativeSelectClasses.Should().NotContain("rounded-lg");
        nativeSelectClasses.Should().NotContain("pl-2.5");
        nativeSelectClasses.Should().NotContain("pr-8");
        nativeSelectClasses.Should().NotContain("data-[size=sm]:h-7");
        nativeSelectClasses.Should().NotContain("data-[size=sm]:rounded-[min(var(--radius-md),10px)]");
        nativeSelectClasses.Should().NotContain("transition-[color,shadow]");
        nativeSelectClasses.Should().NotContain("q-native-select");

        nativeSelectIconClasses.Should().Contain("right-3.5");
        nativeSelectIconClasses.Should().Contain("size-4");
        nativeSelectIconClasses.Should().Contain("text-muted-foreground");
        nativeSelectIconClasses.Should().Contain("opacity-50");
        nativeSelectIconClasses.Should().Contain("select-none");
        nativeSelectIconClasses.Should().NotContain("right-2.5");
    }

    [Test]
    public void NativeSelect_option_and_optgroup_match_shadcn_canvas_contract()
    {
        var cut = Render<NativeSelect>(parameters => parameters
            .Add(p => p.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenComponent<NativeSelectOption>(0);
                builder.AddAttribute(1, "Value", "");
                builder.AddAttribute(2, "ChildContent", (RenderFragment)(child => child.AddContent(0, "Select status")));
                builder.CloseComponent();

                builder.OpenComponent<NativeSelectOptGroup>(3);
                builder.AddAttribute(4, "Label", "Engineering");
                builder.AddAttribute(5, "Disabled", true);
                builder.AddAttribute(6, "ChildContent", (RenderFragment)(child =>
                {
                    child.OpenComponent<NativeSelectOption>(0);
                    child.AddAttribute(1, "Value", "frontend");
                    child.AddAttribute(2, "ChildContent", (RenderFragment)(option => option.AddContent(0, "Frontend")));
                    child.CloseComponent();
                }));
                builder.CloseComponent();
            })));

        var option = cut.Find("[data-slot='native-select-option']");
        var optgroup = cut.Find("[data-slot='native-select-optgroup']");

        option.GetAttribute("value").Should().Be(string.Empty);
        option.GetAttribute("class").Should().ContainAll("bg-[Canvas]", "text-[CanvasText]");
        optgroup.GetAttribute("label").Should().Be("Engineering");
        optgroup.GetAttribute("disabled").Should().Be(string.Empty);
        optgroup.GetAttribute("class").Should().ContainAll("bg-[Canvas]", "text-[CanvasText]");
    }

    [Test]
    public void NativeSelect_demo_examples_match_current_shadcn_docs_examples()
    {
        var source = File.ReadAllText(Path.Combine(GetSuiteRootForNativeSelect(), "test", "Soenneker.Quark.Suite.Demo", "Pages", "Components", "NativeSelects.razor"));

        source.Should().Contain("Title=\"Basic\"");
        source.Should().Contain("Title=\"With Groups\"");
        source.Should().Contain("Title=\"Sizes\"");
        source.Should().Contain("Title=\"With Field\"");
        source.Should().Contain("Title=\"Disabled\"");
        source.Should().Contain("Title=\"Invalid\"");
        source.Should().Contain("Select a fruit");
        source.Should().Contain("Select a food");
        source.Should().Contain("NativeSelectOptGroup Label=\"Fruits\"");
        source.Should().Contain("NativeSelectSize.Sm");
        source.Should().Contain("FieldLabel For=\"native-select-country\"");
        source.Should().Contain("Select your country of residence.");
        source.Should().Contain("Error state");
        source.Should().NotContain("Native Select vs Select");
        source.Should().NotContain("Select status");
        source.Should().NotContain("Select department");
    }

    private static string GetSuiteRootForNativeSelect()
    {
        var directory = AppContext.BaseDirectory;

        while (!File.Exists(Path.Combine(directory, "Soenneker.Quark.Suite.slnx")))
            directory = Directory.GetParent(directory)!.FullName;

        return directory;
    }
}
