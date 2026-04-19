using AwesomeAssertions;
using Bunit;
using Soenneker.Quark;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void DialogTitle_matches_shadcn_default_component_contract()
    {
        var title = Render<DialogTitle>(parameters => parameters
            .Add(p => p.ChildContent, "Title"));

        string titleClasses = title.Find("[data-slot='dialog-title']").GetAttribute("class")!;

        titleClasses.Should().Contain("cn-font-heading");
        titleClasses.Should().Contain("text-base");
        titleClasses.Should().Contain("leading-none");
        titleClasses.Should().Contain("font-medium");
    }
}
