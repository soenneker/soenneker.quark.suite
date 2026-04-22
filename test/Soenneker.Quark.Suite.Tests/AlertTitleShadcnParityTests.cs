using AwesomeAssertions;
using Bunit;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void AlertTitle_matches_shadcn_base_classes()
    {
        var cut = Render<AlertTitle>(parameters => parameters
            .Add(p => p.ChildContent, "Alert"));

        var classes = cut.Find("[data-slot='alert-title']").GetAttribute("class")!;

        classes.Should().Contain("font-medium");
        classes.Should().Contain("group-has-[>svg]/alert:col-start-2");
        classes.Should().Contain("[&_a]:underline");
        classes.Should().Contain("[&_a]:underline-offset-3");
        classes.Should().NotContain("q-alert-title");
    }
}
