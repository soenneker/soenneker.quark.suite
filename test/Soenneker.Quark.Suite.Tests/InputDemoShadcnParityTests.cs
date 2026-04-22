using AwesomeAssertions;
using Bunit;
using System.Linq;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Input_demo_page_uses_shadcn_preview_overrides()
    {
        var cut = Render<global::Soenneker.Quark.Suite.Demo.Pages.Components.InputDemo>();
        var previewClasses = cut.FindAll("[data-slot='preview']")
            .Select(node => node.FirstElementChild!.GetAttribute("class")!)
            .ToList();

        previewClasses.Count(cls => cls.Contains("*:max-w-xs")).Should().BeGreaterThanOrEqualTo(10);
        previewClasses.Should().Contain(cls => cls.Contains("p-6"));
        previewClasses.Should().Contain(cls => cls.Contains("h-[32rem]"));
    }
}
