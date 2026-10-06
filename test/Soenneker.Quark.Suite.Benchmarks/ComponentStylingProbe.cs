namespace Soenneker.Quark.Suite.Benchmarks;

public sealed class ComponentStylingProbe : Component
{
    public ComponentStylingProbe(bool styled = false, bool usePreset = false)
    {
        QuarkOptions = new QuarkOptions { AlwaysRender = true };
        if (styled)
        {
            Width = Quark.Width.Token("[240px]");
            Display = Soenneker.Quark.Display.Flex;
        }
        if (usePreset)
            Preset = new QuarkPresetToken("benchmark", static context => context.Width = Quark.Width.Token("[240px]"));
    }

    public object Rebuild() => BuildAttributes();
}
