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

        inputClasses.Should().Contain("h-9");
        inputClasses.Should().Contain("rounded-md");
        inputClasses.Should().Contain("border-input");
        inputClasses.Should().Contain("file:h-7");
        inputClasses.Should().Contain("focus-visible:ring-[3px]");
        inputClasses.Should().NotContain("focus-visible:ring-3");
        inputClasses.Should().NotContain("aria-invalid:ring-3");
        inputClasses.Should().NotContain("q-input");
    }
}
