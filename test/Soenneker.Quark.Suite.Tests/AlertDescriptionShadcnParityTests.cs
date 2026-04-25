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
        classes.Should().Contain("text-balance");
        classes.Should().Contain("text-muted-foreground");
        classes.Should().Contain("md:text-pretty");
        classes.Should().Contain("[&_a]:underline");
        classes.Should().Contain("[&_a]:underline-offset-3");
        classes.Should().Contain("[&_a]:hover:text-foreground");
        classes.Should().Contain("[&_p:not(:last-child)]:mb-4");
        classes.Should().NotContain("col-start-2");
        classes.Should().NotContain("grid");
        classes.Should().NotContain("justify-items-start");
        classes.Should().NotContain("gap-1");
        classes.Should().NotContain("[&_p]:leading-relaxed");
        classes.Should().NotContain("q-alert-description");
    }
}
