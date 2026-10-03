using AwesomeAssertions;
using Bunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Field_error_cache_observes_mutations_and_preserves_first_occurrence_order()
    {
        string?[] errors = [" zero ", "one", "two", "three", "four", "five", "six", "seven", "eight", "eight", null];
        var cut = Render<FieldError>(p => p.Add(x => x.Errors, errors));
        cut.FindAll("li").Should().HaveCount(9);
        cut.FindAll("li")[0].TextContent.Should().Be("zero");

        errors[0] = "eight";
        errors[1] = "one changed";
        cut.Render(p => p.Add(x => x.Errors, errors));
        cut.FindAll("li").Should().HaveCount(8);
        cut.FindAll("li")[0].TextContent.Should().Be("eight");
        cut.FindAll("li")[1].TextContent.Should().Be("one changed");

        cut.Render(p => p.Add(x => x.Errors, []));
        cut.FindAll("[role=alert]").Should().BeEmpty();
    }
}
