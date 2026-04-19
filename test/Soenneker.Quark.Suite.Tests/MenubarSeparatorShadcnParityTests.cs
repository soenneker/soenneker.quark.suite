using AwesomeAssertions;
using Bunit;
using Soenneker.Quark;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void MenubarSeparator_matches_shadcn_base_classes()
    {
        var separator = Render<MenubarSeparator>();

        string separatorClasses = separator.Find("[data-slot='menubar-separator']").GetAttribute("class")!;

        separatorClasses.Should().Contain("-mx-1");
        separatorClasses.Should().Contain("my-1");
        separatorClasses.Should().Contain("h-px");
        separatorClasses.Should().Contain("bg-border");
        separatorClasses.Should().NotContain("q-menubar-separator");
    }
}
