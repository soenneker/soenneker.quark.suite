using AwesomeAssertions;
using Bunit;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void BreadcrumbSeparator_matches_shadcn_base_classes()
    {
        var separator = Render<BreadcrumbSeparator>();

        string separatorClasses = separator.Find("[data-slot='breadcrumb-separator']").GetAttribute("class")!;

        separatorClasses.Should().Contain("[&>svg]:size-3.5");
        separatorClasses.Should().NotContain("q-breadcrumb-separator");
    }
}
