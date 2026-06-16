using AwesomeAssertions;
using Bunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Single_preset_applies_expected_classes()
    {
        var cut = Render<Div>(parameters => parameters
            .Add(component => component.Preset, QuarkPresets.ContainerWrapper));

        cut.Markup.Should().Contain("mx-auto");
        cut.Markup.Should().Contain("w-full");
        cut.Markup.Should().Contain("max-w-[1400px]");
        cut.Markup.Should().Contain("px-2");
        cut.Markup.Should().Contain("container-wrapper");
    }

    [Test]
    public void Singular_preset_composes_before_plural_presets()
    {
        var full = new QuarkPresetToken("width-full", static context => context.Width = Width.IsFull);
        var fit = new QuarkPresetToken("width-fit", static context => context.Width = Width.IsFit);

        var cut = Render<Div>(parameters => parameters
            .Add(component => component.Preset, full)
            .Add(component => component.Presets, new[] { fit }));

        var className = cut.Find("div").GetAttribute("class")!;

        className.Should().Contain("w-fit");
        className.Should().NotContain("w-full");
    }

    [Test]
    public void Explicit_component_values_override_preset_values()
    {
        var cut = Render<Div>(parameters => parameters
                                            .Add(component => component.Preset, QuarkPresets.ContainerWrapper)
                                            .Add(component => component.Width, Width.IsFit));

        var className = cut.Find("div").GetAttribute("class")!;

        className.Should().Contain("w-fit");
        className.Should().NotContain("w-full");
        className.Should().Contain("max-w-[1400px]");
    }

    [Test]
    public void Preset_background_color_applies_expected_class()
    {
        var background = new QuarkPresetToken("bg-primary", static context => context.BackgroundColor = BackgroundColor.Primary);

        var cut = Render<Div>(parameters => parameters
                                            .Add(component => component.Preset, background));

        cut.Find("div").GetAttribute("class")!.Should().Contain("bg-primary");
    }

    [Test]
    public void Explicit_component_background_color_overrides_preset_background_color()
    {
        var background = new QuarkPresetToken("bg-primary", static context => context.BackgroundColor = BackgroundColor.Primary);

        var cut = Render<Div>(parameters => parameters
                                            .Add(component => component.Preset, background)
                                            .Add(component => component.BackgroundColor, BackgroundColor.Secondary));

        var className = cut.Find("div").GetAttribute("class")!;

        className.Should().Contain("bg-secondary");
        className.Should().NotContain("bg-primary");
    }

    [Test]
    public void Preset_border_color_applies_through_component_override()
    {
        var border = new QuarkPresetToken("border-primary", static context => context.BorderColor = BorderColor.Primary);

        var cut = Render<Announcement>(parameters => parameters
                                                 .Add(component => component.Preset, border)
                                                 .Add(component => component.ChildContent, "Preset border"));

        var className = cut.Find("[data-slot='announcement']").GetAttribute("class")!;

        className.Should().Contain("border-primary");
        className.Should().NotContain("border-border");
    }

    [Test]
    public void Explicit_component_border_color_overrides_preset_border_color()
    {
        var border = new QuarkPresetToken("border-primary", static context => context.BorderColor = BorderColor.Primary);

        var cut = Render<Announcement>(parameters => parameters
                                                 .Add(component => component.Preset, border)
                                                 .Add(component => component.BorderColor, BorderColor.Secondary)
                                                 .Add(component => component.ChildContent, "Explicit border"));

        var className = cut.Find("[data-slot='announcement']").GetAttribute("class")!;

        className.Should().Contain("border-secondary");
        className.Should().NotContain("border-primary");
    }

    [Test]
    public void Multiple_presets_compose_in_order()
    {
        var displayPreset = new QuarkPresetToken("display-grid", static context => context.Display = Display.Grid);

        var cut = Render<Div>(parameters => parameters
            .Add(component => component.Presets, new[] { displayPreset, QuarkPresets.ContainerWrapper }));

        var className = cut.Find("div").GetAttribute("class")!;

        className.Should().Contain("grid");
        className.Should().Contain("mx-auto");
        className.Should().Contain("w-full");
        className.Should().Contain("max-w-[1400px]");
        className.Should().Contain("px-2");
        className.Should().Contain("container-wrapper");
    }

    [Test]
    public void Later_presets_override_earlier_conflicting_values()
    {
        var full = new QuarkPresetToken("width-full", static context => context.Width = Width.IsFull);
        var fit = new QuarkPresetToken("width-fit", static context => context.Width = Width.IsFit);

        var cut = Render<Div>(parameters => parameters
            .Add(component => component.Presets, new[] { full, fit }));

        var className = cut.Find("div").GetAttribute("class")!;

        className.Should().Contain("w-fit");
        className.Should().NotContain("w-full");
    }

    [Test]
    public void Preset_ordering_affects_conflicting_output()
    {
        var full = new QuarkPresetToken("width-full", static context => context.Width = Width.IsFull);
        var fit = new QuarkPresetToken("width-fit", static context => context.Width = Width.IsFit);

        var cut = Render<Div>(parameters => parameters
            .Add(component => component.Presets, new[] { fit, full }));

        var className = cut.Find("div").GetAttribute("class")!;

        className.Should().Contain("w-full");
        className.Should().NotContain("w-fit");
    }

    [Test]
    public void Container_wrapper_preset_emits_shadcn_class()
    {
        var cut = Render<Div>(parameters => parameters
            .Add(component => component.Preset, QuarkPresets.ContainerWrapper));

        cut.Find("div").GetAttribute("class")!.Should().Contain("container-wrapper");
    }

    [Test]
    public void Class_still_appends_normally()
    {
        var cut = Render<Div>(parameters => parameters
                                            .Add(component => component.Preset, QuarkPresets.ContainerWrapper)
                                            .Add(component => component.Class, "custom-class"));

        var className = cut.Find("div").GetAttribute("class")!;

        className.Should().Contain("mx-auto");
        className.Should().Contain("container-wrapper");
        className.Should().Contain("custom-class");
    }
}
