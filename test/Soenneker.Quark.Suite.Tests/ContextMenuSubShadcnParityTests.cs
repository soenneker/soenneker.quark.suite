using AwesomeAssertions;
using Bunit;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void ContextMenuSub_matches_shadcn_base_classes()
    {
        var sub = Render<ContextMenuSub>(parameters => parameters.Add(p => p.ChildContent, "Sub"));

        string subClasses = sub.Find("[data-slot='context-menu-sub']").GetAttribute("class")!;

        subClasses.Should().Contain("relative");
        subClasses.Should().NotContain("q-context-menu-sub");
    }

}
