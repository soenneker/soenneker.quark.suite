using AwesomeAssertions;
using Bunit;
using System.Linq;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void InputOtps_demo_page_uses_shadcn_preview_overrides()
    {
        var cut = Render<global::Soenneker.Quark.Suite.Demo.Pages.Components.InputOtps>();

        cut.FindAll("[data-slot='preview']")
            .Select(node => node.FirstElementChild!.GetAttribute("class")!)
            .Should().OnlyContain(cls => cls.Contains("min-h-72"));
    }
}
