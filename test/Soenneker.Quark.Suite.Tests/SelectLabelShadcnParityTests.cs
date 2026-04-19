using AwesomeAssertions;
using Bunit;
using Soenneker.Quark;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void SelectLabel_matches_shadcn_base_classes()
    {
        var label = Render<SelectLabel>(parameters => parameters.Add(p => p.ChildContent, "Fruits"));

        string labelClasses = label.Find("[data-slot='select-label']").GetAttribute("class")!;

        labelClasses.Should().Contain("px-2");
        labelClasses.Should().Contain("py-1.5");
        labelClasses.Should().Contain("text-xs");
        labelClasses.Should().Contain("font-medium");
        labelClasses.Should().Contain("text-muted-foreground");
        labelClasses.Should().NotContain("q-select-label");
    }
}
