using System;
using System.Collections.Generic;
using AwesomeAssertions;
using Bunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed class RenderRepresentationTests : BunitContext
{
    [Test]
    public void Suppression_observes_equal_values_with_different_rendered_representations()
    {
        (object Before, object After)[] values =
        [
            (0d, -0d), (0f, -0f), (1m, 1.00m),
            (new DateTime(2026, 10, 3, 12, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 3, 12, 0, 0, DateTimeKind.Utc)),
            (new DateTimeOffset(2026, 10, 3, 12, 0, 0, TimeSpan.Zero), new DateTimeOffset(2026, 10, 3, 13, 0, 0, TimeSpan.FromHours(1)))
        ];
        foreach (var (before, after) in values)
        {
            var cut = Render<RenderRepresentationProbe>(p => p.Add(c => c.Value, before));
            string original = cut.Markup;
            cut.Render(p => p.Add(c => c.Value, after));
            cut.Markup.Should().NotBe(original);
            int rendered = cut.RenderCount;
            cut.Render(p => p.Add(c => c.Value, after));
            cut.RenderCount.Should().Be(rendered);

            var attributes = new Dictionary<string, object> { ["data-value"] = before };
            var attributeCut = Render<RenderRepresentationProbe>(p => p.Add(c => c.Attributes, attributes));
            string originalAttribute = attributeCut.Markup;
            attributes["data-value"] = after;
            attributeCut.Render(p => p.Add(c => c.Attributes, attributes));
            attributeCut.Markup.Should().NotBe(originalAttribute);
        }
    }
}
