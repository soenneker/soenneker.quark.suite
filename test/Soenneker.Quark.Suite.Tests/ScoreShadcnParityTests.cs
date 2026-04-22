using AwesomeAssertions;
using Bunit;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Score_slots_use_data_slot_hooks_instead_of_legacy_q_classes()
    {
        var cut = Render<Score>(parameters => parameters
            .Add(p => p.Value, 42)
            .Add(p => p.Text, "Quality"));

        var score = cut.Find("[data-slot='score']");
        var circle = cut.Find("[data-slot='score-circle']");
        var center = cut.Find("[data-slot='score-center']");
        var value = cut.Find("[data-slot='score-value']");
        var text = cut.Find("[data-slot='score-text']");

        score.GetAttribute("class")!.Should().Contain("flex");
        score.GetAttribute("class")!.Should().Contain("flex-col");
        score.GetAttribute("class")!.Should().Contain("items-center");
        score.GetAttribute("class")!.Should().Contain("gap-2");
        circle.OuterHtml.Should().Contain("data-slot=\"score-circle\"");
        center.OuterHtml.Should().Contain("data-slot=\"score-center\"");
        value.TextContent.Should().Be("42");
        text.TextContent.Should().Be("Quality");
        cut.Markup.Should().NotContain("q-score");
        cut.Markup.Should().NotContain("q-score-circle");
        cut.Markup.Should().NotContain("q-score-value");
    }
}
