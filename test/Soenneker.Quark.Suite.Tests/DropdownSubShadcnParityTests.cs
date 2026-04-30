using AwesomeAssertions;
using Bunit;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void DropdownSub_matches_shadcn_base_classes()
    {
        var sub = Render<DropdownSub>(parameters => parameters.Add(p => p.ChildContent, "Sub"));

        sub.Markup.Should().Contain("Sub");
        sub.FindAll("[data-slot='dropdown-menu-sub']").Should().BeEmpty();
        sub.Markup.Should().NotContain("q-dropdown-sub");
    }
}
