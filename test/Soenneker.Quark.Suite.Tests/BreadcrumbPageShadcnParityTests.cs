using AwesomeAssertions;
using Bunit;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void BreadcrumbPage_matches_shadcn_base_classes()
    {
        var page = Render<BreadcrumbPage>(parameters => parameters
            .Add(p => p.ChildContent, "Current"));

        var pageClasses = page.Find("[data-slot='breadcrumb-page']").GetAttribute("class")!;

        pageClasses.Should().Contain("font-normal");
        pageClasses.Should().Contain("text-foreground");
        pageClasses.Should().NotContain("q-breadcrumb-page");
    }
}
