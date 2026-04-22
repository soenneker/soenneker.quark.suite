using AwesomeAssertions;
using Bunit;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void MenubarSub_matches_shadcn_base_classes()
    {
        var sub = Render<MenubarSub>(parameters => parameters.Add(p => p.ChildContent, "Sub"));

        var subClasses = sub.Find("[data-slot='menubar-sub']").GetAttribute("class")!;

        subClasses.Should().Contain("relative");
        subClasses.Should().NotContain("q-menubar-sub");
    }
}
