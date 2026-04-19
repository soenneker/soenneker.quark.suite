using AwesomeAssertions;
using Bunit;
using Soenneker.Quark;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void RadioGroup_matches_shadcn_base_classes()
    {
        var radioGroup = Render<RadioGroup>(parameters => parameters.Add(p => p.ChildContent, "Choice"));

        string radioGroupClasses = radioGroup.Find("[data-slot='radio-group']").GetAttribute("class")!;

        radioGroupClasses.Should().Contain("grid");
        radioGroupClasses.Should().Contain("gap-2");
        radioGroupClasses.Should().Contain("w-fit");
        radioGroupClasses.Should().NotContain("q-radio-group");
    }
}
