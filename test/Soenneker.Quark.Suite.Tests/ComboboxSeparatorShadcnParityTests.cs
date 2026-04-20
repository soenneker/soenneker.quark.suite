using AwesomeAssertions;
using Bunit;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void ComboboxSeparator_matches_shadcn_default_component_contract()
    {
        var cut = Render<ComboboxSeparator>();

        var classes = cut.Find("[data-slot='combobox-separator']").GetAttribute("class")!;

        classes.Should().Contain("bg-border");
        classes.Should().Contain("-mx-1");
        classes.Should().Contain("my-1");
        classes.Should().Contain("h-px");
        classes.Should().NotContain("q-combobox-separator");
    }
}
