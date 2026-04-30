using AwesomeAssertions;
using Bunit;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void BreadcrumbSeparator_matches_shadcn_base_classes()
    {
        var separator = Render<BreadcrumbSeparator>();

        var separatorClasses = separator.Find("[data-slot='breadcrumb-separator']").GetAttribute("class")!;

        separatorClasses.Should().Contain("[&>svg]:size-3.5");
        separatorClasses.Should().NotContain("inline-flex");
        separatorClasses.Should().NotContain("items-center");
        separatorClasses.Should().NotContain("q-breadcrumb-separator");
    }
}
