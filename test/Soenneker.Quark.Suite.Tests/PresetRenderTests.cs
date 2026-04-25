using AwesomeAssertions;
using Bunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Single_preset_applies_expected_classes()
    {
        IRenderedComponent<Div> cut = Render<Div>(parameters => parameters
            .Add(component => component.Presets, new[] { QuarkPresets.ContainerWrapper }));

        cut.Markup.Should().Contain("mx-auto");
        cut.Markup.Should().Contain("w-full");
        cut.Markup.Should().Contain("px-2");
    }

    [Test]
    public void Explicit_component_values_override_preset_values()
    {
        IRenderedComponent<Div> cut = Render<Div>(parameters => parameters
            .Add(component => component.Presets, new[] { QuarkPresets.ContainerWrapper })
            .Add(component => component.Width, Width.IsFit));

        string className = cut.Find("div").GetAttribute("class")!;

        className.Should().Contain("w-fit");
        className.Should().NotContain("w-full");
    }

    [Test]
    public void Multiple_presets_compose_in_order()
    {
        var displayPreset = new QuarkPresetToken("display-grid", static context => context.Display = Display.Grid);

        IRenderedComponent<Div> cut = Render<Div>(parameters => parameters
            .Add(component => component.Presets, new[] { displayPreset, QuarkPresets.ContainerWrapper }));

        string className = cut.Find("div").GetAttribute("class")!;

        className.Should().Contain("grid");
        className.Should().Contain("mx-auto");
        className.Should().Contain("w-full");
        className.Should().Contain("px-2");
    }

    [Test]
    public void Later_presets_override_earlier_conflicting_values()
    {
        var full = new QuarkPresetToken("width-full", static context => context.Width = Width.IsFull);
        var fit = new QuarkPresetToken("width-fit", static context => context.Width = Width.IsFit);

        IRenderedComponent<Div> cut = Render<Div>(parameters => parameters
            .Add(component => component.Presets, new[] { full, fit }));

        string className = cut.Find("div").GetAttribute("class")!;

        className.Should().Contain("w-fit");
        className.Should().NotContain("w-full");
    }

    [Test]
    public void Preset_ordering_affects_conflicting_output()
    {
        var full = new QuarkPresetToken("width-full", static context => context.Width = Width.IsFull);
        var fit = new QuarkPresetToken("width-fit", static context => context.Width = Width.IsFit);

        IRenderedComponent<Div> cut = Render<Div>(parameters => parameters
            .Add(component => component.Presets, new[] { fit, full }));

        string className = cut.Find("div").GetAttribute("class")!;

        className.Should().Contain("w-full");
        className.Should().NotContain("w-fit");
    }

    [Test]
    public void Preset_name_is_not_emitted_as_class()
    {
        IRenderedComponent<Div> cut = Render<Div>(parameters => parameters
            .Add(component => component.Presets, new[] { QuarkPresets.ContainerWrapper }));

        cut.Find("div").GetAttribute("class")!.Should().NotContain("container-wrapper");
    }

    [Test]
    public void Class_still_appends_normally()
    {
        IRenderedComponent<Div> cut = Render<Div>(parameters => parameters
            .Add(component => component.Presets, new[] { QuarkPresets.ContainerWrapper })
            .Add(component => component.Class, "custom-class"));

        string className = cut.Find("div").GetAttribute("class")!;

        className.Should().Contain("mx-auto");
        className.Should().Contain("custom-class");
    }
}
