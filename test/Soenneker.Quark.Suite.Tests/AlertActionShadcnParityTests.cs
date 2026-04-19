using AwesomeAssertions;
using Bunit;
using Soenneker.Quark;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void AlertAction_matches_shadcn_base_classes()
    {
        var action = Render<AlertAction>(parameters => parameters
            .Add(p => p.ChildContent, "Undo"));

        string actionClasses = action.Find("[data-slot='alert-action']").GetAttribute("class")!;

        actionClasses.Should().Contain("absolute");
        actionClasses.Should().Contain("top-4");
        actionClasses.Should().Contain("right-4");
        actionClasses.Should().NotContain("q-alert-action");
    }
}
