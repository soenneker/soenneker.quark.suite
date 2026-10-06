using BenchmarkDotNet.Attributes;

namespace Soenneker.Quark.Suite.Benchmarks;

[MemoryDiagnoser]
public class ThemeGenerationBenchmarks
{
    private readonly ComponentOptions _empty = new();
    private readonly ComponentOptions _styles = new()
    {
        Width = 240,
        Height = 120,
        Padding = Padding.OnY.Is2.OnX.Is4,
        Display = Display.Flex,
        TextColor = TextColor.Primary,
        BackgroundColor = BackgroundColor.Card,
        Rounded = Rounded.Lg
    };
    private readonly Theme _emptyTheme = new();
    private readonly Theme _theme = new()
    {
        Buttons = new ButtonOptions
        {
            Padding = Padding.OnY.Is2.OnX.Is4,
            TextColor = TextColor.Primary,
            BackgroundColor = BackgroundColor.Primary,
            Rounded = Rounded.Lg
        },
        Cards = new CardOptions
        {
            Width = 320,
            Height = 240,
            Padding = Padding.OnY.Is2.OnX.Is4,
            Buttons = new ButtonOptions { Width = 120, Height = 40 },
            Bodies = new CardBodyOptions { Width = 280, Height = 180 }
        }
    };

    [Benchmark] public string EmptyOptions() => ComponentCssGenerator.Generate(_empty);
    [Benchmark] public string StyledOptions() => ComponentCssGenerator.Generate(_styles);
    [Benchmark] public string EmptyTheme() => ComponentsCssGenerator.Generate(_emptyTheme);
    [Benchmark] public string NestedTheme() => ComponentsCssGenerator.Generate(_theme);
}
