using AwesomeAssertions;
using Bunit;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void BreadcrumbEllipsis_matches_shadcn_base_classes()
    {
        var cut = Render<BreadcrumbEllipsis>();

        var classes = cut.Find("[data-slot='breadcrumb-ellipsis']").GetAttribute("class")!;

        classes.Should().Contain("flex");
        classes.Should().Contain("size-5");
        classes.Should().Contain("items-center");
        classes.Should().Contain("justify-center");
        classes.Should().Contain("[&>svg]:size-4");
        classes.Should().NotContain("size-9");
        classes.Should().NotContain("q-breadcrumb-ellipsis");
    }
}
