using AwesomeAssertions;
using Bunit;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void BreadcrumbItem_matches_shadcn_base_classes()
    {
        var item = Render<BreadcrumbItem>(parameters => parameters
            .Add(p => p.ChildContent, "Home"));

        string itemClasses = item.Find("[data-slot='breadcrumb-item']").GetAttribute("class")!;

        itemClasses.Should().Contain("inline-flex");
        itemClasses.Should().Contain("items-center");
        itemClasses.Should().Contain("gap-1");
        itemClasses.Should().NotContain("q-breadcrumb-item");
    }
}
