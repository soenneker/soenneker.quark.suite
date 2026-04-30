using AwesomeAssertions;
using Bunit;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void AlertDescription_matches_shadcn_base_classes()
    {
        var cut = Render<AlertDescription>(parameters => parameters
            .Add(p => p.ChildContent, "Body"));

        var classes = cut.Find("[data-slot='alert-description']").GetAttribute("class")!;

        classes.Should().Contain("text-sm");
        classes.Should().Contain("text-muted-foreground");
        classes.Should().Contain("col-start-2");
        classes.Should().Contain("grid");
        classes.Should().Contain("justify-items-start");
        classes.Should().Contain("gap-1");
        classes.Should().Contain("[&_p]:leading-relaxed");
        classes.Should().NotContain("text-balance");
        classes.Should().NotContain("md:text-pretty");
        classes.Should().NotContain("[&_a]:underline");
        classes.Should().NotContain("[&_p:not(:last-child)]:mb-4");
        classes.Should().NotContain("q-alert-description");
    }
}
