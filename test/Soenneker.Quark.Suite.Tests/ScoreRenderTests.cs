using AwesomeAssertions;
using Bunit;
using Microsoft.Extensions.DependencyInjection;

namespace Soenneker.Quark.Suite.Tests;

public sealed class ScoreRenderTests : BunitContext
{
    [Test]
    public void Small_score_keeps_concentric_rings_and_updates_the_existing_indicator()
    {
        Services.AddDefaultQuarkOptionsAsScoped();
        var cut = Render<Score>(p => p.Add(c => c.Value, 7).Add(c => c.ScoreSize, 28).Add(c => c.Thickness, 3));
        cut.FindAll("link[rel=stylesheet]").Should().BeEmpty();
        cut.Find("[data-slot=score-indicator]").ClassList.Should().NotContain("motion-reduce:transition-none");
        var indicator = cut.Find("[data-slot=score-indicator]");
        indicator.GetAttribute("cx").Should().Be("14");
        indicator.GetAttribute("cy").Should().Be("14");
        indicator.GetAttribute("r").Should().Be("12.5");
        indicator.GetAttribute("style").Should().Contain("stroke-dashoffset: 93");
        // Timing must work even when a consumer's generated CSS omits duration utilities.
        indicator.GetAttribute("style").Should().Contain("transition-duration: 1000ms");
        indicator.GetAttribute("style").Should().Contain("transition-timing-function: cubic-bezier(0, 0, 0.2, 1)");
        cut.Find("[data-slot=score-track]").GetAttribute("r").Should().Be("12.5");
        cut.Render(p => p.Add(c => c.Value, 100));
        indicator = cut.Find("[data-slot=score-indicator]");
        indicator.GetAttribute("style").Should().Contain("stroke-dashoffset: 0");
        cut.Find("[data-slot=score-value]").TextContent.Should().Be("100");
    }
}
