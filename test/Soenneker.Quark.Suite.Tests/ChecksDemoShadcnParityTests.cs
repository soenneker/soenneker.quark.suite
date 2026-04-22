using AwesomeAssertions;
using Bunit;
using System.Linq;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Checks_demo_page_uses_shadcn_preview_overrides()
    {
        var cut = Render<global::Soenneker.Quark.Suite.Demo.Pages.Components.Checks>();
        var previews = cut.FindAll("[data-slot='preview']");
        var previewInners = previews
            .Select(node => node.FirstElementChild!)
            .ToList();

        previewInners.Should().Contain(node => node.GetAttribute("class")!.Contains("h-80"));
        previewInners.Should().Contain(node => node.GetAttribute("class")!.Contains("p-4 md:p-8"));

        var rtlPreview = previews.First(node => node.GetAttribute("dir") == "rtl");
        rtlPreview.GetAttribute("data-lang").Should().Be("ar");
        rtlPreview.FirstElementChild!.GetAttribute("class")!.Should().Contain("h-80");
    }
}
