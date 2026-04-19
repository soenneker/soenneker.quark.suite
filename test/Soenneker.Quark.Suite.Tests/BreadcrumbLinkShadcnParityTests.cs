using AwesomeAssertions;
using Bunit;
using Soenneker.Quark;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void BreadcrumbLink_matches_shadcn_base_classes()
    {
        var link = Render<BreadcrumbLink>(parameters => parameters
            .Add(p => p.To, "/")
            .Add(p => p.ChildContent, "Home"));

        string linkClasses = link.Find("[data-slot='breadcrumb-link']").GetAttribute("class")!;

        linkClasses.Should().Contain("transition-colors");
        linkClasses.Should().Contain("hover:text-foreground");
        linkClasses.Should().NotContain("q-breadcrumb-link");
    }
}
