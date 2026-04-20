using AwesomeAssertions;
using Bunit;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void BreadcrumbEllipsis_matches_shadcn_base_classes()
    {
        var cut = Render<BreadcrumbEllipsis>();

        string classes = cut.Find("[data-slot='breadcrumb-ellipsis']").GetAttribute("class")!;

        classes.Should().Contain("flex");
        classes.Should().Contain("size-9");
        classes.Should().Contain("items-center");
        classes.Should().Contain("justify-center");
        classes.Should().Contain("[&>svg]:size-4");
        classes.Should().NotContain("q-breadcrumb-ellipsis");
    }
}
