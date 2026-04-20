using AwesomeAssertions;
using Bunit;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void InputGroup_matches_shadcn_base_classes()
    {
        var inputGroup = Render<InputGroup>(parameters => parameters
            .AddChildContent("<input data-slot=\"input-group-control\" />"));

        var groupClasses = inputGroup.Find("[data-slot='input-group']").GetAttribute("class")!;

        groupClasses.Should().Contain("group/input-group");
        groupClasses.Should().Contain("relative");
        groupClasses.Should().Contain("flex");
        groupClasses.Should().Contain("w-full");
        groupClasses.Should().Contain("items-center");
        groupClasses.Should().Contain("rounded-lg");
        groupClasses.Should().Contain("border");
        groupClasses.Should().Contain("border-input");
        groupClasses.Should().Contain("bg-transparent");
        groupClasses.Should().Contain("transition-colors");
        groupClasses.Should().Contain("h-8");
        groupClasses.Should().Contain("min-w-0");
        groupClasses.Should().NotContain("shadow-xs");
        groupClasses.Should().NotContain("q-input-group");
    }
}
