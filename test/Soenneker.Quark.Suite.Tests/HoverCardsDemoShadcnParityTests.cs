using AwesomeAssertions;
using Bunit;
using System.Linq;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void HoverCards_demo_page_uses_shadcn_preview_overrides()
    {
        var cut = Render<global::Soenneker.Quark.Suite.Demo.Pages.Components.HoverCards>();
        var previews = cut.FindAll("[data-slot='preview']");
        var previewInners = previews
            .Select(node => node.FirstElementChild!)
            .ToList();

        previewInners.Count(node => node.GetAttribute("class")!.Contains("h-80")).Should().BeGreaterThanOrEqualTo(3);
        previewInners.Should().Contain(node => node.GetAttribute("class")!.Contains("h-[22rem]"));

        var rtlPreview = previews.First(node => node.GetAttribute("dir") == "rtl");
        rtlPreview.GetAttribute("data-lang").Should().Be("ar");
    }
}
