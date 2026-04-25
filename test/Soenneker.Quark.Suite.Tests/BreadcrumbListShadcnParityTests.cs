using AwesomeAssertions;
using Bunit;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void BreadcrumbList_matches_shadcn_base_classes()
    {
        var cut = Render<BreadcrumbList>(parameters => parameters
            .Add(p => p.ChildContent, "Trail"));

        var classes = cut.Find("[data-slot='breadcrumb-list']").GetAttribute("class")!;

        classes.Should().Contain("flex");
        classes.Should().Contain("flex-wrap");
        classes.Should().Contain("items-center");
        classes.Should().Contain("gap-1.5");
        classes.Should().Contain("text-sm");
        classes.Should().Contain("break-words");
        classes.Should().Contain("text-muted-foreground");
        classes.Should().Contain("sm:gap-2.5");
        classes.Should().NotContain("q-breadcrumb-list");
    }
}
