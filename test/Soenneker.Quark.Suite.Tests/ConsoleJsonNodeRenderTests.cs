using System.Linq;
using System.Text.Json;
using AwesomeAssertions;
using Bunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed class ConsoleJsonNodeRenderTests : BunitContext
{
    [Test]
    public void Duplicate_property_names_keep_distinct_nodes()
    {
        using var document = JsonDocument.Parse("{\"name\":1,\"name\":2}");
        var cut = Render<ConsoleJsonNode>(p => p.Add(c => c.Value, document.RootElement));
        cut.Find("summary").Click();
        cut.FindAll(".json-leaf").Should().HaveCount(2);
        cut.FindAll(".json-number").Select(e => e.TextContent).Should().Equal("1", "2");
    }

    [Test]
    public void Large_json_is_lazy_and_pages_without_losing_values()
    {
        using var document = JsonDocument.Parse(JsonSerializer.Serialize(Enumerable.Range(0, 10000)));
        var cut = Render<ConsoleJsonNode>(p => p.Add(c => c.Value, document.RootElement));
        cut.FindAll(".json-leaf").Should().BeEmpty();
        cut.Find("summary").Click();
        cut.FindAll(".json-leaf").Should().HaveCount(100);
        cut.FindAll("nav button")[1].Click();
        cut.FindAll(".json-leaf").Should().HaveCount(100);
        cut.Find(".json-leaf").TextContent.Should().Be("100,");
        cut.Find("summary").Click();
        cut.FindAll(".json-leaf").Should().BeEmpty();
    }
}
