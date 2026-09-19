using AwesomeAssertions;
using Bunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Filter_effects_accept_fluent_builders_and_update_on_parameter_changes()
    {
        var cut = Render<Div>(parameters => parameters
            .Add(component => component.BackdropBlur, BackdropBlur.Sm.OnMd.Lg)
            .Add(component => component.BackdropBrightness, BackdropBrightness.Is95)
            .Add(component => component.Blur, Blur.Sm)
            .Add(component => component.Invert, Invert.Is100));

        var classes = cut.Find("div").ClassList;
        classes.Should().Contain("backdrop-blur-sm");
        classes.Should().Contain("md:backdrop-blur-lg");
        classes.Should().Contain("backdrop-brightness-95");
        classes.Should().Contain("blur-sm");
        classes.Should().Contain("invert-100");

        cut.Render(parameters => parameters
            .Add(component => component.BackdropBlur, BackdropBlur.None)
            .Add(component => component.Blur, Blur.Lg));

        classes = cut.Find("div").ClassList;
        classes.Should().Contain("backdrop-blur-none");
        classes.Should().Contain("blur-lg");
        classes.Should().NotContain("backdrop-blur-sm");
        classes.Should().NotContain("md:backdrop-blur-lg");
        classes.Should().NotContain("blur-sm");
    }

    [Test]
    public void Filter_effects_resolve_presets_and_explicit_overrides()
    {
        var preset = new QuarkPresetToken("frosted", static context =>
        {
            context.BackdropBlur = BackdropBlur.Sm;
            context.BackdropBrightness = BackdropBrightness.Is95;
            context.Blur = Blur.Sm;
        });

        var cut = Render<Div>(parameters => parameters
            .Add(component => component.Preset, preset)
            .Add(component => component.BackdropBlur, BackdropBlur.Lg));

        var classes = cut.Find("div").ClassList;
        classes.Should().Contain("backdrop-blur-lg");
        classes.Should().Contain("backdrop-brightness-95");
        classes.Should().Contain("blur-sm");
        classes.Should().NotContain("backdrop-blur-sm");
    }

    [Test]
    public void Filter_effects_node_editor_uses_typed_backdrop_default_and_override()
    {
        var cut = Render<NodeEditorControls>();
        cut.Find("[data-slot='node-editor-controls']").ClassList.Should().Contain("backdrop-blur-sm");

        cut.Render(parameters => parameters.Add(component => component.BackdropBlur, BackdropBlur.None));

        var classes = cut.Find("[data-slot='node-editor-controls']").ClassList;
        classes.Should().Contain("backdrop-blur-none");
        classes.Should().NotContain("backdrop-blur-sm");
    }
}
