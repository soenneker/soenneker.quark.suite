using AwesomeAssertions;
using Bunit;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void Breadcrumb_matches_shadcn_base_classes()
    {
        var breadcrumb = Render<Breadcrumb>(parameters => parameters
            .Add(p => p.Composed, false)
            .Add(p => p.ChildContent, "Trail"));

        var breadcrumbClasses = breadcrumb.Find("[data-slot='breadcrumb']").GetAttribute("class") ?? string.Empty;
        var listClasses = breadcrumb.Find("[data-slot='breadcrumb-list']").GetAttribute("class")!;

        breadcrumbClasses.Should().BeEmpty();

        listClasses.Should().Contain("flex");
        listClasses.Should().Contain("flex-wrap");
        listClasses.Should().Contain("items-center");
        listClasses.Should().Contain("gap-1.5");
        listClasses.Should().Contain("text-sm");
        listClasses.Should().Contain("wrap-break-word");
        listClasses.Should().Contain("text-muted-foreground");
        listClasses.Should().NotContain("q-breadcrumb-list");
        listClasses.Should().NotContain("sm:gap-2.5");
    }
}

