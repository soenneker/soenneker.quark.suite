using BenchmarkDotNet.Attributes;

namespace Soenneker.Quark.Suite.Benchmarks;

[MemoryDiagnoser]
public class ThemeGenerationBenchmarks
{
    private readonly ComponentOptions _empty = new();
    private readonly ComponentOptions _styles = new()
    {
        Width = "240px",
        Height = "120px",
        Padding = "0.5rem 1rem",
        Display = "flex",
        TextColor = "var(--primary)",
        BackgroundColor = "var(--card)",
        Rounded = "0.5rem"
    };
    private readonly Theme _emptyTheme = new();
    private readonly Theme _theme = new()
    {
        Buttons = new ButtonOptions
        {
            Padding = "0.5rem 1rem",
            TextColor = "var(--primary)",
            BackgroundColor = "var(--primary)",
            Rounded = "0.5rem"
        },
        Cards = new CardOptions
        {
            Width = "320px",
            Height = "240px",
            Padding = "0.5rem 1rem",
            Buttons = new ButtonOptions { Width = "120px", Height = "40px" },
            Bodies = new CardBodyOptions { Width = "280px", Height = "180px" }
        }
    };

    [Benchmark] public string EmptyOptions() => ComponentCssGenerator.Generate(_empty);
    [Benchmark] public string StyledOptions() => ComponentCssGenerator.Generate(_styles);
    [Benchmark] public string EmptyTheme() => ComponentsCssGenerator.Generate(_emptyTheme);
    [Benchmark] public string NestedTheme() => ComponentsCssGenerator.Generate(_theme);
}
