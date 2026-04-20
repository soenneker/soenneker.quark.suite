using AwesomeAssertions;
using Bunit;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void DialogHeader_matches_shadcn_default_component_contract()
    {
        var header = Render<DialogHeader>(parameters => parameters
            .Add(p => p.ChildContent, "Header"));

        string headerClasses = header.Find("[data-slot='dialog-header']").GetAttribute("class")!;

        headerClasses.Should().Contain("flex");
        headerClasses.Should().Contain("flex-col");
        headerClasses.Should().Contain("gap-2");
        headerClasses.Should().NotContain("text-center");
        headerClasses.Should().NotContain("sm:text-left");
    }
}
