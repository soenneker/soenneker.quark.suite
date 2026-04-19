using AwesomeAssertions;
using Bunit;
using Soenneker.Quark;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void SelectSeparator_matches_shadcn_base_classes()
    {
        var separator = Render<SelectSeparator>();

        string separatorClasses = separator.Find("[data-slot='select-separator']").GetAttribute("class")!;

        separatorClasses.Should().Contain("pointer-events-none");
        separatorClasses.Should().Contain("-mx-1");
        separatorClasses.Should().Contain("my-1");
        separatorClasses.Should().Contain("h-px");
        separatorClasses.Should().Contain("bg-border");
        separatorClasses.Should().NotContain("q-select-separator");
    }
}
