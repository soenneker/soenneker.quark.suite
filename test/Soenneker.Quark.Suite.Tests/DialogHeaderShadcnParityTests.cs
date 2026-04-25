using AwesomeAssertions;
using Bunit;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void DialogHeader_matches_shadcn_default_component_contract()
    {
        var header = Render<DialogHeader>(parameters => parameters
            .Add(p => p.ChildContent, "Header"));

        var headerClasses = header.Find("[data-slot='dialog-header']").GetAttribute("class")!;

        headerClasses.Should().Contain("flex");
        headerClasses.Should().Contain("flex-col");
        headerClasses.Should().Contain("gap-2");
        headerClasses.Should().Contain("text-center");
        headerClasses.Should().Contain("sm:text-left");
    }
}
