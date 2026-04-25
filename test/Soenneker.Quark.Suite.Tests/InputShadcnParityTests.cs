using AwesomeAssertions;
using Bunit;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Input_matches_shadcn_base_classes()
    {
        var input = Render<Input>(parameters => parameters.Add(p => p.Placeholder, "Enter text"));

        var inputClasses = input.Find("[data-slot='input']").GetAttribute("class")!;

        inputClasses.Should().Contain("h-8");
        inputClasses.Should().Contain("rounded-lg");
        inputClasses.Should().Contain("border-input");
        inputClasses.Should().Contain("file:h-6");
        inputClasses.Should().Contain("focus-visible:ring-3");
        inputClasses.Should().Contain("aria-invalid:ring-3");
        inputClasses.Should().NotContain("focus-visible:ring-[3px]");
        inputClasses.Should().NotContain("aria-invalid:ring-[3px]");
        inputClasses.Should().NotContain("q-input");
    }
}
