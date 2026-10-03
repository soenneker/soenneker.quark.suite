using AwesomeAssertions;
using Bunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Numeric_input_preserves_decimal_scale_when_values_compare_equal()
    {
        var cut = Render<NumericInput>(p => p.Add(c => c.Value, 1.0m).Add(c => c.Min, 0.0m).Add(c => c.Max, 10.0m).Add(c => c.Step, 0.1m));
        cut.Find("input").GetAttribute("value").Should().Be("1.0");
        cut.Render(p => p.Add(c => c.Value, 1.00m).Add(c => c.Min, 0.00m).Add(c => c.Max, 10.00m).Add(c => c.Step, 0.10m));
        var input = cut.Find("input");
        input.GetAttribute("value").Should().Be("1.00");
        input.GetAttribute("min").Should().Be("0.00");
        input.GetAttribute("max").Should().Be("10.00");
        input.GetAttribute("step").Should().Be("0.10");
    }
}
