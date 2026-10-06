namespace Soenneker.Quark.Suite.Benchmarks;

public sealed class ComponentStylingProbe : Component
{
    public ComponentStylingProbe(bool styled = false, bool usePreset = false)
    {
        QuarkOptions = new QuarkOptions { AlwaysRender = true };
        if (styled)
        {
            Width = 240;
            Display = Soenneker.Quark.Display.Flex;
        }
        if (usePreset)
            Preset = new QuarkPresetToken("benchmark", static context => context.Width = 240);
    }

    public object Rebuild() => BuildAttributes();
}
