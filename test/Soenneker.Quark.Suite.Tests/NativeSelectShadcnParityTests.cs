using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;


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
        nativeSelectClasses.Should().Contain("rounded-md");
        nativeSelectClasses.Should().Contain("border-input");
        nativeSelectClasses.Should().Contain("px-3");
        nativeSelectClasses.Should().Contain("py-2");
        nativeSelectClasses.Should().Contain("pr-9");
        nativeSelectClasses.Should().Contain("shadow-xs");
        nativeSelectClasses.Should().Contain("transition-[color,box-shadow]");
        nativeSelectClasses.Should().Contain("select-none");
        nativeSelectClasses.Should().Contain("focus-visible:ring-[3px]");
        nativeSelectClasses.Should().NotContain("q-native-select");

        nativeSelectIconClasses.Should().Contain("right-3.5");
        nativeSelectIconClasses.Should().Contain("size-4");
        nativeSelectIconClasses.Should().Contain("text-muted-foreground");
        nativeSelectIconClasses.Should().Contain("opacity-50");
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
                builder.AddAttribute(5, "ChildContent", (RenderFragment)(child =>
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
        optgroup.GetAttribute("class").Should().ContainAll("bg-[Canvas]", "text-[CanvasText]");
    }
}
