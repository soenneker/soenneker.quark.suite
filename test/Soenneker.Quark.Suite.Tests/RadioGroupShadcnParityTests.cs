using AwesomeAssertions;
using Bunit;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void RadioGroup_matches_shadcn_base_classes()
    {
        var radioGroup = Render<RadioGroup>(parameters => parameters.Add(p => p.ChildContent, "Choice"));

        var radioGroupClasses = radioGroup.Find("[data-slot='radio-group']").GetAttribute("class")!;

        radioGroupClasses.Should().Contain("grid");
        radioGroupClasses.Should().Contain("gap-3");
        radioGroupClasses.Should().NotContain("w-fit");
        radioGroupClasses.Should().NotContain("q-radio-group");
    }
}
